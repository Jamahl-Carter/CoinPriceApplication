using CoinPrice.Common.Configuration.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoinPrice.Common.Configuration
{
    public static class Bindings
    {
        public static IServiceCollection AddCommonDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            // Arrange configuration instances
            var urlConfig = new WebApiUrls();

            // Populate with config
            configuration.Bind("WebApiUrls", urlConfig);

            // Register to container
            services.AddSingleton(urlConfig);

            return services;
        }
    }
}
