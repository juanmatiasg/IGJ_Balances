using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutoridadesController : ControllerBase
    {
        private readonly IAutoridadesBusiness _autoridadesBusiness;
        private readonly ISessionService _sessionService;

        public AutoridadesController(IAutoridadesBusiness autoridadesBusiness, ISessionService sessionService)
        {
            _autoridadesBusiness = autoridadesBusiness;
            _sessionService = sessionService;
        }

        [HttpPost]
        [Route("Insert")]
        public ResponseDTO<BalanceDto> Insert([FromBody] AutoridadDto autoridadDto)
        {

            var rsp = _autoridadesBusiness.Insert(autoridadDto);
            return rsp;

        }

        [HttpPost]
        [Route("Update")]
        public ResponseDTO<BalanceDto> Update([FromBody] AutoridadDto autoridadDto)
        {

            var rsp = _autoridadesBusiness.Update(autoridadDto);
            return rsp;

        }

        [HttpDelete]
        [Route("DeleteAutoridad")]
        public ResponseDTO<BalanceDto> Delete([FromBody] AutoridadDto autoridadDto)
        {

            var rsp = _autoridadesBusiness.Delete(autoridadDto);
            return rsp;

        }

    }
}
