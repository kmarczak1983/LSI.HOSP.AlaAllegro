using LSI.HOSP.AlaAllegro.Domain.Entities.Auctions;
using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<PurchaseOffer> PurchaseOffers { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auction>(eb =>
            {
                eb.Property(a => a.StartPrice).HasPrecision(14, 2);
                eb.HasOne(a => a.Author)
                    .WithMany(u => u.Auctions)
                    .HasForeignKey(c => c.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            
            modelBuilder.Entity<PurchaseOffer>(eb =>
            {
                eb.Property(po => po.Price).HasPrecision(14, 2);
                eb.HasOne(po => po.Auction)
                    .WithMany(a => a.PurchaseOffers)
                    .HasForeignKey(po  => po.AuctionId)
                    .OnDelete(DeleteBehavior.Cascade);
                eb.HasOne(po => po.User)
                    .WithMany(u => u.PurchaseOffers)
                    .HasForeignKey(po =>po.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                eb.HasAlternateKey(po => new { po.AuctionId, po.UserId });
            });
                        
        }
    }
}
