using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventDataAccess.Abstractions
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<TEntity> InsertAsync(TEntity entity);
        public Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int? itemsToSkip = null,
            int? itemsToTake = null
            );
        public Task<TEntity?> GetByIdAsync(object id);
        public Task<bool> Exists(Expression<Func<TEntity, bool>> filter);
        public Task<bool> DeleteAsync(object id);
        public Task<TEntity> UpdateAsync(TEntity entity);
    }
}
