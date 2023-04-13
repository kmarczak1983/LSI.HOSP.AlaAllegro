using LSI.HOSP.AlaAllegro.Domain.Entities;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Application.Common.Queries
{
    public abstract class BaseQueryListHandler<TQuery, TResult, TEntity> : IRequestHandler<TQuery, List<TResult>>
        where TQuery : IRequest<List<TResult>>
        where TEntity : BaseEntity
    {
        protected readonly IRepository<TEntity> _repository;
        protected BaseQueryListHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        public virtual async Task<List<TResult>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return await Query(request).Select(Map()).ToListAsync(cancellationToken);
        }

        protected abstract Expression<Func<TEntity, TResult>> Map();

        protected virtual IQueryable<TEntity> Query(TQuery request)
            => _repository.GetQueryable().Where(c => !c.IsDeleted).AsNoTracking();
    }
}
