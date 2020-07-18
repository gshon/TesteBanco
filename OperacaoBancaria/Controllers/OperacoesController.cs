using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperacaoBancaria.Application.Interfaces;
using OperacaoBancaria.Application.ViewModels;
using OperacaoBancaria.Domain.Core.Notifications;
using OperacaoBancaria.Domain.Interfaces;

namespace OperacaoBancaria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperacoesController : BaseController
    {
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;
        private readonly IAnaliseCreditoAppService _analiseCreditoAppService;

        public OperacoesController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator, IAnaliseCreditoAppService analiseCreditoAppService) : base(notifications, mediator)
        {
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
            _analiseCreditoAppService = analiseCreditoAppService;
        }

        [HttpPost]
        [Route("AnaliseCredito")]
        public IActionResult Post([FromBody] AnaliseCreditoViewModel model)
        {
            try
            {
                if (!ModelStateValida())
                {
                    return Response();
                }

                var resuldadoAnalise = _analiseCreditoAppService.AnalisaCredito(model);

                if (_notifications.HasNotifications())
                    return Response(_notifications.GetNotifications());

                var retorno = new
                {
                    Status = resuldadoAnalise.Result.StatusCredito.ToString(),
                    Juros = resuldadoAnalise.Result.ValorJuros,
                    Total = resuldadoAnalise.Result.ValorToTal
                };

                return Response(retorno);
            }
            catch (System.Exception ex)
            {
                return Response(ex);
            }            
        }

        private bool ModelStateValida()
        {
            if (ModelState.IsValid) return true;

            NotificarErroModelInvalida();
            return false;
        }
    }
}
