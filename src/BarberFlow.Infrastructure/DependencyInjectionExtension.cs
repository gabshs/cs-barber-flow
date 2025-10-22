using BarberFlow.Domain.Repositories;
using BarberFlow.Domain.Repositories.Billings;
using BarberFlow.Infrastructure.DataAccess;
using BarberFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberFlow.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services, configuration);

    }

    public static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IBillingsReadOnlyRepository, BillingsRepository>();
        services.AddScoped<IBillingsWriteOnlyRepository, BillingsRepository>();
        services.AddScoped<IBillingUpdateOnlyRepository, BillingsRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 43));

        services.AddDbContext<BarberFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}