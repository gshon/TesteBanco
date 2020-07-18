using OperacaoBancaria.Domain.Enum;

namespace OperacaoBancaria.Application.ViewModels
{
    public class ResultadoAnaliseViewModel
    {
        public StatusCredito StatusCredito { get; set; }
        public decimal ValorToTal { get; set; }
        public decimal ValorJuros { get; set; }
    }
}
