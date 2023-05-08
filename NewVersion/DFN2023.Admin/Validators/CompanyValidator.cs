using FluentValidation;
using DFN2023.Entities.EF;

namespace DFN2023.Admin.Validators
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.BrandName).NotEmpty().WithMessage("Marka Adı Boş Geçilemez");
            RuleFor(x => x.OfficialName).NotEmpty().WithMessage("Resmi Ad Boş Geçilemez");
            RuleFor(x => x.ShortDescription).NotEmpty().WithMessage("Kısa Açıklama Boş Geçilemez");
            RuleFor(x => x.DetailDescription).NotEmpty().WithMessage("Detaylı Açıklama Boş Geçilemez");
            RuleFor(x => x.TaxOffice).NotEmpty().WithMessage("Vergi Dairesi Boş Geçilemez");
            RuleFor(x => x.TaxNo).NotEmpty().WithMessage("Vergi Numarası Boş Geçilemez");
            RuleFor(x => x.CompanyTypeId).NotEmpty().WithMessage("Resmi Adres Boş Geçilemez");
            RuleFor(x => x.MapAddress).NotEmpty().WithMessage("Harita Adresi Boş Geçilemez");
            RuleFor(x => x.OfficialAddress).NotEmpty().WithMessage("Resmi Adres Boş Geçilemez");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Şirket E-postası Boş Geçilemez");
            RuleFor(x => x.Mobile).NotEmpty().WithMessage("Şirket Cep Telefonu Boş Geçilemez");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Şirket Telefonu Boş Geçilemez");
        }
    }

}
