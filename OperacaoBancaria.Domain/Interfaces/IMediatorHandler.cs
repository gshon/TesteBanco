using System.Threading.Tasks;
using OperacaoBancaria.Domain.Core.Command;
using OperacaoBancaria.Domain.Core.Events;

namespace OperacaoBancaria.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task EnviarComando<T>(T comando) where T : Command;
    }
}
