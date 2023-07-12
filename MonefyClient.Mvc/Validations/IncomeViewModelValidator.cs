using FluentValidation;
using MonefyClient.ViewModels;

namespace MonefyClient.Mvc.Validations
{
    public class IncomeViewModelValidator : AbstractValidator<IncomeViewModel>
    {
        public IncomeViewModelValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("*Required");
            RuleFor(x => x.Value).NotEmpty().WithMessage("*Required");
            RuleFor(x => x.Date).NotEmpty().WithMessage("*Required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("*Required");
        }
    }
}
