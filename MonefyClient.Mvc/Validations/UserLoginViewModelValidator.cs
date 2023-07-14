using FluentValidation;
using MonefyClient.ViewModels.InputViewModels;

namespace MonefyClient.Mvc.Validations
{
    public class UserLoginViewModelValidator : AbstractValidator<InputUserLoginViewModel>
    {
        public UserLoginViewModelValidator() 
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("*Required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Not Valid").NotEmpty().WithMessage("*Required");
        }
    }
}
