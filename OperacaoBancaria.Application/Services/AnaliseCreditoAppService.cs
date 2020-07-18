using AutoMapper;
using OperacaoBancaria.Application.Interfaces;
using OperacaoBancaria.Application.ViewModels;
using OperacaoBancaria.Domain.Interfaces;
using OperacaoBancaria.Domain.ValueObjects;
using System.Threading.Tasks;

namespace OperacaoBancaria.Application.Services
{
    public class AnaliseCreditoAppService : IAnaliseCreditoAppService
    {
        private readonly IAnaliseCreditoService _analiseCreditoService;
        private readonly IMapper _mapper;

        public AnaliseCreditoAppService(IAnaliseCreditoService analiseCreditoService, IMapper mapper)
        {
            _analiseCreditoService = analiseCreditoService;
            _mapper = mapper;
        }

        public Task<ResultadoAnaliseViewModel> AnalisaCredito(AnaliseCreditoViewModel analiseCreditoViewModel)
        {
            var dadosAnaliseCredito = _mapper.Map<AnaliseCreditoRequest>(analiseCreditoViewModel);
            var resultadoAnalise = _analiseCreditoService.AnalisaCredito(dadosAnaliseCredito);

            if (resultadoAnalise == null)
                return null;

            var resultado = _mapper.Map<ResultadoAnaliseViewModel>(resultadoAnalise.Result);

            return Task.FromResult(resultado);
        }
    }
}
