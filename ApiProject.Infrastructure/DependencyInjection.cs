using Microsoft.Extensions.DependencyInjection;
using ApiProject.Infrastructure.Repository;
using ApiProject.Infrastructure.Repository.AnimeRepository;

namespace ApiProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IAnimeRepository,AnimeRepository>();
            return services;
        }
    }
}