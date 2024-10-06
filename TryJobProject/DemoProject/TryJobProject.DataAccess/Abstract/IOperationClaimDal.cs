using Core.DataAccess;
using Core.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.DataAccess.Abstract
{
    public interface IOperationClaimDal : IAsyncRepository<OperationClaim>,IRepository<OperationClaim>
    {
    }
}
