using AdoDotNetProj.Manager;
using AdoDotNetProj.Manager.Interface;
using AdoDotNetProj.Repository;
using AdoDotNetProj.Repository.Interface;
using AdoDotNetProj.Services;
using AdoDotNetProj.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoDotNetProj
{
    public static class DiInstaller
    {
        public static void InstallTaskDi(this IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ILoginManager, LoginManager>();
        }
    }
}
