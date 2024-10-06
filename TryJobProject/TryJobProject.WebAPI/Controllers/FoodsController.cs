using Core.Business.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TryJobProject.Business.Abstract;
using TryJobProject.Business.DTOs.Foods;

namespace TryJobProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly IFoodService _foodService;

        public FoodsController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CreatedFoodDto createdFoodDto)
        {
            var result = await _foodService.AddAsync(createdFoodDto);
            return Ok("Başarılı bir şekilde eklendi");
        }

        [HttpPost("addrange")]
        public async Task<IActionResult> AddRange(List<CreatedFoodDto> createdFoodDtos)
        {
            await _foodService.AddRangeAsync(createdFoodDtos);
            return Ok();
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            var result = await _foodService.GetAllAsync(pageRequest);
            return Ok(result);
        }
    }
}
