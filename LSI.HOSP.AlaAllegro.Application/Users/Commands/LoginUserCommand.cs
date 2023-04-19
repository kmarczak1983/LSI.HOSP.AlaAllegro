using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using MediatR;
using System.Security.Cryptography;
using System.Threading;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;

namespace LSI.HOSP.AlaAllegro.Application.Users.Commands
{
    public class LoginUserCommand : IRequest<string>
    {
        public string Email { get; set; }

        public string Password { get; set; }    
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IRepository<User> _repository;
        private readonly AuthenticationSettings _authenticationSettings;

        public LoginUserCommandHandler(IRepository<User> repository,
                                       AuthenticationSettings authenticationSettings)
        {
            _repository = repository;            
            _authenticationSettings = authenticationSettings;  
        }

        
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetFirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user is null) 
            {
                throw new Exception("aaa");
            }

            var password = UserHashPassword.HashPassword(request.Password);

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
    }
}
