using Core.Entites.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites.Concrete
{
    public class OperationClaim : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
