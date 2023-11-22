using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Services.Contract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Balances.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContadorController : ControllerBase
    {
        private readonly IContadorBusiness _contadorBusiness;
        private readonly ISessionService _sessionService;

        public ContadorController(IContadorBusiness contadorBusiness, ISessionService sessionService)
        {
            _contadorBusiness = contadorBusiness;
            _sessionService = sessionService;
        }

        [HttpPost]
        [Route("Insert")]
        public ResponseDTO<BalanceDto> Insert([FromBody] ContadorDto contadordto)
        {

            var rsp = _contadorBusiness.Insert(contadordto);
            return rsp;

        }

        [HttpGet]
        [Route("{balanceId}")]
        public ContadorDto Get(string balanceId)
        {

            var contadorDto = _contadorBusiness.GetById(balanceId);



            return contadorDto;
        }

    }
}
