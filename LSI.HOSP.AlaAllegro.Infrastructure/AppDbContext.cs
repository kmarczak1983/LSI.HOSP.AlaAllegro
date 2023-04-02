using LSI.HOSP.AlaAllegro.Domain.Entities.Auctions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Auction> Auctions { get; set; }

        public virtual DbSet<Auction> Users { get; set; }
    }
}
