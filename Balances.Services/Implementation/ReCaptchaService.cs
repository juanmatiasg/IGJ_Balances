using Balances.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Balances.Services.Implementation
{
    public class ReCaptchaService : IReCaptchaService
    {
        private readonly HttpClient _httpClient;
        private const string SecretKey = "6LfVmSEqAAAAABssVNfpYHYYKdzR-nNh6A-LOV0B";

        public ReCaptchaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Validate(string token)
        {
            var response = await _httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={SecretKey}&response={token}", null);
            var jsonString = await response.Content.ReadAsStringAsync();
            var captchaResponse = JsonSerializer.Deserialize<CaptchaResponse>(jsonString);
            return captchaResponse.success;
        }

        private class CaptchaResponse
        {
            public bool success { get; set; }
            public string challenge_ts { get; set; }
            public string hostname { get; set; }
        }
    }
}
