using Balances.Model;
using Balances.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceService _balanceService;

        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }


        [HttpPost("PostBalance")]
        public IActionResult Create(Balance balance)

        {
            _balanceService.InsertBalance(balance); // Asegúrate de que el método utilizado sea el correcto

            return Ok(balance);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var balance = _balanceService.GetById(id);
            if (balance == null)
            {
                return NotFound();
            }

            return Ok(balance);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var balances = _balanceService.GetAll();

            return Ok(balances);
        }


        [HttpPut]
        public IActionResult Update(Balance balance)
        {
            _balanceService.UpdateBalance(balance.Id, balance);



            return Ok(balance);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _balanceService.DeleteBalance(id);

            return Ok();
        }
    }
}
