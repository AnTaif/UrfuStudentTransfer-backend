using Microsoft.Extensions.DependencyInjection;
using StudentTransfer.Logic.Services;

namespace StudentTransfer.Logic;

public static class DependencyInjectionHelper
{
    public static IServiceCollection AddLogicLayer(this IServiceCollection services)
    {
        services.AddTransient<IVacantService, VacantService>();
        return services;
    }
}