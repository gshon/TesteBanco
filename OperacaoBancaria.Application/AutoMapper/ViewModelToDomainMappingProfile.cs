using AutoMapper;
using OperacaoBancaria.Application.ViewModels;
using OperacaoBancaria.Domain.ValueObjects;

namespace OperacaoBancaria.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<AnaliseCreditoViewModel, AnaliseCreditoRequest>();
        }
    }
}
