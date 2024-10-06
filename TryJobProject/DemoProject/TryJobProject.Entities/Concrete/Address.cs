using Core.Entites.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.Entities.Concrete
{
    public class Address : BaseEntity
    {
        public string City { get; set; }
        public string District { get; set; }
        public ICollection<Customer>? Customers { get; set; }
    }
}
