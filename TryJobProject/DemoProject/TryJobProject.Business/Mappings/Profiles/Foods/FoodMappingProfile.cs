using AutoMapper;
using Core.Business.Response;
using Core.DataAccess.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.Foods;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.Mappings.Profiles.Foods
{
    public class FoodMappingProfile : Profile
    {
        public FoodMappingProfile()
        {
            CreateMap<Food, CreatedFoodDto>()
                .ForMember(cft => cft.UnitPrice, f => f.MapFrom(x => x.Price))
                .ForMember(cft=>cft.CategoryName,f=>f.MapFrom(x=>x.Category.Name)).ReverseMap();

            CreateMap<Food, GetFoodDto>().ReverseMap();
            CreateMap<Food, GetProductCategoryDto>()
                 .ForMember(dto => dto.FoodName, x => x.MapFrom(f => f.Name))
                 .ForMember(dto => dto.CategoryName, x => x.MapFrom(f => f.Category.Name)).ReverseMap();

            CreateMap<Paginate<Food>, GetListResponse<GetFoodDto>>();
        }
    }
}
