using CountryApi.Model;
using FluentValidation;

namespace CountryApi.ModelValidators
{
    public class CountryValidator : AbstractValidator<Country>
    {
        public CountryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id must not be empty.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

            RuleFor(x => x.Capital)
                .NotEmpty().WithMessage("Capital is required.")
                .Length(2, 100).WithMessage("Capital must be between 2 and 100 characters.");

            RuleFor(x => x.Population)
                .GreaterThan(0).WithMessage("Population must be greater than 0.");

            RuleFor(x => x.Area)
                .GreaterThan(0).WithMessage("Area must be greater than 0.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(1, 50).WithMessage("Currency must be between 1 and 50 characters.");
        }
    }
}
