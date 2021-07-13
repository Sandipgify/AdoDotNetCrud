using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoDotNetProj.Installer
{
    public static class InstallService
    {
        public static void InstallAllServices( this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly
                             .GetExportedTypes()
                                 .Where(y => typeof(IInstaller).IsAssignableFrom(y) && !y.IsInterface && !y.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            foreach (var installer in installers)
            {
                installer.InstallServices(services, configuration);
            }
        }
    }
}
