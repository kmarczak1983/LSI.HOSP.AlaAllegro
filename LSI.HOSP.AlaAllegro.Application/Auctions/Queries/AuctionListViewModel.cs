using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Application.Auctions.Queries
{
    public class AuctionListViewModel
    {        
        public AuctionListViewModel(string id, string title, string createdBy, string modificationDate, string price) 
        {
            Id = id;
            Title = title;
            CreatedBy = createdBy;           
            ModificationDate = modificationDate;            
            Price = price;            
        }
     
        public string Id { get; }
        public string Title { get; }
        public string CreatedBy { get; }        
        public string ModificationDate { get; }        
        public string Price { get; }        
    }
}
