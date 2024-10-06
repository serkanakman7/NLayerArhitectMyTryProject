using Core.Entites.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.Entities.Concrete
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Address>? Addresses { get; set; }
    }
}
