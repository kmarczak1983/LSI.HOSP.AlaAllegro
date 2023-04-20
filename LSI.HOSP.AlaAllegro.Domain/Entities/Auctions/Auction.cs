using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using MathNet.Numerics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Domain.Entities.Auctions
{
    public class Auction : BaseEntity<Guid>
    {
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Body { get; set; }
        
        public decimal StartPrice { get; set; }

        public virtual User Author { get; set; }
        public int AuthorId { get; set; }

        public virtual List<PurchaseOffer> PurchaseOffers { get; set; }
    }
}
