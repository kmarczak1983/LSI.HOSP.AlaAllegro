using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Domain.Entities.Auctions
{
    public class PurchaseOffer : BaseEntity<int>
    {
        public virtual Auction Auction { get; set; }
        public Guid AuctionId { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }        

        public decimal Price { get; set; }
        
    }
}
