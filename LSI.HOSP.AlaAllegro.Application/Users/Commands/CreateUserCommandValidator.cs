using FluentValidation;

namespace LSI.HOSP.AlaAllegro.Application.Users.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() 
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);
        }
    }
       
}
