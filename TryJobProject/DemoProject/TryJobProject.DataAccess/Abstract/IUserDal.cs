using Core.DataAccess;
using Core.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.DataAccess.Abstract
{
    public interface IUserDal : IRepository<User>,IAsyncRepository<User>
    {
        IQueryable<OperationClaim> GetClaims(User user);
    }
}
