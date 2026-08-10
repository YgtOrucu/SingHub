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
        }
    }
}
