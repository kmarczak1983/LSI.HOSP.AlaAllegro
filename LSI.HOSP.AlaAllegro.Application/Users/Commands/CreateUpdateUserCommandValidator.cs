using FluentValidation;
using LSI.HOSP.AlaAllegro.Application.PurchaseOffers.Commands;
using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using LSI.HOSP.AlaAllegro.Infrastructure.Services;

namespace LSI.HOSP.AlaAllegro.Application.Users.Commands
{
    public class CreateUpdateUserCommandValidator : AbstractValidator<CreateUpdateUserCommand>
    {
        public CreateUpdateUserCommandValidator(IRepository<User> repository, ICurrentUserService currentUserService) 
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Custom((value, context) =>
                    {
                        var existingUser = repository.GetFirstOrDefaultAsync(u => u.Email == value && u.Id != currentUserService.GetUserId).Result;
                        if (existingUser != null)
                        {
                            context.AddFailure($"{value} is not unique e-mail for user");
                        }

                    });                

            RuleFor(x => x.Password).MinimumLength(6);
        }

    }
       
}
