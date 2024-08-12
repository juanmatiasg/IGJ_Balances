using Balances.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReCaptchaController : Controller
    {
        private readonly IReCaptchaService _ReCaptchaService;

        public ReCaptchaController(IReCaptchaService ReCaptchaService)
        {
            _ReCaptchaService = ReCaptchaService;
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateCaptcha([FromBody] CaptchaRequest request)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return BadRequest("El token de reCAPTCHA es requerido.");
            }

            var isValid = await _ReCaptchaService.Validate(request.Token);
            if (isValid)
            {
                return Ok(new { success = true });
            }
            else
            {
                return BadRequest("reCAPTCHA no válido.");
            }
        }

        public class CaptchaRequest
        {
            public string Token { get; set; }
        }
    }
}
