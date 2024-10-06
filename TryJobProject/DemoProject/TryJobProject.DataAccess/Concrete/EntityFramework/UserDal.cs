using Core.DataAccess.EntityFramework;
using Core.Entites.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.DataAccess.Abstract;
using TryJobProject.DataAccess.Concrete.Context;

namespace TryJobProject.DataAccess.Concrete.EntityFramework
{
    public class UserDal : EfRepositoryBase<User, FoodWebsiteDbContext>, IUserDal
    {
        public UserDal(FoodWebsiteDbContext context) : base(context)
        {
        }

        public IQueryable<OperationClaim> GetClaims(User user)
        {
            var result = Table.Where(u => u.Id == user.Id).SelectMany(u => u.OperationClaims);
            return result;
        }
    }
}
