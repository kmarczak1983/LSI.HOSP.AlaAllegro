using AutoMapper;
using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using LSI.HOSP.AlaAllegro.Domain.Exceptions;
using LSI.HOSP.AlaAllegro.Infrastructure;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using LSI.HOSP.AlaAllegro.Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LSI.HOSP.AlaAllegro.Application.Users.Commands
{
    public class CreateUpdateUserCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Phone { get; set; }        
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUpdateUserCommand, Unit>
    {
        private readonly IRepository<User> repository;        
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IRepository<User> repository,          
                                        ICurrentUserService currentUserService,
                                        IMapper mapper)
        {
            this.repository = repository;            
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        
        public async Task<Unit> Handle(CreateUpdateUserCommand request, CancellationToken cancellationToken)
        {            
            if (_currentUserService.GetUserId is null)
            {
                /*
                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Password = request.Password
                };
                user.Password = UserHashPassword.HashPassword(request.Password);
                */
                var user = _mapper.Map<User>(request);                

                await repository.AddAsync(user, cancellationToken);                

                return Unit.Value;
            }
            else
            {                
                var currentUserId = (int)_currentUserService.GetUserId;
                
                var user = await repository.GetFirstOrDefaultAsync(u => u.Id == currentUserId, cancellationToken);
                
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
                if (request.Phone != null) 
                { 
                    user.PhoneNumber = request.Phone;
                }
                user.Password = UserHashPassword.HashPassword(request.Password);                

                await repository.UpdateAsync(user, cancellationToken);

                return Unit.Value;
            }                                               
        }

    
    }
}
