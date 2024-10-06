﻿using Core.DataAccess.EntityFramework;
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
    public class OrderDal : EfRepositoryBase<Order, FoodWebsiteDbContext>, IOrderDal
    {
        public OrderDal(FoodWebsiteDbContext context) : base(context)
        {
        }
    }
}