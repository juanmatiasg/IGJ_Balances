using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BalanceController : ControllerBase
    {

        private readonly IBalanceBusiness _balanceBusiness;
        private readonly IBalanceService _balanceService;

        public BalanceController(IBalanceService balanceService, IBalanceBusiness balanceBusiness)
        {

            _balanceBusiness = balanceBusiness;
            _balanceService = balanceService;
        }

        [HttpPost("InsertBalance")]
        public IActionResult Create(Balance balance)
        {
            var rsp = _balanceBusiness.Insert(balance);

            return Ok(rsp);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var balance = _balanceBusiness.GetById(id);

            if (balance == null) return NotFound();

            return Ok(balance);
        }


        [HttpGet("GetAll/{correlativo}")]
        public IActionResult GetAll(string correlativo)
        {
            var balances = _balanceBusiness.List(correlativo);

            if (balances == null) return NotFound();

            return Ok(balances);
        }


        [HttpPut("Update")]
        public IActionResult Update(BalanceDto balance)
        {

            var balancedto = _balanceBusiness.Update(balance);

            return Ok(balancedto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var rsp = _balanceBusiness.Delete(id);

            return Ok(rsp);
        }
    }
}
