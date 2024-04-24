using Balances.DTO;
using Balances.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;


        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }


        [HttpGet("getNewSession")]
        public IActionResult GetNewSession()
        {

            var sesionId = _sessionService.GetNewSesion();

            var response = new ResponseDTO<String>();


            if (sesionId == null)
            {
                response.Message = "Session not created";
                response.IsSuccess = false;
                NotFound();
            }
            else
            {

                response.Result = sesionId;
                response.IsSuccess = true;
                response.Message = "Session created";
            }

            return Ok(response);
        }


        [HttpGet("SetBalanceId")]
        public ActionResult<bool> SetBalanceId(string sesionId, string balanceId)
        {

            var sesion = _sessionService.SetBalance(sesionId, balanceId);

            return sesion;
        }

        [HttpGet("getBalanceId")]
        public ActionResult<string> GetBalanceId(string sesionId)
        {

            var balIdSession = _sessionService.GetBalanceId(sesionId);

            return balIdSession;
        }


    }
}
