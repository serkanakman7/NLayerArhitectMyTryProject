using Core.Business.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TryJobProject.Business.Abstract;
using TryJobProject.Business.DTOs.Customers;

namespace TryJobProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CustomerDto customerDto)
        {
            await _customerService.AddAsync(customerDto);
            return Ok("Ürün başarılı bir şekilde eklendi");
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            return Ok(await _customerService.GetAllAsync(pageRequest));
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove(string id)
        {
            await _customerService.DeleteAsync(id);
            return Ok("başarılı bir şekilde silindi.");
        }
    }
}
