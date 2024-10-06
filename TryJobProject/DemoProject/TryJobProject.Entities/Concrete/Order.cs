using Core.Entites.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.Entities.Concrete
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Description { get; set; }
        public Customer? Customer { get; set; }
        public ICollection<Food>? Foods { get; set; }
        public Company? Company { get; set; }
    }
}
