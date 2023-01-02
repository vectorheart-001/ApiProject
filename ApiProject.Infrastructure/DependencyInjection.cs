using Microsoft.Extensions.DependencyInjection;
using ApiProject.Infrastructure.Repository;
using ApiProject.Infrastructure.Repository.AnimeRepository;
using Microsoft.EntityFrameworkCore;
using ApiProject.Infrastructure.CSVDataInsertion;

namespace ApiProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApiAppContext>();
            services.AddTransient<IAnimeRepository,AnimeRepository>();
            return services;
        }
    }
}