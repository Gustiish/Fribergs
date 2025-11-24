using Contracts.DTO;
using FluentValidation;

namespace Webservice.Services.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDTO>
    {
        public CreateOrderValidator()
        {
            RuleFor(o => o.Price).NotEmpty();
            RuleFor(o => o.Start).NotEmpty();
            RuleFor(o => o.End).NotEmpty();
            RuleFor(o => o.CarId).NotEmpty();
            RuleFor(o => o.UserId).NotEmpty();

        }
    }
}
