using FluentValidation;
using MonefyClient.ViewModels.InputViewModels;

namespace MonefyClient.Mvc.Validations
{
    public class ExpenseViewModelValidator : AbstractValidator<InputExpenseViewModel>
    {
        public ExpenseViewModelValidator() 
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("*Required");
            RuleFor(x => x.Value).NotEmpty().WithMessage("*Required");
            RuleFor(x => x.Date).NotEmpty().WithMessage("*Required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("*Required");
        }
    }
}
