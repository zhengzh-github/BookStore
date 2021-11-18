using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace BookStore.MongoDB
{
    public static class BookStoreMongoDbContextExtensions
    {
        public static void ConfigureBookStore(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new BookStoreMongoModelBuilderConfigurationOptions(
                BookStoreDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}