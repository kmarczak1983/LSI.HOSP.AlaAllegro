using LSI.HOSP.AlaAllegro.Domain.Entities;
using LSI.HOSP.AlaAllegro.Domain.Exceptions;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Infrastructure.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity//<Guid>
    {
        protected readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(c => !c.IsDeleted).Where(predicate);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _dbContext.Set<TEntity>().Where(c => !c.IsDeleted);
        }

        public async Task<TEntity> AddAsync(TEntity entity,
            CancellationToken cancellationToken = new())
        {
            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity,
            CancellationToken cancellationToken = new())
        {
            entity.LastModifiedDate = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
