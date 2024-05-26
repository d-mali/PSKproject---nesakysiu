using EventBackend;
using EventDataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventDataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly MainDbContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(MainDbContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            DbSet.Remove(entity);
            await _context.SaveChangesAsync();

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
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbSet.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
