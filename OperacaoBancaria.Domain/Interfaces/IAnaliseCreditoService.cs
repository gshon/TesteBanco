using OperacaoBancaria.Domain.ValueObjects;
using System.Threading.Tasks;

namespace OperacaoBancaria.Domain.Interfaces
{
    public interface IAnaliseCreditoService
    {
        public Task<AnaliseCreditoResponse> AnalisaCredito(AnaliseCreditoRequest analiseCreditoResquest);
    }
}
