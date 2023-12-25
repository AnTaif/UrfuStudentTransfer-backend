using Microsoft.Extensions.DependencyInjection;
using StudentTransfer.Bll.Services.Vacant;
using StudentTransfer.Bll.Services;
using StudentTransfer.Bll.Services.Application;
using StudentTransfer.Bll.Services.Auth;
using StudentTransfer.Bll.Services.Auth.JwtToken;
using StudentTransfer.Bll.Services.Auth.User;
using StudentTransfer.Bll.Services.File;
using StudentTransfer.Bll.Services.StatusServices;
using StudentTransfer.Dal;

namespace StudentTransfer.Bll;

public static class DependencyInjectionHelper
{
    public static IServiceCollection AddLogicLayer(this IServiceCollection services, string contentRootPath)
    {
        services.AddTransient<IVacantService, VacantService>();
        services.AddTransient<IApplicationService, ApplicationService>();
        services.AddTransient<IFileService, RootFileService>(provider =>
        {
            var context = provider.GetRequiredService<StudentTransferContext>();
            return new RootFileService(context, Path.Combine(contentRootPath, "Uploads"));
        });
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddTransient<IStatusService, StatusService>();
        
        return services;
    }
}