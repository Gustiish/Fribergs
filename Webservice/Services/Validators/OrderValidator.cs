using Contracts.DTO;
using FluentValidation;

namespace Webservice.Services.Validators
{
    public class OrderValidator : AbstractValidator<OrderDTO>
    {
        public OrderValidator()
        {
            RuleFor(o => o.Price).NotEmpty();
            RuleFor(o => o.Start).NotEmpty();
            RuleFor(o => o.End).NotEmpty();
            RuleFor(o => o.CarId).NotEmpty();
            RuleFor(o => o.Car).NotEmpty();
            RuleFor(o => o.Id).NotEmpty();

        }
    }
}
