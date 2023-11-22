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


            if (session == null)
            {
                NotFound();
            }


            var response = new ResponseDTO<String>
            {
                Result = session,
                IsSuccess = true,
                Message = "Session found"
            };



            return Ok(response);
        }
    }
}
