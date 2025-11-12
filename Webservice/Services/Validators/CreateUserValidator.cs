using Contracts.DTO;
using FluentValidation;

namespace Webservice.Services.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserValidator()
        {
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
            RuleFor(user => user.Password).NotEmpty().MaximumLength(16).MinimumLength(6);
        }

    }
}
