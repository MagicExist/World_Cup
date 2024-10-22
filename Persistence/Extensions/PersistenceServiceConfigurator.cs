using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using World_Cup.DataBase;

namespace Persistence.Extensions
{
    public static class PersistenceServiceConfigurator
    {
        public static IServiceCollection PersistenceService(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddDbContext<WorldCupsDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));
            return service;
        }
    }
}
