using Core.Business.Request;
using Core.Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.Categories;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.Abstract
{
    public interface ICategoryService
    {
        Task<GetListResponse<GetListCategoryDto>> GetAllAsync(PageRequest pageRequest);
        Task<Category> GetByNameAsync(string name);
        Task<Category> GetByIdAsync(string id);
        Task AddAsync(CreatedCategoryDto createdCategoryDto);
        Task RemoveAsync(string id);
    }
}
