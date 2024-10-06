using Core.DataAccess.EntityFramework;
using Core.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.DataAccess.Abstract;
using TryJobProject.DataAccess.Concrete.Context;

namespace TryJobProject.DataAccess.Concrete.EntityFramework
{
    public class OperationClaimDal : EfRepositoryBase<OperationClaim, FoodWebsiteDbContext>, IOperationClaimDal
    {
        public OperationClaimDal(FoodWebsiteDbContext context) : base(context)
        {
        }
    }
}
