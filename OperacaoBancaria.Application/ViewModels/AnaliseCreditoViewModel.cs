using OperacaoBancaria.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace OperacaoBancaria.Application.ViewModels
{
    public class AnaliseCreditoViewModel
    {
        [Required]
        public decimal ValorCredito { get; set; }
        public TipoCreditoTaxa TipoCredito { get; set; }

        [Required]
        public int QuantidadeParcelas { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
