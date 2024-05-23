using EventDataAccess.Abstractions;
using EventDataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventDataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly IMainDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(IMainDbContext context)
        {
            Context = context;
            DbSet = context.Instance.Set<TEntity>();
        } 

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity == null)
            {
                return false; 
            }

            DbSet.Remove(entity);
            await Context.SaveChangesAsync();

            return true;
        }
        //For now unused, might be useful later
        public async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return await DbSet.AnyAsync(filter);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int? itemsToSkip = null, 
            int? itemsToTake = null
            )
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (itemsToSkip != null)
            {
                query = query.Skip(itemsToSkip.Value);
            }

            if (itemsToTake != null)
            {
                query = query.Take(itemsToTake.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbSet.Attach(entity);

            Context.Entry(entity).State = EntityState.Modified;

            await Context.SaveChangesAsync();

            return entity;
        }
    }
}
