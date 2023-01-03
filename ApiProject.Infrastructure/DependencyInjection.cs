using Microsoft.Extensions.DependencyInjection;
using ApiProject.Infrastructure.Repository;
using ApiProject.Infrastructure.Repository.AnimeRepository;
using Microsoft.EntityFrameworkCore;
using ApiProject.Infrastructure.CSVDataInsertion;
using ApiProject.Infrastructure.Repository.RefreshTokenRepository;
using Microsoft.EntityFrameworkCore.Metadata;
using ApiProject.Infrastructure.Repository.UserRepository;

namespace ApiProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApiAppContext>();
            services.AddTransient<IAnimeRepository,AnimeRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}