using Balances.Bussiness.Contrato;
using Balances.Bussiness.Implementacion;
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
        [Route("InsertPersonaHumana")]
        public ResponseDTO<BalanceDto> InsertPersonaHumana(PersonaHumanaDto personaHumanaDto)
        {

            var rsp = _sociosBusiness.InsertPersonaHumana(personaHumanaDto);
            return rsp;

        }

        [HttpPost]
        [Route("UpdatePersonaHumana")]
        public ResponseDTO<BalanceDto> UpdatePersonaHumana(PersonaHumanaDto personaHumanaDto)
        {

            var rsp = _sociosBusiness.UpdatePersonaHumana(personaHumanaDto);
            return rsp;

        }

        [HttpDelete]
        [Route("DeletePersonaHumana")]
        public ResponseDTO<BalanceDto> DeletePersonaHumana(PersonaHumanaDto personaHumanaDto)
        {

            var rsp = _sociosBusiness.DeletePersonaHumana(personaHumanaDto);
            return rsp;

        }


        [HttpPost]
        [Route("InsertPersonaJuridica")]
        public ResponseDTO<BalanceDto> InsertPersonaJuridica(PersonaJuridicaDto personaJuridicaDto)
        {

            var rsp = _sociosBusiness.InsertPersonaJuriridica(personaJuridicaDto);
            return rsp;

        }

        [HttpDelete]
        [Route("DeletePersonaJuridica")]
        public ResponseDTO<BalanceDto> DeletePersonaJuridica(PersonaJuridicaDto personaJuridicaDto)
        {

            var rsp = _sociosBusiness.DeletePersonaJuridica(personaJuridicaDto);
            return rsp;

        }

        [HttpPost]
        [Route("UpdatePersonaJuridica")]
        public ResponseDTO<BalanceDto> UpdatePersonaJuridica(PersonaJuridicaDto personaJuridicaDto)
        {

            var rsp = _sociosBusiness.UpdatePersonaJuridica(personaJuridicaDto);
            return rsp;

        }

    }
}
