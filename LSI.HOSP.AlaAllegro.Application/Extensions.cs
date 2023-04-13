using FluentValidation;
using FluentValidation.AspNetCore;
using LSI.HOSP.AlaAllegro.Application.Users.Commands;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace LSI.HOSP.AlaAllegro.Application
{
    public static class Extensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddFluentValidation();

            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();

        }
    }
}
