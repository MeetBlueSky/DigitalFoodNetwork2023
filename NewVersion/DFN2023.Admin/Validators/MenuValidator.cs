using FluentValidation;
using DFN2023.Entities.EF;

namespace DFN2023.Admin.Validators
{
    public class MenuValidators : AbstractValidator<MenuManagement>
    {
        public MenuValidators()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim Alanı Boş Geçilemez");
        }
    }
}
