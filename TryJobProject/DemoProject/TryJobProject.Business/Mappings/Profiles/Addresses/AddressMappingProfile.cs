using AutoMapper;
using Core.Business.Response;
using Core.DataAccess.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.Addresses;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.Mappings.Profiles.Addresses
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<Address, GetListAddressDto>();
            CreateMap<Paginate<Address>,GetListResponse<GetListAddressDto>>();
        }
    }
}
