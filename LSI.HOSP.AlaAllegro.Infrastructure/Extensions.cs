﻿using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Infrastructure
{
    public static class Extensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("AlaAllegroConnectionString");
            //services.AddDbContext<AppDbContext>(
            //options =>  options.UseSqlServer(connectionString)
            //);
            //services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(serviceProvider =>
            {
                var connectionString = configuration.GetConnectionString("AlaAllegroConnectionString");                    
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;
                var context = new AppDbContext(options);
                return context;
            });


        }
    }
}
