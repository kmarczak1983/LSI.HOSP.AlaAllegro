using LSI.HOSP.AlaAllegro.Domain.Entities;
using LSI.HOSP.AlaAllegro.Domain.Exceptions;
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
    public static class DbContextExtension
    {
        public static IQueryable<TEntity> GetFiltered<TEntity>(this DbSet<TEntity> dbSet, Expression<Func<TEntity, bool>> whereExpression = null)
            where TEntity : BaseEntity
        {
            var query = dbSet.Where(e => !e.IsDeleted);
            if (whereExpression is not null)
                query = query.Where(whereExpression);
            return query;
        }

        public static async Task<TEntity> GetFirstOrDefaultAsync<TEntity>(this IQueryable<TEntity> query,
                                                                         Expression<Func<TEntity, bool>> fodExrpession,
                                                                         CancellationToken cancellationToken,
                                                                         bool throwException = true)
        {
            var entity = await query.FirstOrDefaultAsync(fodExrpession, cancellationToken);

            if (throwException)
            {
                if (entity is null)
                    throw new EntityNotFoundException(typeof(TEntity).Name);
            }

            return entity;
        }
    }
}
