using AGLTest.Common.Config;
using AGLTest.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AGLTest.Common.IOC
{
    public static class DependencyRegistration
    {
        /// <summary>
        /// Register all services
        /// </summary>
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // configure, and register pet service
            var feedConfig = new DataFeedConfig();
            configuration.GetSection("DataFeed").Bind(feedConfig);

            services.AddScoped<IPetService>(_provider => new PetService(feedConfig));
        }
    }
}
