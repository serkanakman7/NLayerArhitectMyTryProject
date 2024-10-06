using Core.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.OperationClaims;

namespace TryJobProject.Business.Abstract
{
    public interface IOperationClaimService
    {
        Task<List<OperationClaimDto>> GetAllAsync();
        Task<OperationClaim> GetByNameAsync(string name);
        Task AddAsync(OperationClaimDto operationCliamDto);

    }
}
