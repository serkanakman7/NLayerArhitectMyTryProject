using AutoMapper;
using Core.Business.Request;
using Core.Business.Response;
using Core.DataAccess.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.Abstract;
using TryJobProject.Business.DTOs.Addresses;
using TryJobProject.DataAccess.Abstract;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.Concrete
{
    public class AddressManager : IAddressService
    {

        private readonly IAddressDal _addressDal;
        private readonly IMapper _mapper;

        public AddressManager(IAddressDal addressDal, IMapper mapper)
        {
            _addressDal = addressDal;
            _mapper = mapper;
        }

        public async Task AddAsync(Address address)
        {
            await _addressDal.AddAsync(address);
            await _addressDal.SaveAsync();
        }

        public async Task DeleteAsync(string id)
        {
            Address address  = await _addressDal.GetAsync(a => a.Id==Guid.Parse(id));
            await _addressDal.RemoveAsync(address);
            await _addressDal.SaveAsync();
        }

        public async Task<GetListResponse<GetListAddressDto>> GetAllAsync()
        {
            Paginate<Address> addresses = await _addressDal.GetAllAsync();
            GetListResponse<GetListAddressDto> response = _mapper.Map<GetListResponse<GetListAddressDto>>(addresses);
            return response;

        }
    }
}
