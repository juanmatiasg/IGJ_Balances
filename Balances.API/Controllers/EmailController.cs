using Dominio.Helpers;
using EmailSender;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSenderService _emailSenderService;

        public EmailController(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        [HttpGet]
        [Route("mail")]
        public async Task<IActionResult> PostEmail(string email)
        {
            MailRequest mailRequest = new MailRequest();
            mailRequest.To = email;

            await _emailSenderService.SendEmailAsync(mailRequest);
            return Ok();
        }
    }
}

