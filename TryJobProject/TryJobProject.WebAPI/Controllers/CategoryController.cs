using Core.Business.Request;
using Core.Business.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TryJobProject.Business.Abstract;
using TryJobProject.Business.DTOs.Categories;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CreatedCategoryDto createdCategoryDto)
        {
            await _categoryService.AddAsync(createdCategoryDto);
            return Ok("Ürün eklendi");
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            GetListResponse<GetListCategoryDto> response = await _categoryService.GetAllAsync(pageRequest);
            return Ok(response);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove(string id)
        {
            await _categoryService.RemoveAsync(id);
            return Ok("başarılı bir şekide silindi");
        }
    }
}
