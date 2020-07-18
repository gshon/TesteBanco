using MediatR;
using OperacaoBancaria.Domain.Core.Notifications;
using OperacaoBancaria.Domain.Interfaces;
using OperacaoBancaria.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace OperacaoBancaria.Domain.Services
{
    public class AnaliseCreditoService : IAnaliseCreditoService
    {
        private readonly DomainNotificationHandler _notifications;

        public AnaliseCreditoService(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        public Task<AnaliseCreditoResponse> AnalisaCredito(AnaliseCreditoRequest analiseCreditoResquest)
        {
            var resultadoAnalise = new AnaliseCreditoResponse();

            ValidaEntrada(analiseCreditoResquest).Wait();

            if (_notifications.HasNotifications())
            {
                resultadoAnalise.StatusCredito = Enum.StatusCredito.REPROVADO;
            }
            else
            {
                int valorJuros = 0;

                switch (analiseCreditoResquest.TipoCredito)
                {
                    case Enum.TipoCreditoTaxa.CONSIGNADO :
                        valorJuros = 1;
                        break;
                    case Enum.TipoCreditoTaxa.DIRETO:
                        valorJuros = 2;
                        break;
                    case Enum.TipoCreditoTaxa.IMOBILIARIO:
                        valorJuros = 9;
                        break;
                    case Enum.TipoCreditoTaxa.PF:
                        valorJuros = 3;
                        break;
                    case Enum.TipoCreditoTaxa.PJ:
                        valorJuros = 5;
                        break;
                }

                resultadoAnalise.StatusCredito = Enum.StatusCredito.APROVADO;
                resultadoAnalise.ValorJuros = Math.Round((valorJuros * analiseCreditoResquest.ValorCredito) / 100, 2);
                resultadoAnalise.ValorToTal = analiseCreditoResquest.ValorCredito + resultadoAnalise.ValorJuros;

            }

            return Task.FromResult(resultadoAnalise);
        }

        private Task ValidaEntrada(AnaliseCreditoRequest analiseCreditoResquest)
        {
            if(analiseCreditoResquest.QuantidadeParcelas < 5)
                _notifications.Handle(new DomainNotification("Parcelas", "O valor minimo de parcelas é de 5"), new System.Threading.CancellationToken());

            if (analiseCreditoResquest.QuantidadeParcelas > 72)
                _notifications.Handle(new DomainNotification("Parcelas", "O valor maximo de parcelas é de 72"), new System.Threading.CancellationToken());

            if (analiseCreditoResquest.ValorCredito > 1000000)
                _notifications.Handle(new DomainNotification("Valor Credito", "O valor máximo de emprestimo é de R$1.000.000,00"), new System.Threading.CancellationToken());

            if (analiseCreditoResquest.DataVencimento < DateTime.Now.AddDays(15))
                _notifications.Handle(new DomainNotification("Data Vencimento", "Data de vencimento deve ser acima de 15 dias a partir de hoje"), new System.Threading.CancellationToken());

            if (analiseCreditoResquest.DataVencimento > DateTime.Now.AddDays(40))
                _notifications.Handle(new DomainNotification("Data Vencimento", "Data de vencimento deve ser inferior a 40 dias a partir de hoje"), new System.Threading.CancellationToken());

            if (analiseCreditoResquest.TipoCredito == Enum.TipoCreditoTaxa.PJ && analiseCreditoResquest.ValorCredito < 15000)
                _notifications.Handle(new DomainNotification("Valor Credito", "O valor mínimo de emprestimo para PJ é R$15.000,00"), new System.Threading.CancellationToken());

            return Task.CompletedTask;
        }
    }
}
