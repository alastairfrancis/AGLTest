using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace AGLTest.Api.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Register the Swagger generator, and define the Swagger documents 
        /// </summary>
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "AGL CodeTest API", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
            });
        }
    }
}
