using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Application.Auctions.Queries
{
    public class AuctionViewModel
    {
        public record UserCreatedBy(string fullName, string email, string phone);

        public AuctionViewModel(string id, string title, UserCreatedBy createdBy, string body, DateTime modificationDate, DateTime createdDate, string currentPrice, string currentPriceHolder) 
        {
            Id = id;
            Title = title;
            CreatedBy = createdBy;
            Body = body;
            ModificationDate = modificationDate;
            CreatedDate = createdDate;
            CurrentPrice = currentPrice;
            CurrentPriceHolder = currentPriceHolder;         
        }

        public string Id { get; }
        public string Title { get; }
        public UserCreatedBy CreatedBy { get; }
        public string Body { get; }
        public DateTime ModificationDate { get; }
        public DateTime CreatedDate { get; }
        public string CurrentPrice { get; }
        public string CurrentPriceHolder { get; }  
    }
}
