using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.DataAccess.Abstract
{
    public interface ICustomerDal : IAsyncRepository<Customer>,IRepository<Customer>
    {
    }
}
