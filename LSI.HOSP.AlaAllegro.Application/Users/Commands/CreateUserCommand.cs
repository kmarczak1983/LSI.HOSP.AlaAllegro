using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using LSI.HOSP.AlaAllegro.Domain.Exceptions;
using LSI.HOSP.AlaAllegro.Infrastructure;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using LSI.HOSP.AlaAllegro.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class CreateUserCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Phone { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IRepository<User> repository;
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;


        public CreateUserCommandHandler(IRepository<User> repository,
                                        AppDbContext appDbContext,
                                        ICurrentUserService currentUserService)
        {
          this.repository = repository;
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
        }

        
        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.GetUserId is null)
            {
                if (await repository.GetQueryable().AnyAsync(u => u.Email.Equals(request.Email),  cancellationToken)) 
                    throw new ElementNotUniqueException(nameof(User));

                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Password = request.Password
                };

                user.Password = HashPassword(request.Password);

                await repository.AddAsync(user, cancellationToken);                

                return Unit.Value;
            }
            else
            {
                var userId = (int)_currentUserService.GetUserId;

                var user = await _appDbContext.Users.GetFiltered().GetFirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
                user.Password = HashPassword(request.Password);

                await repository.UpdateAsync(user, cancellationToken);

                return Unit.Value;
            }                                               
        }

        private string HashPassword(string password)
        {
            SHA256 sha256 = SHA256Managed.Create();            
            UTF8Encoding objUtf8 = new UTF8Encoding();            
            return Convert.ToBase64String(sha256.ComputeHash(objUtf8.GetBytes(password)));
        }
        /*
        protected async Task CheckUniqueness(Expression<Func<Guest, bool>> expression)
        {
            var exists = await repository.AnyAsync(expression);
          if (exists)
                throw new ElementNotUniqueException(typeof(Guest).Name); // nameof doesn't work with generic
        }
       */
    }
}
