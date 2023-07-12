using FluentValidation;
using MonefyClient.ViewModels;

namespace MonefyClient.Mvc.Validations
{
    public class AccountViewModelValidator : AbstractValidator<AccountViewModel>
    {
        public AccountViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("*Required");
            RuleFor(x => x.Currency).NotEmpty().WithMessage("*Required");
        }
    }
}
