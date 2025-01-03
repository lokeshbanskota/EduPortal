using EduPortal.DBContext;
using Microsoft.AspNetCore.Identity;
using Services.Authentication;
using Services.Implementations;
using Services.Interfaces;
namespace EduPortal.Middleware
{
    public static class BusinessDIMiddleware
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services, IConfiguration config)
        {
            // For Identity--AuthenticateController
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            //Jwt
            services.AddJwtAuthentication(config);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "EduPortal", Version = "v1" });

                // Add Bearer Token Authentication
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            return services;
        }
    }
}
