using FluentValidation;
using DFN2023.Entities.DTO;

namespace DFN2023.Admin.Validators
{
    public class UserValidators : AbstractValidator<UserDTO>
    {
        public UserValidators()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim Boş Geçilemez");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyadı Boş Geçilemez");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Mail Boş Geçilemez");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Boş Geçilemez");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Şifre Uzunluğu 6 kelimeden uzun olmalıdır");
        }
    }
}
