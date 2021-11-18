using BookStore.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace BookStore
{
    public abstract class BookStoreController : AbpController
    {
        protected BookStoreController()
        {
            LocalizationResource = typeof(BookStoreResource);
        }
    }
}
