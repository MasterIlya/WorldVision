using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WorldVision.Configuration;

namespace WorldVision
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();
            services.AddHttpContextAccessor();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Authorization/Authentication");
                    options.ExpireTimeSpan = new System.TimeSpan(1, 0, 0, 0);
                })
                .AddGoogle(options =>
                {
                    options.ClientId = "xxx";
                    options.ClientSecret = "xxx";
                })
                .AddFacebook(options =>
                {
                    options.AppId = "xxx";
                    options.AppSecret = "xxx";
                });
            services.AddAuthorization();
            services.AddControllersWithViews();

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var appSettings = new ApplicationSettings();
            appSettings.Init(builder);

            new DependencyInjection(appSettings, services).Init();
        }

        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
