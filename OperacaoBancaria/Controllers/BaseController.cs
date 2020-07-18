using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperacaoBancaria.Domain.Core.Notifications;
using OperacaoBancaria.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OperacaoBancaria.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;

        public const string message = "Processo efetuado com sucesso!";
        public BaseController(INotificationHandler<DomainNotification> notifications,
                                         IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected new IActionResult Response(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result ?? message
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected IActionResult ResponseBadRequest(IList<DomainNotification> notifications)
        {
            return BadRequest(new
            {
                success = false,
                errors = notifications.Select(n => n.Value)
            });
        }

        protected bool OperacaoValida()
        {
            return (!_notifications.HasNotifications());
        }

        protected void NotificarErroModelInvalida()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(string.Empty, erroMsg);
            }
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediator.PublicarEvento(new DomainNotification(codigo, mensagem));
        }

        protected void NotificarErro(List<string> lista)
        {
            lista.ForEach(l =>
            {
                _mediator.PublicarEvento(new DomainNotification("operacao", l));
            });
        }

        protected void NotificarErro(IList<FluentValidation.Results.ValidationFailure> erros)
        {
            erros.ToList().ForEach(e =>
            {
                _mediator.PublicarEvento(new DomainNotification(e.ErrorCode, e.ErrorMessage));
            });
        }

        protected string FormatarMensagemErro(string mensagem, string parametro)
        {
            return string.Format(mensagem, parametro);
        }

        protected bool HasNotifications() => this._notifications.HasNotifications();
        protected IList<DomainNotification> GetNotifications() => this._notifications.GetNotifications();

        protected IActionResult GetResponseAfterCheckingNotifications(object result = null)
        {
            if (this.HasNotifications())
                return this.ResponseBadRequest(this.GetNotifications());
            else
                return this.Response(result);
        }

    }
}
