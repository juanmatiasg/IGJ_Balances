namespace Balances.Web.Services.Contracts
{
    public interface IReCaptchaClientService
    {
        Task<bool> ValidarCaptcha(string captcha);
    }
}
