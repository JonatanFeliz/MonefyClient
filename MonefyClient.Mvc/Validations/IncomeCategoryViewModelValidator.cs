using FluentValidation;
using MonefyClient.ViewModels.InputViewModels;

namespace MonefyClient.Mvc.Validations
{
    public class IncomeCategoryViewModelValidator : AbstractValidator<InputIncomeCategoryViewModel>
    {
        public IncomeCategoryViewModelValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("*Required");
        }
    }
}
