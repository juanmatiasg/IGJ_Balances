using Balances.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        private readonly IArchivoService _servicio;
        private readonly ISessionService _sessionService;

        public ArchivoController(IArchivoService servicio, ISessionService sessionService)
        {
            _servicio = servicio;
            _sessionService = sessionService;
        }


        [HttpPost]
        //[Route("Archivo/Upload/{balanceId}")]
        [Route("Upload")]
        public Task<bool> UploadFile()
        {
            string balanceId = _sessionService.GetSessionId();
            bool resultado = false;
            var files = Request.Form.Files;
            _servicio.Upload(balanceId, files);
            return Task.FromResult(resultado);
        }




    }
}
