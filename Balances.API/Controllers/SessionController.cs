using Balances.DTO;
using Balances.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel.Channels;

namespace Balances.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private const string KEY_VALUE = "idSession";

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        public IActionResult CreateSession(string balanceId)
        {
            var sessionResult = _sessionService.CreateSessionId(KEY_VALUE, balanceId);

            var response = new ResponseDTO<String>
            {
                Result = sessionResult,
                IsSuccess = true,
                Message = "Session created successfully"
            };

            return Ok(response);

            

        }

        [HttpGet]
        public IActionResult GetSession()
        {
            var session = _sessionService.GetSessionId(KEY_VALUE);
            var response = new ResponseDTO<String>();

            if (session != null)
            {
                response.Result = session;
                response.IsSuccess = true;
                response.Message = "Session found";
            }
            else
            {
                response.Result = session;
                response.IsSuccess = false;
                response.Message = "Session not found";
            }

            return Ok(response);
        }
    }
}
