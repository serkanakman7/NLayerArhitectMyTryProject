using AutoMapper;
using Core.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.OperationClaims;

namespace TryJobProject.Business.Mappings.Profiles.OperationClaims
{
    internal class OpreationClaimMappingProfile : Profile
    {
        public OpreationClaimMappingProfile()
        {
            CreateMap<OperationClaim, OperationClaimDto>().ReverseMap();
        }
    }
}
