using AutoMapper;
using Core.Aspects.Autofac.Validation;
using Core.Business.Request;
using Core.Business.Response;
using Core.DataAccess.Paging;
using Core.Services.RabbitMQ;
using TryJobProject.Business.Abstract;
using TryJobProject.Business.DTOs.Categories;
using TryJobProject.Business.ValidationRules.FluentValidation;
using TryJobProject.DataAccess.Abstract;
using TryJobProject.DataAccess.Concrete.EntityFramework;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.Concrete
{
    public class CategoryManger : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IMapper _mapper;
        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public CategoryManger(ICategoryDal categoryDal, IMapper mapper, RabbitMQPublisher rabbitMQPublisher)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [ValidationAspect(typeof(CategoryValidator))]
        public async Task AddAsync(CreatedCategoryDto createdCategoryDto)
        {
            Category category = _mapper.Map<Category>(createdCategoryDto);
            await _categoryDal.AddAsync(category);
            await _categoryDal.SaveAsync();
        }

        public async Task<GetListResponse<GetListCategoryDto>> GetAllAsync(PageRequest pageRequest)
        {
            Paginate<Category> categories = await _categoryDal.GetAllAsync(size:pageRequest.PageSize,index:pageRequest.PageIndex,withDeleted:true);
            GetListResponse<GetListCategoryDto> response = _mapper.Map<GetListResponse<GetListCategoryDto>>(categories);

            _rabbitMQPublisher.Publish(response);

            return response;
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            return await _categoryDal.GetAsync(c => c.Id == Guid.Parse(id));
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _categoryDal.GetAsync(c => c.Name == name);
        }

        public async Task RemoveAsync(string id)
        {
            Category category = await _categoryDal.GetAsync(a => a.Id == Guid.Parse(id));
            await _categoryDal.RemoveAsync(category);
            await _categoryDal.SaveAsync();
        }
    }
}
