using Core.DataAccess.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.Abstract
{
    public interface ICompanyService
    {
        Task AddAsync(Company company);
        Task<Paginate<Company>> GetAllAsync();
        Task DeleteAsync(string id);
    }
}
