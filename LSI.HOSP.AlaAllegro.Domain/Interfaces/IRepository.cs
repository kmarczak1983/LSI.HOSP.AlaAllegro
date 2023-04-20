using LSI.HOSP.AlaAllegro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate);
        
        IQueryable<TEntity> GetQueryable();  

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = new());

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = new());

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = new());

        Task<TEntity> GetLastModifiedOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = new());
    }
}
