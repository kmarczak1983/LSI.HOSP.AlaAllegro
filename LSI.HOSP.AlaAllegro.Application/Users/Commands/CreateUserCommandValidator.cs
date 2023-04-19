using FluentValidation;
using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;

namespace LSI.HOSP.AlaAllegro.Application.Users.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IRepository<User> repository) 
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Custom((value, context) =>
                 {
                     var existingUser = repository.GetFirstOrDefaultAsync(u => u.Email == value).Result;
                     if (existingUser != null)
                     {
                         context.AddFailure($"{value} is not unique e-mail for user");
                     }
                 });

            RuleFor(x => x.Password).MinimumLength(6);
        }
    }
       
}
