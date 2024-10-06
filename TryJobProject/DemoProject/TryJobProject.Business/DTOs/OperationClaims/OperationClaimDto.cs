using AutoMapper;
using Core.Entites.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.Business.DTOs.OperationClaims
{
    public class OperationClaimDto : IDto
    {
        public string Name { get; set; }
    }
}
