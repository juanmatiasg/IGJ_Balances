using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EstadoContableController : ControllerBase
    {
        private readonly IEstadoContableBusiness _estadocontableBusiness;
        private readonly ISessionService _sessionService;

        public EstadoContableController(IEstadoContableBusiness estadocontableBusiness, ISessionService sessionService)
        {
            _estadocontableBusiness = estadocontableBusiness;
            _sessionService = sessionService;
        }

        [HttpPost]
        [Route("InsertEECC")]
        public ResponseDTO<BalanceDto> InsertEECC([FromBody] EstadoContableDto estadocontableDto)
        {

            var rsp = _estadocontableBusiness.Insert(estadocontableDto);
            return rsp;

        }
        [HttpPost]
        [Route("InsertRubro")]
        public ResponseDTO<BalanceDto> InsertRubro([FromBody] RubroPatrimonioNetoDto rubroDto)
        {

            var rsp = _estadocontableBusiness.Insert(rubroDto);
            return rsp;

        }

        [HttpPost]
        [Route("DeleteRubro")]
        public ResponseDTO<BalanceDto> DeleteRubro([FromBody] RubroPatrimonioNetoDto rubroDto)
        {

            var rsp = _estadocontableBusiness.Delete(rubroDto);
            return rsp;

        }
    }


}
