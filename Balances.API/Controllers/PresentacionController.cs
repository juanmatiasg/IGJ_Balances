using Balances.Bussiness.Contrato;
using Balances.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PresentacionController : ControllerBase
    {
        private readonly IPresentacionBusiness _presentacionBusiness;

        public PresentacionController(IPresentacionBusiness presentacionBusiness)
        {
            _presentacionBusiness = presentacionBusiness;
        }

        [HttpGet("GenerarPresentacion")]
        public ResponseDTO<BalanceDto> GenerarPresentacion()

        {

            var rsp = _presentacionBusiness.PresentarTramite();
            return rsp;
        }
    }
}
