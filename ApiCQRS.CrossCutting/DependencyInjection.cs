using ApiCQRS.Domain.Interfaces;
using ApiCQRS.Infrastructure.Context;
using ApiCQRS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace ApiCQRS.CrossCutting
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
          IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext)
                            .Assembly.FullName)));            

            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            services.AddScoped<IUserReadRepository, UserReadRespository>();
            services.AddScoped<IUserReadHOTRepository, UserReadHOTRepository>();

            services.AddTransient<MySqlConnection>(_ => new MySqlConnection(configuration.GetConnectionString("MysqlConnection")));

            var mcc = new MysqlConnectConfiguration {
                MysqlUrl = configuration.GetConnectionString("MysqlConnection")
            };

            services.AddSingleton(mcc);

            return services;
        }

    }
}
