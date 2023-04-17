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
            var lastPurchaseOffer = await _appDbContext.PurchaseOffers
                .OrderByDescending(po => po.CreatedDate)
                .GetFirstOrDefaultAsync(po => po.AuctionId.ToString() == request.auctionId, cancellationToken);
            //repository.ge
           // if (lastPurchaseOffer.Price == null) { }


            return Unit.Value;
        }
    }
}
