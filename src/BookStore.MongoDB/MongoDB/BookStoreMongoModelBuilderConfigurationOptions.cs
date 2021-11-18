using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace BookStore.MongoDB
{
    public class BookStoreMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public BookStoreMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}