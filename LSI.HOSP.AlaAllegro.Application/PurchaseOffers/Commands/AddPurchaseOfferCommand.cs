using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using LSI.HOSP.AlaAllegro.Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LSI.HOSP.AlaAllegro.Domain.Entities.Auctions;


namespace LSI.HOSP.AlaAllegro.Application.PurchaseOffers.Commands
{
    public class AddPurchaseOfferCommand : IRequest<Unit>
    {
        public string AuctionId { get; set; }

        public string Price { get; set; }

    }

    public class AddPurchaseOfferCommandHandler : IRequestHandler<AddPurchaseOfferCommand, Unit>
    {
        private readonly IRepository<PurchaseOffer> _repository;        
        private readonly ICurrentUserService _currentUserService;


        public AddPurchaseOfferCommandHandler(IRepository<PurchaseOffer> repository,
                                              ICurrentUserService currentUserService)
        {
            _repository = repository;            
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(AddPurchaseOfferCommand request, CancellationToken cancellationToken)
        {
            //if (_repositoryAuction.AnyAsync(a => a.ID)) //TODO Sprawdzić czy istnieje aukcja

            var lastPurchaseOffer = await _repository
                .GetLastModifiedOrDefaultAsync(po => po.AuctionId == Guid.Parse(request.AuctionId), cancellationToken);

            

            var currentUserId = (int)_currentUserService.GetUserId;

            if (lastPurchaseOffer is null)
            {
                var purchaseOffer = new PurchaseOffer
                {
                    UserId = currentUserId,
                    AuctionId = Guid.Parse(request.AuctionId),
                    Price = Convert.ToDecimal(request.Price)
                };


               // var validator = new AddPurchaseOfferCommandValidator(11);
               // var validatorResult = await validator.ValidateAsync(request);

               // if (!validatorResult.IsValid)
                 //   return Unit.Value; //(validatorResult);




                await _repository.AddAsync(purchaseOffer, cancellationToken);                
           }
           else
           {
                var lastCurrentUserPurchaseOffer = await _repository
                    .GetLastModifiedOrDefaultAsync(po => po.AuctionId.ToString() == request.AuctionId && po.UserId == currentUserId, cancellationToken);
                    
                if (lastCurrentUserPurchaseOffer is null) 
                {
                    var purchaseOffer = new PurchaseOffer
                    {
                        UserId = currentUserId,
                        AuctionId = Guid.Parse(request.AuctionId),
                        Price = Convert.ToDecimal(request.Price)
                    };

                    await _repository.AddAsync(purchaseOffer, cancellationToken);
                }
                else
                {
                    lastCurrentUserPurchaseOffer.Price = Convert.ToDecimal(request.Price);
                    await _repository.UpdateAsync(lastCurrentUserPurchaseOffer, cancellationToken);
                }                
           }                               

            return Unit.Value;
        }
    }
}
