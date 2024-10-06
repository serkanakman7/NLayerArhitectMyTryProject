using Core.Entites.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TryJobProject.Business.Abstract;

namespace TryJobProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = await _authService.LoginAsync(userForLoginDto);
            if(userToLogin == null)
            {
                return BadRequest("Kullanıcı bulunamadı");
            }

            var result = await _authService.CreateTokenAsync(userToLogin);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = await _authService.UserExistsAsync(userForRegisterDto.Email);
            if(!userExists)
            {
                return BadRequest("Bu kullanıcı zaten var");
            }

            var userRegister = await _authService.RegisterAsync(userForRegisterDto);
            var result = await _authService.CreateTokenAsync(userRegister);

            if (result!=null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
