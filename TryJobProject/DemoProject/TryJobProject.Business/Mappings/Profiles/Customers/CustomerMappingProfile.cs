using AutoMapper;
using Core.Business.Response;
using Core.DataAccess.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.Customers;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.Mappings.Profiles.Customers
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<Paginate<Customer>, GetListResponse<CustomerDto>>().ReverseMap();
        }
    }
}
