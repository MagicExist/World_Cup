using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using World_Cup.DataBase;

namespace Persistence.Extensions
{
    public static class PersistenceServiceConfigurator
    {
        public static IServiceCollection PersistenceService(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddDbContext<WorldCupsDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));
            service.AddScoped<IWorldCup, WorldCup>();
            return service;
        }
    }
}
