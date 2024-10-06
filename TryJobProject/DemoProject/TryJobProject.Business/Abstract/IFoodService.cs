using Core.Business.Request;
using Core.Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.Foods;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.Abstract
{
    public interface IFoodService
    {
        Task<GetListResponse<GetFoodDto>> GetAllAsync(PageRequest pageRequest);
        Task<GetFoodDto> GetByIdAsync(string id);
        Task<GetListResponse<GetFoodDto>> GetByCategoryIdAsync(string id, PageRequest pageRequest);
        Task<List<GetProductCategoryDto>> GetFoodDetailAsync();
        Task<List<GetFoodDto>> GetByPriceAsync(float min, float max);
        Task<CreatedFoodDto> AddAsync(CreatedFoodDto foodDto);
        Task AddRangeAsync(List<CreatedFoodDto> foodDtos);
        Task RemoveAsync(string id);

    }
}
