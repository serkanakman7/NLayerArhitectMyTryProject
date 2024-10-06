using AutoMapper;
using Core.Aspects.Autofac.Validation;
using Core.Entites.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.Abstract;
using TryJobProject.Business.DTOs.Users;
using TryJobProject.Business.ValidationRules.FluentValidation;
using TryJobProject.DataAccess.Abstract;

namespace TryJobProject.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;

        public UserManager(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(UserValidator))]
        public async Task AddAsync(User user)
        {
            await _userDal.AddAsync(user);
            await _userDal.SaveAsync();
        }

        public async Task<List<GetUserDto>> GetAllAsync()
        {
            List<User> users = await _userDal.GetAll().ToListAsync();
            List<GetUserDto> getUserDtos = _mapper.Map<List<GetUserDto>>(users);
            return getUserDtos;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var c = email.Reverse().ToArray();
            var cs = new string(c);
            return await _userDal.GetAsync(u => u.Email == email);
        }

        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            return await _userDal.GetClaims(user).ToListAsync();
        }
    }
}
