using BookStore.Dtos;
using BookStore.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Samples
{
    public class CategoryAppService: ApplicationService
    {

        private readonly IRepository<Category, int> _categoryRepository;

        public CategoryAppService(IRepository<Category, int> repository)
        {
            _categoryRepository = repository;
        }

        /// <summary>
        /// 创建类别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> CreateAsync(CreateCategoryDto input)
        {
            var category = ObjectMapper.Map<CreateCategoryDto, Category>(input);

            var categoryAdd = await _categoryRepository.InsertAsync(category);

            return "添加成功";
        }
    }
}
