using Core.Entites.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.Business.DTOs.Foods
{
    public class GetFoodDto : GetListDto
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
