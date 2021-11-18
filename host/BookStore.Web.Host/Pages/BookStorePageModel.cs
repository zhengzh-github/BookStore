﻿using BookStore.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace BookStore.Pages
{
    public abstract class BookStorePageModel : AbpPageModel
    {
        protected BookStorePageModel()
        {
            LocalizationResourceType = typeof(BookStoreResource);
        }
    }
}