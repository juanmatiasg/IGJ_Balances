using AutoMapper;
using Balances.DTO;
using Balances.Model;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System;

namespace Balances.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
       
            CreateMap<CaratulaDto, BalanceDto>().ReverseMap();
            CreateMap<Balance, BalanceDto>().ReverseMap();
            CreateMap<BalanceDto, Balance>().ReverseMap();
            CreateMap<Autoridad, AutoridadDto>().ReverseMap();
            CreateMap<AutoridadDto, Autoridad>().ReverseMap();
            CreateMap<Caratula, CaratulaDto>().ReverseMap();
            CreateMap<CaratulaDto, Caratula>().ReverseMap();
            CreateMap<EstadoContableDto, EstadoContable>().ReverseMap();
            CreateMap<EstadoContable, EstadoContableDto>().ReverseMap();
            CreateMap<RubroPatrimonioNetoDto, RubroPatrimonioNeto>().ReverseMap();
            CreateMap<RubroPatrimonioNeto, RubroPatrimonioNetoDto>().ReverseMap();
            CreateMap<ArchivoDTO, Archivo>().ReverseMap();
            CreateMap<Archivo, ArchivoDTO>().ReverseMap();
            CreateMap<Libro, LibroDto>().ReverseMap();
            CreateMap<LibroDto, Libro>().ReverseMap();
            CreateMap<LibrosDto, Libros>().ReverseMap();
            CreateMap<Libros, LibrosDto>().ReverseMap();
            CreateMap<PersonaHumana, PersonaHumanaDto>().ReverseMap();
            CreateMap<PersonaHumanaDto, PersonaHumana>().ReverseMap();
            CreateMap<PersonaJuridica, PersonaJuridicaDto>().ReverseMap();
            CreateMap<PersonaJuridicaDto, PersonaJuridica>().ReverseMap();
            CreateMap<Socios, SociosDto>().ReverseMap();
            CreateMap<SociosDto, Socios>().ReverseMap();

        }
    }
}