using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SignHub.Application.Contract.Persistence;
using SignHub.Application.Options;
using SignHub.Domain.Entities;
using SignHub.Persistence.Concreate;
using SignHub.Persistence.Context;
using SignHub.Persistence.IdentityErrors;
using SignHub.Persistence.Seeders;
using System.Text;

namespace SignHub.Persistence.Extensions;

public static class ServiceRegistration
{
    public static void AppPersistenceSetting(this IServiceCollection services, IConfiguration builder)
    {
        //services.AddScoped<AuditDbContextInterceptors>();

        services.AddDbContext<SignHubContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(builder.GetConnectionString("DefaultConnection"));
        });

        services.AddIdentity<AppUser, AppRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireNonAlphanumeric = false;
        })
        .AddEntityFrameworkStores<SignHubContext>()
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
