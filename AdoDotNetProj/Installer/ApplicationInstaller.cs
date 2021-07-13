using Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User;

namespace AdoDotNetProj.Installer
{
    public class ApplicationInstaller:IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.InstallDatabaseDi(configuration);
            services.InstallUserDi();
            services.InstallTaskDi();
        }
        }
}
