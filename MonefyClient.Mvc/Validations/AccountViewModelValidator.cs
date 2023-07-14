﻿using FluentValidation;
using MonefyClient.ViewModels.InputViewModels;

namespace MonefyClient.Mvc.Validations
{
    public class AccountViewModelValidator : AbstractValidator<InputAccountViewModel>
    {
        public AccountViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("*Required");
            RuleFor(x => x.Currency).NotEmpty().WithMessage("*Required");
        }
    }
}
