using FluentValidation;
using DFN2023.Entities.EF;

namespace DFN2023.Admin.Validators
{
    public class ProductBaseValidator : AbstractValidator<ProductBase>
    {
        public ProductBaseValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Adı Boş Geçilemez");
            RuleFor(x => x.RowNum).NotEmpty().WithMessage("Sıra Numarası Boş Geçilemez");
            //RuleFor(x => x.Surname).NotEmpty().WithMessage("Kullanıcının Soyadı Boş Geçilemez");
            //RuleFor(x => x.Password).NotEmpty().WithMessage("Kullanıcının Şifresi Boş Geçilemez");
            //RuleFor(x => x.Email).NotEmpty().WithMessage("Kullanıcının E-postası Boş Geçilemez");
        }
    }

}
