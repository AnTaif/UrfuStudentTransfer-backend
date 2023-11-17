using Microsoft.Extensions.DependencyInjection;
using StudentTransfer.Bll.Services.Vacant;
using StudentTransfer.Bll.Services;

namespace StudentTransfer.Bll;

public static class DependencyInjectionHelper
{
    public static IServiceCollection AddLogicLayer(this IServiceCollection services)
    {
        services.AddTransient<IVacantService, VacantService>();
        return services;
    }
}