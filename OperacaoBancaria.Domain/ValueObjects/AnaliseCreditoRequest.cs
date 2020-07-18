using OperacaoBancaria.Domain.Enum;
using System;

namespace OperacaoBancaria.Domain.ValueObjects
{
    public class AnaliseCreditoRequest
    {
        public decimal ValorCredito { get; set; }
        public TipoCreditoTaxa TipoCredito { get; set; }
        public int QuantidadeParcelas { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
