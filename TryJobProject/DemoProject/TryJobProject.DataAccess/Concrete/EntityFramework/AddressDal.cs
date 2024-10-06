using Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.DataAccess.Abstract;
using TryJobProject.DataAccess.Concrete.Context;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.DataAccess.Concrete.EntityFramework
{
    public class AddressDal : EfRepositoryBase<Address, FoodWebsiteDbContext>, IAddressDal
    {
        public AddressDal(FoodWebsiteDbContext context) : base(context)
        {
        }
    }
}
