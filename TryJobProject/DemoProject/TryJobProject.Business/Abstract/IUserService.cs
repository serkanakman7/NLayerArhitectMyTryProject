using Core.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.Users;

namespace TryJobProject.Business.Abstract
{
    public interface IUserService
    {
        Task<List<OperationClaim>> GetClaimsAsync(User user);
        Task AddAsync(User user);
        Task<User> GetByEmailAsync(string email);
        Task<List<GetUserDto>> GetAllAsync();
    }
}
