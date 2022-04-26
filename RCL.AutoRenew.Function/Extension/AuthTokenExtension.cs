using RCL.AutoRenew.Function;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthTokenExtension
    {
        public static IServiceCollection AddAuthTokenService(this IServiceCollection services, Action<AuthorizationOptions> setupAction)
        {
            services.AddTransient<IAuthTokenService, AuthTokenService>();
            services.Configure(setupAction);

            return services;
        }
    }
}
