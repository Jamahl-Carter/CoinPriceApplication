using System;
using CoinPrice.Common.Configuration.Model;
using CoinPrice.Data.Client;
using CoinPrice.Data.Client.Implementation;
using CoinPrice.Data.Gateway;
using CoinPrice.Data.Gateway.Implementation;
using CoinPrice.Data.Repository;
using CoinPrice.Data.Repository.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace CoinPrice.Data.Configuration
{
    public static class Bindings
    {
        public static IServiceCollection AddDataDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICoinPriceGateway, CoinPriceGateway>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddHttpClient<ICointreeClient, CointreeClient>((factory, configureClient) =>
            {
                string coinTreeUrl = factory.GetRequiredService<WebApiUrls>().CoinTreeUrl;
                configureClient.BaseAddress = new Uri(coinTreeUrl);
            });
            services.AddMemoryCache();

            return services;
        }
    }
}
