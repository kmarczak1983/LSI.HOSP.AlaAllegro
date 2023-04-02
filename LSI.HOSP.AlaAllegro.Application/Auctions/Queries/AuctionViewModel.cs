using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Application.Auctions.Queries
{
    public class AuctionViewModel
    {
        public AuctionViewModel(int id, string title, string body) 
        {
            Id = id;
            Title = title;
            Body = body;

        }

        public int Id { get; }
        public string Title { get; }
        public string Body { get; }
    }
}
