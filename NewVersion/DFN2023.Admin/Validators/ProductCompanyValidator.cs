using FluentValidation;
using DFN2023.Entities.EF;

namespace DFN2023.Admin.Validators
{
    public class ProductCompanyValidator : AbstractValidator<ProductCompany>
    {
        public ProductCompanyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Adı Boş Geçilemez");
            RuleFor(x => x.CompanyId).NotEmpty().WithMessage("Şirket Boş Geçilemez");
            RuleFor(x => x.ProductBaseId).NotEmpty().WithMessage("Temel Ürün Boş Geçilemez");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori Boş Geçilemez");
            //RuleFor(x => x.Email).NotEmpty().WithMessage("Kullanıcının E-postası Boş Geçilemez");
        }
    }

}
