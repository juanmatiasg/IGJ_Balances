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

        [HttpPost("{balanceId}")]
        public IActionResult CreateSession(string balanceId)
        {
            _sessionService.SetBalanceId(balanceId);
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


            var session = _sessionService.GetBalanceId();

            var response = new ResponseDTO<String>();


            if (session == null)
            {
                response.Message = "Session not found";
                response.IsSuccess = false;
                NotFound();
            }
            else
            {

                response.Result = session;
                response.IsSuccess = true;
                response.Message = "Session found";
            }






            return Ok(response);
        }
    }
}
