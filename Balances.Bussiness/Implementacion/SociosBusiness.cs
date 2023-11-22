using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;

namespace Balances.Bussiness.Implementacion
{
    public class SociosBusiness : ISociosBusiness

    {
        private readonly IBalanceBusiness _balanceBusiness;
        private readonly IMapper _mapper;

        public SociosBusiness(IBalanceBusiness balanceBusiness, IMapper mapper)
        {
            _balanceBusiness = balanceBusiness;
            _mapper = mapper;
        }

        public ResponseDTO<BalanceDto> InsertPersonaJuriridica(PersonaJuridicaDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();


            try
            {
                var bDto = _balanceBusiness.BalanceActual;

                modelo.Id = Guid.NewGuid().ToString();


                if (bDto.Socios == null) bDto.Socios = new SociosDto();

                if (bDto.Socios.PersonasJuridicas == null)
                    bDto.Socios.PersonasJuridicas = new List<PersonaJuridicaDto>();

                bDto.Socios.PersonasJuridicas.Add(modelo);




                _balanceBusiness.Update(bDto);

                respuesta.IsSuccess = true;
                respuesta.Result = bDto;
                respuesta.Message = "Persona juridica guardada correctamente";


            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;

            }

            return respuesta;
        }

        public ResponseDTO<BalanceDto> InsertPersonaHumana(PersonaHumanaDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();


            try
            {
                var bDto = _balanceBusiness.BalanceActual;

                modelo.Id = Guid.NewGuid().ToString();

                if (bDto.Socios == null) bDto.Socios = new SociosDto();

                if (bDto.Socios.PersonasHumanas == null)
                    bDto.Socios.PersonasHumanas = new List<PersonaHumanaDto>();

                bDto.Socios.PersonasHumanas.Add(modelo);




                _balanceBusiness.Update(bDto);

                respuesta.IsSuccess = true;
                respuesta.Result = bDto;
                respuesta.Message = "Persona humana guardada correctamente";


            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;

            }

            return respuesta;
        }
    }
}
