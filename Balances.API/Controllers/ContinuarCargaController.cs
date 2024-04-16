using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    public class ContinuarCargaController : ControllerBase
    {

        public IActionResult Index(string balid)
        {
            
            return Ok();
        }
    }
}
