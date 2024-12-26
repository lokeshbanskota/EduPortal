using EduPortal.Services.Implementations;
using EduPortal.Services.Interfaces;
using System.ComponentModel.Design;

namespace EduPortal.Middleware
{
    public static class BusinessDIMiddleware
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            return services;
        }
    }
}
