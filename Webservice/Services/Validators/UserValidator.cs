using Contracts.DTO;
using FluentValidation;

namespace Webservice.Services.Validators
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            //RuleFor(user => user.)
        }
    }
}
