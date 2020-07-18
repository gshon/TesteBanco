using OperacaoBancaria.Application.ViewModels;
using System.Threading.Tasks;

namespace OperacaoBancaria.Application.Interfaces
{
    public interface IAnaliseCreditoAppService
    {
        public Task<ResultadoAnaliseViewModel> AnalisaCredito(AnaliseCreditoViewModel analiseCreditoViewModel);
    }
}
