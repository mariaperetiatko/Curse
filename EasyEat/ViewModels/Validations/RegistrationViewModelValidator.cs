﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;


namespace EasyEat.ViewModels.Validations
{
    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationViewModelValidator()
        {
            RuleFor(vm => vm.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(vm => vm.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(vm => vm.FirstName).NotEmpty().WithMessage("FirstName cannot be empty");
            RuleFor(vm => vm.LastName).NotEmpty().WithMessage("LastName cannot be empty");
            RuleFor(vm => vm.Phone).NotEmpty().WithMessage("Phone cannot be empty");
        }
    }
}
