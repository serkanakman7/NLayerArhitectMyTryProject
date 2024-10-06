using Core.CrossCuttingConcerns.Exception.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.Abstract;
using TryJobProject.Business.Constants;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.BusinessRules.Foods
{
    public class FoodBusinessRules
    {
        private readonly ICategoryService _categoryService;

        public FoodBusinessRules(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task DontNewCategory(Food food, string categoryName)
        {
            Category category = await _categoryService.GetByNameAsync(categoryName);

            if (category.Name == categoryName)
            {
                food.Category = category;
            }
            else
            {
                throw new BusinessException(BusinessMessage.NotCategoryName);
            }

        }
    }
}
