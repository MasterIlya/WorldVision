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
                    options.ClientId = "312938971846-ucjhqqefu0e4q8g1p96tomol2j2bn1re.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                })
                .AddFacebook(options =>
                {
                    options.AppId = "1116959762416997";
                    options.AppSecret = "2a5e7d043de4428a325568196bcab37d";
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
