using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace StudentTransfer.Dal;

public static class DependencyInjectionHelper
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<StudentTransferContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }
}