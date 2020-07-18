using Microsoft.Extensions.DependencyInjection;
using OperacaoBancaria.Infra.CrossCutting.IoC;

namespace OperacaoBancaria.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}