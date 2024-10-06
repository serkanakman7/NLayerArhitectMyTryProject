using AutoMapper;
using Core.Aspects.Autofac.Validation;
using Core.Entites.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.Abstract;
using TryJobProject.Business.DTOs.OperationClaims;
using TryJobProject.Business.ValidationRules.FluentValidation;
using TryJobProject.DataAccess.Abstract;

namespace TryJobProject.Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        private readonly IMapper _mapper;

        public OperationClaimManager(IOperationClaimDal operationClaimDal, IMapper mapper)
        {
            _operationClaimDal = operationClaimDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        public async Task AddAsync(OperationClaimDto operationCliamDto)
        {
            OperationClaim operationClaim = _mapper.Map<OperationClaim>(operationCliamDto);
            await _operationClaimDal.AddAsync(operationClaim);
            await _operationClaimDal.SaveAsync();
        }

        public async Task<List<OperationClaimDto>> GetAllAsync()
        {
            List<OperationClaim> operationClaims = await _operationClaimDal.GetAll().ToListAsync();
            List<OperationClaimDto> operationClaimDto = _mapper.Map<List<OperationClaimDto>>(operationClaims);
            return operationClaimDto;
        }

        public async Task<OperationClaim> GetByNameAsync(string name)
        {
             return await _operationClaimDal.GetAsync(o=>o.Name == name);
        }
    }
}
