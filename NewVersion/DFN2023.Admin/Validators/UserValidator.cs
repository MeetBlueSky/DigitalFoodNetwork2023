using FluentValidation;
using DFN2023.Entities.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFN2023.Admin.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adı Boş geçilmez");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Boş geçilmez");
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim Boş geçilmez");
        }
    }
}
