using AutoMapper;
using Core.Business.Response;
using Core.DataAccess.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.Categories;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.Mappings.Profiles.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CreatedCategoryDto, Category>().ReverseMap();
            CreateMap<GetCategoryDto, Category>().ReverseMap();

            CreateMap<GetListCategoryDto, Category>().ReverseMap();
            CreateMap<Paginate<Category>,GetListResponse<GetListCategoryDto>>().ReverseMap();
        }
    }
}
