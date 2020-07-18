using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace OperacaoBancaria.Api.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Operações Bancarias API",
                    Description = "Operações Bancarias API"
                });

                swagger.EnableAnnotations();
            });
        }
    }
}
