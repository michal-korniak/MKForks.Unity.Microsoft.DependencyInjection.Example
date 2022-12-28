using Microsoft.Extensions.DependencyInjection;
using System;
using Unity;

namespace ASP.Net.Core.Unity.Example.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceProvider BuildCustomServiceProvider(this IServiceCollection services)
        {
            IServiceProvider microsoftServiceProvider = services.BuildServiceProvider();
            IServiceProviderFactory<IUnityContainer> unityServiceProviderFactory = microsoftServiceProvider.GetService<IServiceProviderFactory<IUnityContainer>>();
            IServiceProvider unityServiceProvider = unityServiceProviderFactory.CreateServiceProvider(unityServiceProviderFactory.CreateBuilder(services));

            return unityServiceProvider;
        }
    }
}
