using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace BookStore.Categorys
{

    [RemoteService]
    [Route("api/BookStore/category")]
    public class CategoryController : BookStoreController, ICategoryAppService
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        [HttpPost]
        [Route("CreateAsync")]
        public async  Task<string> CreateAsync(CreateCategoryDto input)
        {
            return await  _categoryAppService.CreateAsync(input);
        }

    }
}
