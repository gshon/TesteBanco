using AutoMapper;
using OperacaoBancaria.Application.ViewModels;
using OperacaoBancaria.Domain.ValueObjects;

namespace OperacaoBancaria.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<AnaliseCreditoResponse, ResultadoAnaliseViewModel>();
        }
    }
}
