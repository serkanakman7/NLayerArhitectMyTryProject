using Core.CrossCuttingConcerns.Exception.Types;
using Core.Entites.Concrete;
using TryJobProject.Business.Abstract;
using TryJobProject.Business.Constants;

namespace TryJobProject.Business.BusinessRules.Auths
{
    public class AuthBusinessRules
    {
        private readonly IOperationClaimService _operationClaimService;
        private readonly List<OperationClaim> _operationClaim;

        public AuthBusinessRules(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
            _operationClaim = new List<OperationClaim>();
        }

        public async Task DontNewOperationClaim(User user, string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                OperationClaim operationClaim = await _operationClaimService.GetByNameAsync(roleName);
                if (operationClaim != null)
                {
                    _operationClaim.Add(operationClaim);
                }
                else
                {
                    throw new BusinessException(BusinessMessage.NotRoleName);
                }
            }
            user.OperationClaims = _operationClaim;
        }
    }
}
