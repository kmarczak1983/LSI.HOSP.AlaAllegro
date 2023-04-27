using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using LSI.HOSP.AlaAllegro.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Infrastructure
{
    public static class Extensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            /*
            services.AddScoped(serviceProvider =>
            {
                var connectionString = configuration.GetConnectionString("AlaAllegroConnectionString");
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseNpgsql(connectionString)
                    
                    //.UseSqlServer(connectionString)
                    .Options;                    
                var context = new AppDbContext(options);
                return context;
            });
            */

            services.AddDbContext<AppDbContext>((IServiceProvider services, DbContextOptionsBuilder builder) =>
            {
                var configuration = services.GetService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("AlaAllegroConnectionString");
                builder.UseNpgsql(connectionString)
                    .LogTo(data => Debug.WriteLine(data), LogLevel.Information);
            });

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));            
            services.AddTransient<IAuctionRepository, AuctionRepository>();
            services.AddTransient<IPurchaseOfferRepository, PurchaseOfferRepository>();
        }
    }
}
