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

            RuleFor(x => x.AuctionId)
                .NotEmpty();

            RuleFor(x => x.Price)
                .NotEmpty();

            RuleFor(x => x)                
                .MustAsync(IsHigherOrEqualOfferExist).WithMessage("Higher or equal purchase offer exist")
                .MustAsync(IsAuctionStartPriceHigherOrEqual).WithMessage("Auction start price is higher or equal than current purchase offer");
        }

        private async Task<bool> IsHigherOrEqualOfferExist(AddPurchaseOfferCommand c, CancellationToken cancellationToken)
        {
            return !await _repositoryPurchaseOffer.AnyAsync(po => po.AuctionId.ToString() == c.AuctionId && po.Price >= Convert.ToDecimal(c.Price));
        }

        private async Task<bool> IsAuctionStartPriceHigherOrEqual(AddPurchaseOfferCommand c, CancellationToken cancellationToken)
        {
            return !await _repositoryAuction.AnyAsync(po => po.Id.ToString() == c.AuctionId && po.StartPrice >= Convert.ToDecimal(c.Price));
        }
    }
}