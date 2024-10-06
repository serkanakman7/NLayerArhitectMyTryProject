﻿using Core.Entites.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryJobProject.Business.DTOs.Categories
{
    public class GetCategoryDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}