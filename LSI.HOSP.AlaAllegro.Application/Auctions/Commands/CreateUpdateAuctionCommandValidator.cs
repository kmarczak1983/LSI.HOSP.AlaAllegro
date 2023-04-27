using FluentValidation;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using System;

namespace LSI.HOSP.AlaAllegro.Application.Auctions.Commands
{
    public class CreateUpdateAuctionCommandCommandValidator : AbstractValidator<CreateUpdateAuctionCommand>
    {
        public CreateUpdateAuctionCommandCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty();
            RuleFor(x => x.Body)
                .NotEmpty();
            

            RuleFor(x => x.InitialPrice)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    if (!Decimal.TryParse(value, out decimal price) || price < 0)
                    {                     
                        context.AddFailure($"must be greater than or equal to '0'");
                    }                    
                });            
        }
    }
}