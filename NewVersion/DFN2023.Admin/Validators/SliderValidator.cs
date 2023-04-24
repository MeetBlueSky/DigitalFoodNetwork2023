using FluentValidation;
using DFN2023.Entities.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Business.Validators
{
    public class SliderValidator : AbstractValidator<Slider>
    {
        public SliderValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim Boş Geçilemez");
            RuleFor(x => x.Image1).NotEmpty().WithMessage("Lütfen 1. Resmi Seçiniz");
            RuleFor(x => x.Image2).NotEmpty().WithMessage("Lütfen 2. Resmi Seçiniz");
            RuleFor(x => x.Link).NotEmpty().WithMessage("Links Boş Geçilemez");
            RuleFor(x => x.Target).NotEmpty().WithMessage("Hedef Boş Geçilemez");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Tip Boş Geçilemez");
            //RuleFor(x => x.RowNum).NotEmpty().WithMessage("Satır Sayısı Boş Geçilemez");
            RuleFor(x => x.LangId).NotEmpty().WithMessage("Lütfen Dil Seçiniz");
        }
    }
}
