using Balances.Web.Services.Contracts;
using System.Net.Http.Json;

namespace Balances.Web.Services.Implementation
{
    public class ReCaptchaClientService : IReCaptchaClientService
    {
        private readonly HttpClient _httpClient;

        public ReCaptchaClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ValidarCaptcha(string token)
        {
            var response = await _httpClient.PostAsJsonAsync("api/recaptcha/validate", new { Token = token });

            if (response.IsSuccessStatusCode)
            {
                var captchaResponse = await response.Content.ReadFromJsonAsync<CaptchaResponse>();
                return captchaResponse.success;
            }

            return false;
        }
    }



    public class CaptchaResponse
    {
        public bool success { get; set; }
        public string challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}

