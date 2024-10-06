using Core.Entites.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TryJobProject.Business.Abstract;
using TryJobProject.Business.DTOs.OperationClaims;

namespace TryJobProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationCliamsController : ControllerBase
    {
        private readonly IOperationClaimService _operationClaimService;

        public OperationCliamsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(OperationClaimDto operationClaimDto)
        {
            await _operationClaimService.AddAsync(operationClaimDto);
            return Ok("başarılı bir şekilde eklendi");
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _operationClaimService.GetAllAsync());
        }
    }
}
