using FluentValidation;
using MonefyClient.ViewModels;

namespace MonefyClient.Mvc.Validations
{
    public class UserLoginViewModelValidator : AbstractValidator<UserLoginViewModel>
    {
        public UserLoginViewModelValidator() 
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("*Required").Length(6, 10).WithMessage("*Length must be beetwen 6 and 10 characters");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Not Valid").NotEmpty().WithMessage("*Required");
        }
    }
}
