using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using LSI.HOSP.AlaAllegro.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            services.AddScoped(serviceProvider =>
            {
                var connectionString = configuration.GetConnectionString("AlaAllegroConnectionString");
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseNpgsql(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    //.UseSqlServer(connectionString)
                    .Options;                    
                var context = new AppDbContext(options);
                return context;
            });
            
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IAuctionRepository), typeof(AuctionRepository));
            services.AddTransient(typeof(IPurchaseOfferRepository), typeof(PurchaseOfferRepository));

        }
    }
}
