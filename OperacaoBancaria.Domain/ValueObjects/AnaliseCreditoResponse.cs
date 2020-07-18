using OperacaoBancaria.Domain.Enum;

namespace OperacaoBancaria.Domain.ValueObjects
{
    public class AnaliseCreditoResponse
    {
        public StatusCredito StatusCredito { get; set; }
        public decimal ValorToTal { get; set; }
        public decimal ValorJuros { get; set; }
    }
}
