using LSI.HOSP.AlaAllegro.Domain.Entities.Auctions;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LSI.HOSP.AlaAllegro.Domain.Entities.Users
{
    public class User : BaseEntity<int>
    {
        [MaxLength(200)]
        public string FirstName { get; set; }

        [MaxLength(200)]
        public string LastName { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(44)]
        public string Password { get; set; }

        [MaxLength(10)]
        public string PhoneNumber { get; set; }

        public virtual List<Auction> Auctions { get; set; }

        public virtual List<PurchaseOffer> PurchaseOffers { get; set; } 
    }
}
