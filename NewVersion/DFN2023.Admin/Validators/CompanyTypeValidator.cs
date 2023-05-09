using FluentValidation;
using DFN2023.Entities.EF;

namespace DFN2023.Admin.Validators
{
    public class CompanyTypeValidator : AbstractValidator<CompanyType>
    {
        public CompanyTypeValidator()
        {
            RuleFor(x => x.TypeName).NotEmpty().WithMessage("Tip Adı Boş Geçilemez");
        }
    }

}
