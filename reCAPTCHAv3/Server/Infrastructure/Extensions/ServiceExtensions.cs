using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using reCAPTCHAv3.Server.Infrastructure.Settings;

namespace reCAPTCHAv3.Server.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void ApplyConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ReCaptchaSettings>(configuration.GetSection("ReCaptchaSettings"));
        }
    }
}
