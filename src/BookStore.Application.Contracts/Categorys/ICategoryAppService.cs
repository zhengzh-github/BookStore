using BookStore.Categorys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Categorys
{
    public interface ICategoryAppService
    {
        Task<string> CreateAsync(CreateCategoryDto input);
    }
}
