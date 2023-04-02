using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Domain.Entities.Auctions
{
    public class Auction : BaseEntity<int>
    {
        public string Title { get; set; }

        public string Body { get; set; }
    }
}
