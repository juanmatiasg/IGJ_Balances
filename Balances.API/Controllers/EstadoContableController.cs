using Balances.Bussiness.Contrato;
using Balances.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EstadoContableController : ControllerBase
    {
        private readonly IEstadoContableBusiness _estadocontableBusiness;


        public EstadoContableController(IEstadoContableBusiness estadocontableBusiness)
        {
            _estadocontableBusiness = estadocontableBusiness;

        }

        [HttpPost]
        [Route("InsertEECC")]
        public ResponseDTO<BalanceDto> InsertEECC([FromBody] EstadoContableDto estadocontableDto)
        {

            var rsp = _estadocontableBusiness.InsertEECC(estadocontableDto);
            return rsp;

        }
        [HttpPost]
        [Route("InsertRubro")]
        public ResponseDTO<BalanceDto> InsertRubro([FromBody] RubroPatrimonioNetoDto rubroDto)
        {

            var rsp = _estadocontableBusiness.InsertRubro(rubroDto);
            return rsp;

        }

        [HttpDelete]
        [Route("DeleteRubro")]
        public ResponseDTO<BalanceDto> DeleteRubro([FromBody] RubroPatrimonioNetoDto rubroDto)
        {

            var rsp = _estadocontableBusiness.DeleteRubro(rubroDto);
            return rsp;

        }
    }


}
