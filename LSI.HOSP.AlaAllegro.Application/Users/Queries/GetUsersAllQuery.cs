using AutoMapper;
using LSI.HOSP.AlaAllegro.Application.Common.Queries;
using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Application.Users.Queries
{
    public class GetUsersAllQuery : IRequest<IEnumerable<UsersAllViewModel>>
    {
    }

    internal class GetUsersAllQueryHandler : IRequestHandler<GetUsersAllQuery, IEnumerable<UsersAllViewModel>>
    //internal class GetUsersAllQueryHandler : BaseQueryListHandler<GetUsersAllQuery, UsersAllViewModel, User>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public GetUsersAllQueryHandler(IMapper mapper, IRepository<User> repository)
        {
           // _carWorkshopRepository = carWorkshopRepository;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<UsersAllViewModel>> Handle(GetUsersAllQuery request, CancellationToken cancellationToken)
        {
            /*  mapper Map is slowly, select always all columns                
                var users = _repository.GetQueryable().ToList();
                var dtos = _mapper.Map<IEnumerable<UsersAllViewModel>>(users);
            */


            // mapper ProjtecTo is more efficient, is select are only necessary columns defined in model
            var dtos = _mapper.ProjectTo<UsersAllViewModel>(_repository.GetQueryable());
            
            return dtos;            
        }
    }
}
