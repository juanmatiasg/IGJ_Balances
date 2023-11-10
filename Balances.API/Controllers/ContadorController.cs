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
        private readonly IContadorService _servicio;
        private readonly ISessionService _sessionService;
        public ContadorController(IContadorService servicio, ISessionService sessionService)
        {
            _servicio = servicio;
            _sessionService = sessionService;
        }

        [HttpPost]
        [Route("Post")]
        public ContadorDto PostContador([FromBody] ActualizarContadorDto contador)
        {

            var contadorDb = _servicio.ActualizarContador(contador);

            return contadorDb;

        }

        [HttpGet]
        [Route("Contador/{balanceId}")]
        public ContadorDto Get(string balanceId)
        {

            var contador = _servicio.Get(balanceId);

            return contador;
        }

    }
}
