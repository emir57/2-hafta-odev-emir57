using _3_hafta.Dto.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_hafta.Business.Validation.FluentValidation
{
    public class CountryValidator : AbstractValidator<CountryDto>
    {
        public CountryValidator()
        {
            RuleFor(c => c.CountryName)
                .Null()
                .Empty()
                .WithMessage("Şehir adı boş olamaz");
            RuleFor(c => c.CountryName)
                .MaximumLength(50)
                .WithMessage("Şehir adı maksimum 50 karakter olabilir");

            RuleFor(c => c.Continent)
                .Null()
                .Empty()
                .WithMessage("Kıta boş olamaz");
            RuleFor(c => c.Continent)
                .MaximumLength(50)
                .WithMessage("Kıta maksimum 50 karakter olabilir");

            RuleFor(c => c.Currency)
                .Null()
                .Empty()
                .WithMessage("Para birimi boş olamaz");
            RuleFor(c => c.Currency)
                .MaximumLength(3)
                .WithMessage("Para birimi maksimum 3 karakter olabilir");

        }
    }
}
