using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.Categories;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.ValidationRules.FluentValidation
{
    public class CategoryValidator:AbstractValidator<CreatedCategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull()
                    .WithMessage("Lütfen kategori ismini boş geçmeyiniz.");
        }
    }
}
