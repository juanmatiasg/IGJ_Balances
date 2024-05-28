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


        [HttpGet("GenerarPresentacionEnHtml/{sesionId}")]
        public string GenerarPresentacionEnHtml(string sesionId)
        {
            var rsp = _presentacionBusiness.FormatPresentacionHTML(sesionId);
            return rsp;
        }
    }
}

