using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace BookStore
{
    [DependsOn(
        typeof(BookStoreApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class BookStoreHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "BookStore";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(BookStoreApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
