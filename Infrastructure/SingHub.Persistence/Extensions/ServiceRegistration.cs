using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Options;
using SingHub.Domain.Entities;
using SingHub.Persistence.Concrete;
using SingHub.Persistence.Context;
using SingHub.Persistence.IdentityErrors;
using SingHub.Persistence.Seeders;
using System.Text;

namespace SingHub.Persistence.Extensions;

public static class ServiceRegistration
{
    public static void AppPersistenceSetting(this IServiceCollection services, IConfiguration builder)
    {
        //services.AddScoped<AuditDbContextInterceptors>();

        services.AddDbContext<SingHubContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(builder.GetConnectionString("DefaultConnection"));
        });

        services.AddIdentity<AppUser, AppRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireNonAlphanumeric = false;
        })
        .AddEntityFrameworkStores<SingHubContext>()
        .AddDefaultTokenProviders()
        .AddErrorDescriber<TurkishIdentityError>();


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
        {
            var jwttokenpotions = builder.GetSection(nameof(JwtTokenOptions)).Get<JwtTokenOptions>();

            opt.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwttokenpotions.Issuer,
                ValidAudience = jwttokenpotions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwttokenpotions.Key)),
                ClockSkew = TimeSpan.Zero,
            };
        });


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IMailService, MailService>();

    }

    public static async Task UseDbSeederAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider;
        await RoleSeeder.SeedRolesAndAdminUserAsync(service);
    }
}
