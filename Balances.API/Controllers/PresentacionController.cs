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

        [HttpGet("GenerarPresentacion/{sesionId}")]
        public ResponseDTO<BalanceDto> GenerarPresentacion(string sesionId)

        {

            var rsp = _presentacionBusiness.PresentarTramite(sesionId);
            return rsp;
        }


        [HttpGet("GenerarPresentacionEnHtml")]
        public string GenerarPresentacionEnHtml()
        {
            var rsp = _presentacionBusiness.FormatPresentacionHTML();
            return rsp;
        }
    }
}

