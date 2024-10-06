using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Exception.Types;
using Core.Entites.Concrete;
using Core.Entites.Dtos;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.Abstract;
using TryJobProject.Business.BusinessRules.Auths;
using TryJobProject.Business.Constants;
using TryJobProject.Business.ValidationRules.FluentValidation;

namespace TryJobProject.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly AuthBusinessRules _authBusinessRules;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<AccessToken> CreateTokenAsync(User user)
        {
            var claim = await _userService.GetClaimsAsync(user);
            var result = _tokenHelper.CreateToken(user, claim);
            return result;
        }

        [ValidationAspect(typeof(AuthLoginValidator))]
        public async Task<User> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var userLogin = await _userService.GetByEmailAsync(userForLoginDto.Email);
            if (userLogin == null)
            {
                throw new AuthenticationException(AuthenticationMessage.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordhash(userForLoginDto.Password, userLogin.PasswordHash, userLogin.PasswordSalt))
            {
                throw new AuthenticationException(AuthenticationMessage.EnteredPasswordIncorrectly);
            }
            return userLogin;
        }

        public async Task<User> RegisterAsync(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            User user = new User
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            await _authBusinessRules.DontNewOperationClaim(user,userForRegisterDto.Roles);

            await _userService.AddAsync(user);
            return user;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            var result = await _userService.GetByEmailAsync(email);
            if (result != null)
            {
                return false;
            }
            return true;
        }
    }
}
