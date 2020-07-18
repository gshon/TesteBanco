using MediatR;
using OperacaoBancaria.Domain.Core.Command;
using OperacaoBancaria.Domain.Core.Events;
using OperacaoBancaria.Domain.Interfaces;
using System.Threading.Tasks;

namespace OperacaoBancaria.Domain.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task EnviarComando<T>(T comando) where T : Command
        {
            await _mediator.Send(comando);       
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }
    }
}
