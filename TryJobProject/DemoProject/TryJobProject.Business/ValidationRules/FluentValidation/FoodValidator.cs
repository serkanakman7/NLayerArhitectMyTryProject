using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.Foods;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.ValidationRules.FluentValidation
{
    public class FoodValidator : AbstractValidator<CreatedFoodDto>
    {
        public FoodValidator()
        {
            RuleFor(f => f.Name).NotEmpty().NotNull()
                    .WithMessage("Lütfen yemek ismini boş geçmeyiniz.");
            RuleFor(f => f.UnitPrice).NotEmpty().NotNull()
                    .WithMessage("Lütfen yemek fiyatını giriniz.")
                .GreaterThanOrEqualTo(1)
                    .WithMessage("Lütfen fiyatı 0 dan büyük bir değer giriniz");
        }
    }
}
