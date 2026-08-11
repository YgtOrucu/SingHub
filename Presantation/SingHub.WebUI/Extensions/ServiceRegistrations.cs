using Microsoft.AspNetCore.Authentication.Cookies;

namespace SingHub.WebUI.Extensions
{
    public static class ServiceRegistrations
    {
        public static void UIServiceRegister(this IServiceCollection services, IConfiguration builder)
        {
            services.AddHttpClient("SingHubAPI", opt =>
            {
                var address = builder.GetSection("ApıAddress").Value;
                if (address == null)
                    throw new Exception("The ApıAddress could not be found");

                opt.BaseAddress = new Uri(address);
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "SingHub.AuthCookie";
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
                options.AccessDeniedPath = "/Auth/AccessDenied";

                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;

                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
            });
        }
    }
}
