using AutoMapper;
using Balances.DTO;
using Balances.Model;
using MongoDB.Bson;

namespace Balances.Utilities
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
     
            CreateMap<Balance, BalanceResponseDTO>();
            CreateMap<BalanceRequestDTO, Balance>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => ObjectId.GenerateNewId()));
            CreateMap<EntidadRequestDTO, Entidad>();
            CreateMap<Entidad, EntidadResponseDTO>();
        }
    }
}