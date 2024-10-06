using Core.Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.Addresses;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.Abstract
{
    public interface IAddressService
    {
        Task AddAsync(Address address);
        Task<GetListResponse<GetListAddressDto>> GetAllAsync();
        Task DeleteAsync(string id);
    }
}
