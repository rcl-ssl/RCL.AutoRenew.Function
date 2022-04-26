using RCL.AutoRenew.Function;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiRequestExtension
    {
        public static IServiceCollection AddApiRequestService(this IServiceCollection services, Action<ApiOptions> setupAction)
        {
            services.AddTransient<ICertificateRequestService, CertificateRequestService>();
            services.Configure(setupAction);

            return services;
        }
    }
}
