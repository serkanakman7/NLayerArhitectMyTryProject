using Core.Entites.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.Business.DTOs.Addresses
{
    public class GetListAddressDto:IDto
    {
        public string City { get; set; }
        public string District { get; set; }
    }
}
