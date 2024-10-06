using Core.Entites.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.Entities.Concrete
{
    public class Food : BaseEntity
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public float Price { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public Category? Category { get; set; }
    }
}
