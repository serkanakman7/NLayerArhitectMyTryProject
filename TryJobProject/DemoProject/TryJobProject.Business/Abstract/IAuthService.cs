using Core.Entites.Concrete;
using Core.Entites.Dtos;
using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.Business.Abstract
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(UserForRegisterDto userForRegisterDto);
        Task<User> LoginAsync(UserForLoginDto userForLoginDto);
        Task<bool> UserExistsAsync(string email);
        Task<AccessToken> CreateTokenAsync(User user);
    }
}
