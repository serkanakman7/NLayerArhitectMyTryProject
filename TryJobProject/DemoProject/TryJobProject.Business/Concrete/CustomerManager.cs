using AutoMapper;
using Core.Aspects.Autofac.Validation;
using Core.Business.Request;
using Core.Business.Response;
using Core.DataAccess.Paging;
using Microsoft.EntityFrameworkCore;
using TryJobProject.Business.Abstract;
using TryJobProject.Business.DTOs.Customers;
using TryJobProject.Business.ValidationRules.FluentValidation;
using TryJobProject.DataAccess.Abstract;
using TryJobProject.DataAccess.Concrete.EntityFramework;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        private readonly IMapper _mapper;

        public CustomerManager(ICustomerDal customerDal, IMapper mapper)
        {
            _customerDal = customerDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public async Task AddAsync(CustomerDto customerDto)
        {
            Customer customer = _mapper.Map<Customer>(customerDto);
            await _customerDal.AddAsync(customer);
            await _customerDal.SaveAsync();
        }

        public async Task DeleteAsync(string id)
        {
            Customer customer = await _customerDal.GetAsync(a => a.Id == Guid.Parse(id));
            await _customerDal.RemoveAsync(customer);
            await _customerDal.SaveAsync();
        }

        public async Task<GetListResponse<CustomerDto>> GetAllAsync(PageRequest pageRequest)
        {
            Paginate<Customer> customers = await _customerDal.GetAllAsync(index:pageRequest.PageIndex,size:pageRequest.PageSize);
            GetListResponse<CustomerDto> customerDtos = _mapper.Map<GetListResponse<CustomerDto>>(customers);
            return customerDtos;
        }
    }
}
