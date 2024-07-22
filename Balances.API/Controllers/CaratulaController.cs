using Balances.Bussiness.Contrato;
using Balances.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CaratulaController : ControllerBase
    {

        private readonly ICaratulaBusiness _caratulaBusiness;


        public CaratulaController(ICaratulaBusiness caratulaBusiness)
        {
            _caratulaBusiness = caratulaBusiness;

        }

        [HttpPost("InsertCaratula")]
        public IActionResult Insert(CaratulaDto caratuladto)

        {

            var rsp = _caratulaBusiness.Insert(caratuladto);
            return Ok(rsp);
        }

        [HttpPost("RectificarCaratula")]
        public IActionResult Rectificar(BalanceDto balance)

        {

            var rsp = _caratulaBusiness.Rectificar(balance);
            return Ok(rsp);
        }

        [HttpPost("UpdateCaratula")]
        public IActionResult Update(CaratulaDto caratuladto)

        {

            var rsp = _caratulaBusiness.Update(caratuladto);
            return Ok(rsp);
        }



    }
}
