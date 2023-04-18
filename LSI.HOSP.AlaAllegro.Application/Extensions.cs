using FluentValidation;
using FluentValidation.AspNetCore;
using LSI.HOSP.AlaAllegro.Application.Auctions.Commands;
using LSI.HOSP.AlaAllegro.Application.PurchaseOffers.Commands;
using LSI.HOSP.AlaAllegro.Application.Users.Commands;
using LSI.HOSP.AlaAllegro.Infrastructure.Services;
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

            services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
            //    .AddFluentValidationAutoValidation();
                //.AddFluentValidationClientsideAdapters();
                /*
            

            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
            services.AddScoped<IValidator<CreateUpdateAuctionCommand>, CreateUpdateAuctionCommandCommandValidator>();
            services.AddScoped<IValidator<AddPurchaseOfferCommand>, AddPurchaseOfferCommandValidator>();
            */

            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }
    }
}
