﻿using System;
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
