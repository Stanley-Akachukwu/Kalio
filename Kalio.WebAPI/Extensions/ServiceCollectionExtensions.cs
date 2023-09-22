using Kalio.Core.Defaults.Weather;
using Kalio.Core.Services.Users;
using Kalio.Core.Services.Users.Implementation;
using Kalio.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Kalio.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKalioDatabaseConnections(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<KalioDbContext>(options => options.UseSqlServer(configuration["Data:KalioConnection:ConnectionString"]));
            services.AddDbContext<KalioIdentityDbContext>(options => options.UseSqlServer(configuration["Data:KalioIdentityConnection:ConnectionString"]));
            return services;
        }

        public static IServiceCollection AddKalioLibraries(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(WeatherCommandHandler).Assembly);
            });
            services.AddAutoMapper(typeof(WeatherCommandHandler).Assembly);
            return services;
        }
        public static IServiceCollection AddAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<KalioIdentityDbContext>()
                .AddDefaultTokenProviders();
            //Authentication
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidIssuer = configuration["Jwt:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecreteKey"])),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false,
                };
            });

            return services;
        }
        public static IServiceCollection AddAuthorization(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuthorization();
            return services;
        }

        public static IServiceCollection AddKalioServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IConfiguration>();
            return services;
        }
    }
}
