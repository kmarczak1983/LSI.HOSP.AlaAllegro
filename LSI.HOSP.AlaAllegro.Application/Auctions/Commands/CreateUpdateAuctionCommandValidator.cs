using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Application.Auctions.Commands
{
    public class CreateUpdateAuctionCommandValidator: AbstractValidator<CreateUpdateAuctionCommand>
    {
        public CreateUpdateAuctionCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Body).NotEmpty();
            RuleFor(x => x.InitialPrice).NotEmpty();
        }
    }
}
