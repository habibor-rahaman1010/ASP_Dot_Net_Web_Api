using CountryApi.Model;
using FluentValidation;

namespace CountryApi.ModelValidators
{
    public class CountryUpdateValidator : AbstractValidator<UpdateDto>
    {
        public CountryUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Please provide a name. It cannot be empty or null.")
                .MaximumLength(25).WithMessage("The name can have maximum of 25 characters.");

            RuleFor(x => x.Capital)
                .NotEmpty().WithMessage("Please provide the capital. It cannot be empty or null.")
                .Length(0, 25).WithMessage("The capital can have maximum of 25 characters.");

            RuleFor(x => x.Population)
                .GreaterThan(0).WithMessage("Population must be greater than 0.");

            RuleFor(x => x.Area)
                .GreaterThan(0).WithMessage("Area must be greater than 0.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required. Please provide a value.")
                .MaximumLength(25).WithMessage("The currency can have maximum of 25 characters.");

            RuleFor(x => x.IsFreedom)
                .NotEmpty().WithMessage("IsFreedom must not be empty or null");
        }
    }
}
