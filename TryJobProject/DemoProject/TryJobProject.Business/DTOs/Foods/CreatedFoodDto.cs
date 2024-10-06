using Core.Entites.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.Business.DTOs.Foods
{
    public class CreatedFoodDto : IDto
    {
        public string Name { get; set; }
        public float UnitPrice { get; set; }
        public string CategoryName { get; set; }

    }
}
