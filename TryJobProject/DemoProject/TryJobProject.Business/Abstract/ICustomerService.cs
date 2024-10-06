using Core.Business.Request;
using Core.Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.Customers;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.Abstract
{
    public interface ICustomerService
    {
        Task AddAsync(CustomerDto customer);
        Task<GetListResponse<CustomerDto>> GetAllAsync(PageRequest pageRequest);
        Task DeleteAsync(string id);
    }
}
