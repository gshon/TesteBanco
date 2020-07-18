using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OperacaoBancaria.Application.Interfaces;
using OperacaoBancaria.Application.Services;
using OperacaoBancaria.Domain.Core.Notifications;
using OperacaoBancaria.Domain.Handlers;
using OperacaoBancaria.Domain.Interfaces;
using OperacaoBancaria.Domain.Services;

namespace OperacaoBancaria.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));            

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            // Domain - Eventos
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain
            services.AddScoped<IAnaliseCreditoService, AnaliseCreditoService>(); 
            services.AddScoped<IAnaliseCreditoAppService, AnaliseCreditoAppService>();            
        }
    }
}
