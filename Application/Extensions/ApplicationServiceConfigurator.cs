using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationServiceConfigurator
    {
        public static IServiceCollection ApplicationService(this IServiceCollection service)
        {
            service.AddTransient<WorldCupService>();
            return service;
        }
    }
}
