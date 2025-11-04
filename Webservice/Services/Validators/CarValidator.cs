using Contracts.DTO;
using FluentValidation;

namespace Webservice.Services.Validators
{
    public class CarValidator : AbstractValidator<CarDTO>
    {
        public CarValidator()
        {
            RuleFor(car => car.Id).NotEmpty();
            RuleFor(car => car.Price).NotEmpty();
            RuleFor(car => car.Model).NotEmpty();
            RuleFor(car => car.Brand).NotEmpty();
            RuleFor(car => car.URL).NotEmpty();

        }
    }
}
