using FluentValidation;
using DFN2023.Entities.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Business.Validators
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {

            //RuleFor(x => x.Desc).NotEmpty().WithMessage("Açıklama Boş Geçilemez");
            //RuleFor(x => x.RowNum).NotEmpty().WithMessage("Satır Sayısı Boş Geçilemez");


        }
    }
}
