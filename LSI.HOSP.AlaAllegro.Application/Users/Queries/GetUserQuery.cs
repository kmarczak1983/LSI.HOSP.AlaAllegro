using LSI.HOSP.AlaAllegro.Application.Auctions.Queries;
using LSI.HOSP.AlaAllegro.Application.Common.Commands;
using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using LSI.HOSP.AlaAllegro.Infrastructure;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using LSI.HOSP.AlaAllegro.Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Application.Users.Queries
{
    public class GetUserQuery : IRequest<UserViewModel>
    {
        public GetUserQuery()
        {
        }

        public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
        {
            private readonly IRepository<User> _repository;
            private readonly ICurrentUserService _currentUserService;

            public GetUserQueryHandler(IRepository<User> repository,
                                       ICurrentUserService currentUserService)
            {
                _repository = repository;                 
                _currentUserService = currentUserService;
            }

            public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _repository.GetFirstOrDefaultAsync(u => u.Id == (int)_currentUserService.GetUserId, cancellationToken);

                return new UserViewModel(user.FirstName, user.LastName, user.Email);
            }
        }
    }
}
