using LSI.HOSP.AlaAllegro.Application.Common.Commands;
using LSI.HOSP.AlaAllegro.Domain.Entities;
using LSI.HOSP.AlaAllegro.Domain.Entities.Auctions;
using LSI.HOSP.AlaAllegro.Infrastructure;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using MediatR;
//using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Application.Auctions.Queries
{
    public class GetAuctionByIdQuery : BaseCommand<Guid>, IRequest<AuctionViewModel>
    {
        public GetAuctionByIdQuery(Guid id)
        {
            Id = id;
        }

        public class GetAuctionByIdQueryHandler : IRequestHandler<GetAuctionByIdQuery, AuctionViewModel>
        {            
            private readonly IAuctionRepository _auctionRepository;
            private readonly IPurchaseOfferRepository _purchaseOfferRepository;

            public GetAuctionByIdQueryHandler(IAuctionRepository auctionRepository, IPurchaseOfferRepository purchaseOfferRepository)
            {
                _auctionRepository = auctionRepository;
                _purchaseOfferRepository = purchaseOfferRepository;
            }

            public async Task<AuctionViewModel> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken)
            {
                var auction = await _auctionRepository.Get(request.Id, cancellationToken);

                var lastPurchaseOffer = await _purchaseOfferRepository.GetLast(auction.Id, cancellationToken);                    

                return new AuctionViewModel(
                        auction.Id.ToString(), 
                        auction.Title,                        
                        new AuctionViewModel.UserCreatedBy(
                                auction.Author.FirstName + " " + auction.Author.LastName,
                                auction.Author.Email, 
                                auction.Author.PhoneNumber),
                        auction.Body,
                        auction.LastModifiedDate,
                        auction.CreatedDate,
                        lastPurchaseOffer is null ? auction.StartPrice.ToString() : lastPurchaseOffer.Price.ToString(),                        
                        lastPurchaseOffer is null ? null : lastPurchaseOffer.User.FirstName + " " + lastPurchaseOffer.User.LastName);
            }
        }
    }
}
