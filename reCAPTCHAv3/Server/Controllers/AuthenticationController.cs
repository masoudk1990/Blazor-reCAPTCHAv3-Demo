using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using reCAPTCHAv3.Server.Infrastructure.Settings;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace reCAPTCHAv3.Server.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ReCaptchaSettings _reCaptchaSettings;

        public AuthenticationController(HttpClient httpClient, IOptions<ReCaptchaSettings> reCaptchaSettings)
        {
            _httpClient = httpClient;
            _reCaptchaSettings = reCaptchaSettings.Value;
        }

        [HttpGet("reverifycaptcha")]
        public async Task<IActionResult> RreVerifyCaptcha(string token)
        {
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("secret", _reCaptchaSettings.secretKey), new KeyValuePair<string, string>("response", token) });
            var response = await _httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify", content);

            var jsonString = await response.Content.ReadAsStringAsync();
            return Ok(jsonString);
        }
    }
}
