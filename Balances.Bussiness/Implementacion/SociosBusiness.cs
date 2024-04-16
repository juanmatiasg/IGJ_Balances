using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Balances.Bussiness.Implementacion
{
    public class SociosBusiness : ISociosBusiness

    {
        private readonly IBalanceBusiness _balanceBusiness;
        private readonly IMapper _mapper;
        private readonly ILogger<SociosBusiness> _logger;

        public SociosBusiness(IBalanceBusiness balanceBusiness,
                              IMapper mapper,
                              ILogger<SociosBusiness> logger
                              )
        {
            _balanceBusiness = balanceBusiness;
            _mapper = mapper;
            _logger = logger;
        }

        public ResponseDTO<BalanceDto> InsertPersonaJuriridica(PersonaJuridicaDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            var pjSerializada = JsonConvert.SerializeObject(modelo);

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
                respuesta.Message = $"Persona juridica guardada correctamente";
                _logger.LogInformation($"SociosBusiness.InsertPersonaJuriridica : ---> {pjSerializada}");

            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
                _logger.LogError($"SociosBusiness.InsertPersonaJuriridica: \n {ex}");
            }

            return respuesta;
        }

        public ResponseDTO<BalanceDto> InsertPersonaHumana(PersonaHumanaDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            var pjSerializada = JsonConvert.SerializeObject(modelo);

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
                _logger.LogInformation($"SociosBusiness.InsertPersonaHumana : ---> {pjSerializada}");

            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
                _logger.LogError($"SociosBusiness.InsertPersonaHumana \n {ex}");
            }

            return respuesta;
        }

        public ResponseDTO<BalanceDto> DeletePersonaHumana(PersonaHumanaDto modelo)
        {
            var respuesta = new ResponseDTO<BalanceDto>();
            var pjSerializada = JsonConvert.SerializeObject(modelo);
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
                _logger.LogInformation($"SociosBusiness.DeletePersonaHumana :  ---> {pjSerializada}");
            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
                _logger.LogError($"SociosBusiness.DeletePersonaHumana: \n {ex}");
            }

            return respuesta;

        }

        public ResponseDTO<BalanceDto> DeletePersonaJuriridica(PersonaJuridicaDto modelo)
        {
            var respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;
            var pjSerializada = JsonConvert.SerializeObject(modelo);
            try
            {
                var bal = _balanceBusiness.BalanceActual;

                var juridica = bal.Socios.PersonasJuridicas.FirstOrDefault(x => x.Id == modelo.Id);

                if (juridica != null) bal.Socios.PersonasJuridicas.Remove(juridica);

                _balanceBusiness.Update(bal);
                respuesta.Result = bal;
                respuesta.IsSuccess = true;
                respuesta.Message = "persona juridica borrada correctamente";
                _logger.LogInformation($"SociosBusiness.DeletePersonaJuriridica : ---> {pjSerializada}");
            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
                _logger.LogError($"SociosBusiness.DeletePersonaJuriridica: \n {ex}");
            }

            return respuesta;
        }
    }
}
