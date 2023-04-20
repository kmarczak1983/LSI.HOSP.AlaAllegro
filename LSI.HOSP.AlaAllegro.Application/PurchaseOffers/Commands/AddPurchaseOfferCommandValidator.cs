using FluentValidation;
using LSI.HOSP.AlaAllegro.Application.PurchaseOffers.Commands;
using LSI.HOSP.AlaAllegro.Domain.Entities.Auctions;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Application.PurchaseOffers.Commands
{
    public class AddPurchaseOfferCommandValidator: AbstractValidator<AddPurchaseOfferCommand>
    {
        private readonly IRepository<PurchaseOffer> _repositoryPurchaseOffer;
        private readonly IRepository<Auction> _repositoryAuction;


        public AddPurchaseOfferCommandValidator(IRepository<PurchaseOffer> repositoryPurchaseOffer, IRepository<Auction> repositoryAuction)
        {
            _repositoryPurchaseOffer = repositoryPurchaseOffer;
            _repositoryAuction = repositoryAuction;

            RuleFor(x => x.auctionId)
                .NotEmpty();

            RuleFor(x => x.price)
                .NotEmpty();

            RuleFor(x => x)                
                .MustAsync(IsHigherOrEqualOfferExist).WithMessage("Higher or equal purchase offer exist")
                .MustAsync(IsAuctionStartPriceHigherOrEqual).WithMessage("Auction start price is higher or equal than current purchase offer");
        }

        private async Task<bool> IsHigherOrEqualOfferExist(AddPurchaseOfferCommand c, CancellationToken cancellationToken)
        {
            return !await _repositoryPurchaseOffer.AnyAsync(po => po.AuctionId.ToString() == c.auctionId && po.Price >= Convert.ToDecimal(c.price));
        }

        private async Task<bool> IsAuctionStartPriceHigherOrEqual(AddPurchaseOfferCommand c, CancellationToken cancellationToken)
        {
            return !await _repositoryAuction.AnyAsync(po => po.Id.ToString() == c.auctionId && po.StartPrice >= Convert.ToDecimal(c.price));
        }
    }
}