using Core.DataAccess.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.Abstract;
using TryJobProject.DataAccess.Abstract;
using TryJobProject.DataAccess.Concrete.EntityFramework;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyDal _companyDal;

        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }

        public async Task AddAsync(Company company)
        {
            await _companyDal.AddAsync(company);
            await _companyDal.SaveAsync();
        }

        public async Task DeleteAsync(string id)
        {
            Company company = await _companyDal.GetAsync(c => c.Id == Guid.Parse(id));
            await _companyDal.RemoveAsync(company);
            await _companyDal.SaveAsync();
        }

        public async Task<Paginate<Company>> GetAllAsync()
        {
            return await _companyDal.GetAllAsync();
        }
    }
}
