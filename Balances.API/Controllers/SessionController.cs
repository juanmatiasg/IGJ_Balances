using Balances.DTO;
using Balances.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpPost("{balanceId}")]
        public IActionResult CreateSession(string balanceId)
        {
            _sessionService.SetSession(balanceId);

            var response = new ResponseDTO<String>
            {
                Result = "Success",
                IsSuccess = true,
                Message = "Session created successfully"
            };
            return Ok(response);

        }


        [HttpGet("getSession")]
        public IActionResult GetSession()
        {

            var session = _sessionService.GetSession();

            var response = new ResponseDTO<String>();


            if (session == null)
            {
                response.Message = "Session not found";
                response.IsSuccess = false;
                NotFound();
            }
            else
            {


                var sessionSerializada = JsonConvert.SerializeObject(session);




                response.Result = sessionSerializada;
                response.IsSuccess = true;
                response.Message = "Session found";
            }



            return Ok(response);
        }

        [HttpGet("getBalanceIdSession")]
        public ActionResult<string> GetBalanceIdSession()
        {

            var balIdSession = _sessionService.GetSessionBalanceId();

            return balIdSession;
        }

        [HttpGet("getTokenSession")]
        public ActionResult<string> GetTokenSession()
        {
            var token = _sessionService.GetSessionToken();

            return token;
        }
    }
}
