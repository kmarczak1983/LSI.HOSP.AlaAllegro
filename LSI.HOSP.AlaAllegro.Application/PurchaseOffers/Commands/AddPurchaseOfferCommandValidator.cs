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
        public AddPurchaseOfferCommandValidator(/*IRepository<PurchaseOffer> repositoryPurchaseOffer, IRepository<Auction> repositoryAuction*/)
        {
            RuleFor(x => x.price)
                .NotEmpty();
                //.c
                /*
                .Custom((value, context) =>
                {
                    var purchaseOffer = repositoryPurchaseOffer.GetFirstOrDefaultAsync(po => po.id == value);
                    if (existingUser != null)
                    {
                        context.AddFailure($"{value} is not unique e-mail for user");
                    }
                });            
                */
        }
        /*
        private async Task<bool> IsNameAndAuthorAlreadyExist
    (AddPurchaseOfferCommand e, CancellationToken cancellationToken)
        {
            var check = await _postRepository.
                IsNameAndAuthorAlreadyExist(e.Title, e.Author);
            return !check;
        }*/
    }
}
