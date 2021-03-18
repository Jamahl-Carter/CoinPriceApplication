using CoinPrice.Business.Service;
using CoinPrice.Business.Service.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace CoinPrice.Business.Configuration
{
    public static class Bindings
    {
        public static IServiceCollection AddBusinessDependencies(this IServiceCollection services)
        {
            services.AddTransient<IPriceCheckService, PriceCheckService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
