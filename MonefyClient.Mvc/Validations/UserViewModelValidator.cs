using FluentValidation;
using MonefyClient.ViewModels.InputViewModels;

namespace MonefyClient.Mvc.Validations
{
    public class UserViewModelValidator : AbstractValidator<InputUserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("*Required").Length(6, 10).WithMessage("*Length must be beetwen 6 and 10 characters");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Not Valid").NotEmpty().WithMessage("*Required");
        }
    }
}
