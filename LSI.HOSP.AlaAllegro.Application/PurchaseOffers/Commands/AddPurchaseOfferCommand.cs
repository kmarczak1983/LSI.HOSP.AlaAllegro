using LSI.HOSP.AlaAllegro.Application.Users.Commands;
using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using LSI.HOSP.AlaAllegro.Infrastructure.Services;
using LSI.HOSP.AlaAllegro.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LSI.HOSP.AlaAllegro.Domain.Entities.Auctions;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LSI.HOSP.AlaAllegro.Application.PurchaseOffers.Commands
{
    public class AddPurchaseOfferCommand : IRequest<Unit>
    {
        public string auctionId { get; set; }

        public string price { get; set; }

    }

    public class AddPurchaseOfferCommandHandler : IRequestHandler<AddPurchaseOfferCommand, Unit>
    {
        private readonly IRepository<PurchaseOffer> repository;
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;


        public AddPurchaseOfferCommandHandler(IRepository<PurchaseOffer> repository,
                                        AppDbContext appDbContext,
                                        ICurrentUserService currentUserService)
        {
            this.repository = repository;
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(AddPurchaseOfferCommand request, CancellationToken cancellationToken)
        {           
            var lastPurchaseOffer = await repository
                .GetQueryable(po => po.AuctionId.ToString() == request.auctionId)
                .OrderByDescending(po => po.LastModifiedDate)
                .FirstOrDefaultAsync(cancellationToken);           

            var currentUserId = (int)_currentUserService.GetUserId;

            if (lastPurchaseOffer is null)
            {
                var purchaseOffer = new PurchaseOffer
                {
                    UserId = currentUserId,
                    AuctionId = Guid.Parse(request.auctionId),
                    Price = Convert.ToDecimal(request.price)
                };

                await repository.AddAsync(purchaseOffer, cancellationToken);                
           }
           else if (Convert.ToDecimal(request.price) > lastPurchaseOffer.Price)
           {                
                var lastCurrentUserPurchaseOffer = await repository
                    .GetQueryable(po => po.AuctionId.ToString() == request.auctionId && po.UserId == currentUserId)
                    .OrderByDescending(po => po.LastModifiedDate)
                    .FirstOrDefaultAsync(cancellationToken);
                
                if (lastCurrentUserPurchaseOffer is null) 
                {
                    var purchaseOffer = new PurchaseOffer
                    {
                        UserId = currentUserId,
                        AuctionId = Guid.Parse(request.auctionId),
                        Price = Convert.ToDecimal(request.price)
                    };

                    await repository.AddAsync(purchaseOffer, cancellationToken);
                }
                {
                    lastCurrentUserPurchaseOffer.Price = Convert.ToDecimal(request.price);
                    await repository.UpdateAsync(lastCurrentUserPurchaseOffer, cancellationToken);
                }                
           }           
           else
           {
                throw new Exception("New price must by bitter then last");
           }                        

            return Unit.Value;
        }
    }
}
