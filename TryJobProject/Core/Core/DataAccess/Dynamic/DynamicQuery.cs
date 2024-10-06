using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Dynamic
{
    public class DynamicQuery
    {
        public Filter? Filter { get; set; }
        public IEnumerable<Sort>? Sorts { get; set; }

        public DynamicQuery()
        {
            
        }

        public DynamicQuery(IEnumerable<Sort>? sorts,Filter? filter)
        {
            Filter = filter;
            Sorts = sorts;
        }
    }
}
