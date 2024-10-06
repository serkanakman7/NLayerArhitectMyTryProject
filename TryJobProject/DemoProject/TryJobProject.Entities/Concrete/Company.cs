using Core.Entites.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.Entities.Concrete
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Order>? Companies { get; set; }
    }
}
