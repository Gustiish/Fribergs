using Contracts.DTO;
using FluentValidation;

namespace Webservice.Services.Validators
{
    public class CreateCarValidator : AbstractValidator<CreateCarDTO>
    {
        public CreateCarValidator()
        {
            RuleFor(car => car.Price).NotEmpty();
            RuleFor(car => car.Model).NotEmpty();
            RuleFor(car => car.Brand).NotEmpty();
            RuleFor(car => car.URL).NotEmpty();

        }
    }
}
