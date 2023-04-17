using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using LSI.HOSP.AlaAllegro.Infrastructure;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LSI.HOSP.AlaAllegro.Application.Users.Commands
{
    public class LoginUserCommand : IRequest<string>
    {
        public string Email { get; set; }

        public string Password { get; set; }    
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IRepository<User> repository;
         // private readonly ICustomerBaseFieldsService customerBaseFields;
       // private readonly IValidationProvider<Guest> _validationProvider;
        private readonly AppDbContext _appDbContext;
        private readonly AuthenticationSettings _authenticationSettings;


        public LoginUserCommandHandler(IRepository<User> repository
            ,
                                         //ICustomerBaseFieldsService customerBaseFields,
                                         //IValidationProvider<Guest> validationProvider,
                                         AppDbContext appDbContext,
                                         AuthenticationSettings authenticationSettings)
        {
          this.repository = repository;
            //this.customerBaseFields = customerBaseFields;
            //_validationProvider = validationProvider;

            _appDbContext = appDbContext;
            _authenticationSettings = authenticationSettings;  
        }

        
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users.GetFirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user is null) 
            {
                throw new Exception("aaa");
            }

            var password = HashPassword(request.Password);

            if (!(password == user.Password))
            {
                throw new Exception("aaa");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")//,
                //new Claim("PhoneNumber", user.PhoneNumber)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
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
