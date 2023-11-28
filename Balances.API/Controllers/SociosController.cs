using Balances.Bussiness.Contrato;
using Balances.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SociosController : ControllerBase
    {
        private readonly ISociosBusiness _sociosBusiness;

        public SociosController(ISociosBusiness sociosBusiness)
        {
            _sociosBusiness = sociosBusiness;
        }

        [HttpPost]
        [Route("InsertPersonaJuridica")]
        public ResponseDTO<BalanceDto> InsertPersonaJuridica(PersonaJuridicaDto personaJuridicaDto)
        {

            var rsp = _sociosBusiness.InsertPersonaJuriridica(personaJuridicaDto);
            return rsp;

        }

        [HttpPost]
        [Route("InsertPersonaHumana")]
        public ResponseDTO<BalanceDto> InsertPersonaHumana(PersonaHumanaDto personaHumanaDto)
        {

            var rsp = _sociosBusiness.InsertPersonaHumana(personaHumanaDto);
            return rsp;

        }
        [HttpDelete]
        [Route("DeletePersonaJuridica")]
        public ResponseDTO<BalanceDto> DeletePersonaJuridica(PersonaJuridicaDto personaJuridicaDto)
        {

            var rsp = _sociosBusiness.DeletePersonaJuriridica(personaJuridicaDto);
            return rsp;

        }

        [HttpDelete]
        [Route("DeletePersonaHumana")]
        public ResponseDTO<BalanceDto> DeletePersonaHumana(PersonaHumanaDto personaHumanaDto)
        {

            var rsp = _sociosBusiness.DeletePersonaHumana(personaHumanaDto);
            return rsp;

        }
    }
}
