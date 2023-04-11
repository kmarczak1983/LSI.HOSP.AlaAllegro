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
          /*
             "createdBy": { "fullName": string, "email": string, "phone": string },
			 "body": string,

             "modificationDate": string(pełna data w UTC ISO),
			 "createdDate": string(pełna data w UTC ISO),
			 "currentPrice": string(aktualna najwyższa oferta kupna bądź cena początkowa),
			 "currentPriceHolder": string | null(imię i nazwisko użytkownika z najwyższą stawką, jeśli nikt nie dał ceny to null)
          */



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
