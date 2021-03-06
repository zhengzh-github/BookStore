using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace BookStore
{
    [DependsOn(
        typeof(BookStoreHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class BookStoreConsoleApiClientModule : AbpModule
    {
        
    }
}
