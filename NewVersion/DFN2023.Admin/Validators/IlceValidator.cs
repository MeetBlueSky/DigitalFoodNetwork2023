﻿using FluentValidation;
using DFN2023.Entities.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Admin.Validators
{
    public class IlceValidator : AbstractValidator<County>
    {
        public IlceValidator()
        {

            //RuleFor(x => x.Desc).NotEmpty().WithMessage("Açıklama Boş Geçilemez");
            //RuleFor(x => x.RowNum).NotEmpty().WithMessage("Satır Sayısı Boş Geçilemez");


        }
    }
}
