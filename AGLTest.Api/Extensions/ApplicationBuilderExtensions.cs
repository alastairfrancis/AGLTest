using Microsoft.AspNetCore.Builder;

namespace AGLTest.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Enable middleware to serve generated Swagger as JSON endpoints
        /// </summary>
        public static void UseSwaggerBuilder(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AGL CodeTest V1"); ;
            });
        }
    }
}
