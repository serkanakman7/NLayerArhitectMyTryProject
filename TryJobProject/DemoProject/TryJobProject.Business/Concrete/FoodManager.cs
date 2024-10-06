using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.Business.Request;
using Core.Business.Response;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.DataAccess.Paging;
using Microsoft.EntityFrameworkCore;
using Npgsql.TypeMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.Abstract;
using TryJobProject.Business.BusinessAspects.Autofac;
using TryJobProject.Business.BusinessRules.Foods;
using TryJobProject.Business.DTOs.Foods;
using TryJobProject.Business.ValidationRules.FluentValidation;
using TryJobProject.DataAccess.Abstract;
using TryJobProject.DataAccess.Concrete.EntityFramework;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.Concrete
{
    public class FoodManager : IFoodService
    {
        private readonly IFoodDal _foodDal;
        private readonly IMapper _mapper;
        private readonly FoodBusinessRules _foodBusinessRules;

        public FoodManager(IFoodDal foodDal, IMapper mapper, FoodBusinessRules foodBusinessRules)
        {
            _foodDal = foodDal;
            _mapper = mapper;
            _foodBusinessRules = foodBusinessRules;
        }

        [ValidationAspect(typeof(FoodValidator))]
        public async Task<CreatedFoodDto> AddAsync(CreatedFoodDto foodDto)
        {

            Food food = _mapper.Map<Food>(foodDto);

            await _foodBusinessRules.DontNewCategory(food, foodDto.CategoryName);

            await _foodDal.AddAsync(food);
            await _foodDal.SaveAsync();
            CreatedFoodDto createdFoodDto = _mapper.Map<CreatedFoodDto>(food);

            return createdFoodDto;
        }

        public async Task AddRangeAsync(List<CreatedFoodDto> foodDtos)
        {
            List<Food> foods = _mapper.Map<List<Food>>(foodDtos);

            foreach (var food in foods)
            {
                await _foodBusinessRules.DontNewCategory(food, food.Category.Name);
            }

            await _foodDal.AddRangeAsync(foods);
            await _foodDal.SaveAsync();
        }

        [SecuredOperation("Admin,Worker")]
        [LogAspect(typeof(PostgreSqlLogger))]
        [CacheAspect]
        public async Task<GetListResponse<GetFoodDto>> GetAllAsync(PageRequest pageRequest)
        {
            Paginate<Food> foods = await _foodDal.GetAllAsync(index:pageRequest.PageIndex,size:pageRequest.PageSize);
            GetListResponse<GetFoodDto> getFoodDto = _mapper.Map<GetListResponse<GetFoodDto>>(foods);           
            
            return getFoodDto;
        }

        public async Task<GetListResponse<GetFoodDto>> GetByCategoryIdAsync(string id,PageRequest pageRequest)
        {
            Paginate<Food> foods = await _foodDal.GetAllAsync(f => f.CategoryId == Guid.Parse(id));
            GetListResponse<GetFoodDto> getFoodDto = _mapper.Map<GetListResponse<GetFoodDto>>(foods);

            return getFoodDto;
        }

        public async Task<GetFoodDto> GetByIdAsync(string id)
        {
            Food food = await _foodDal.GetAsync(f => f.Id == Guid.Parse(id));
            GetFoodDto getFoodDto = _mapper.Map<GetFoodDto>(food);

            return getFoodDto;
        }

        public async Task<List<GetFoodDto>> GetByPriceAsync(float min, float max)
        {
            List<Food> foods = await _foodDal.GetAll(f => f.Price >= min && f.Price <= max).ToListAsync();
            List<GetFoodDto> getFoodDto = _mapper.Map<List<GetFoodDto>>(foods);

            return getFoodDto;
        }

        public async Task<List<GetProductCategoryDto>> GetFoodDetailAsync()
        {
            List<Food> foods = await _foodDal.GetAll().ToListAsync();
            List<GetProductCategoryDto> getProductCategoryDto = _mapper.Map<List<GetProductCategoryDto>>(foods);

            return getProductCategoryDto;
        }

        public async Task RemoveAsync(string id)
        {
            Food food = await _foodDal.GetAsync(a => a.Id == Guid.Parse(id));
            await _foodDal.RemoveAsync(food);
            await _foodDal.SaveAsync();
        }
    }
}
