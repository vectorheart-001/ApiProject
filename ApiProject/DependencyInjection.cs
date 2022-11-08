using ApiProject.Api.Authentication.TokenValidators;

namespace ApiProject.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<ValidateRefreshToken>();
            return services;
        }
    }
}
