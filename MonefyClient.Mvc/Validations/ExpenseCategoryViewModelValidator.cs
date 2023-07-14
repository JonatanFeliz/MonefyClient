using FluentValidation;
using MonefyClient.ViewModels.InputViewModels;

namespace MonefyClient.Mvc.Validations
{
    public class ExpenseCategoryViewModelValidator : AbstractValidator<InputExpenseCategoryViewModel>
    {
        public ExpenseCategoryViewModelValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("*Required");
        }
    }
}
