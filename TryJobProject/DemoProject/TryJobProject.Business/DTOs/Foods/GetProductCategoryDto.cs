using Core.Entites.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.Business.DTOs.Foods
{
    public class GetProductCategoryDto : IDto
    {
        public string FoodName { get; set; }
        public float Price { get; set; }
        public string CategoryName { get; set; }
    }
}
