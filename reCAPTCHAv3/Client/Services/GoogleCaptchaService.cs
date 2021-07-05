using reCAPTCHAv3.Shared.Entities.DataTransferObjects;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace reCAPTCHAv3.Client.Services
{
    public class GoogleCaptchaService
    {
        HttpClient httpClient;
        public GoogleCaptchaService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public virtual async Task<GoogleCaptchaResponseDto> reVerify(string token)
        {
            var response = await httpClient.GetAsync("api/authentication/reverifycaptcha?token=" + token);
            var captchaResponse = JsonSerializer.Deserialize<GoogleCaptchaResponseDto>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return captchaResponse;
        }
    }
}
