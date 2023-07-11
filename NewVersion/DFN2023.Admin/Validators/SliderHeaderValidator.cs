using FluentValidation;
using DFN2023.Entities.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Admin.Validators
{
    public class SliderHeaderValidator : AbstractValidator<SliderHeader>
    {
        public SliderHeaderValidator()
        {
            RuleFor(x => x.SliderName).NotEmpty().WithMessage("İsim Boş Geçilemez");
        }
    }
}
