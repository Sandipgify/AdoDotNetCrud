using Database.Provider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class DiInstaller
    {
        public static void InstallDatabaseDi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbAccessManager, DbAccessManager>();
        }
    }
}
