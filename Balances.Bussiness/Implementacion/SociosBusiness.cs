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

        public ResponseDTO<BalanceDto> DeletePersonaHumana(PersonaHumanaDto modelo)
        {
            var respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;
            try
            {
                var bal = _balanceBusiness.BalanceActual;

                var humano = bal.Socios.PersonasHumanas.FirstOrDefault(x => x.Id == modelo.Id);

                if (humano != null) bal.Socios.PersonasHumanas.Remove(humano);

                _balanceBusiness.Update(bal);
                respuesta.Result = bal;
                respuesta.IsSuccess = true;
                respuesta.Message = "persona humana borrada correctamente";
            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
            }

            return respuesta;

        }

        public ResponseDTO<BalanceDto> DeletePersonaJuriridica(PersonaJuridicaDto modelo)
        {
            var respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;

            try
            {
                var bal = _balanceBusiness.BalanceActual;

                var juridica = bal.Socios.PersonasJuridicas.FirstOrDefault(x => x.Id == modelo.Id);

                if (juridica != null) bal.Socios.PersonasJuridicas.Remove(juridica);

                _balanceBusiness.Update(bal);
                respuesta.Result = bal;
                respuesta.IsSuccess = true;
                respuesta.Message = "persona juridica borrada correctamente";
            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
            }

            return respuesta;
        }
    }
}
