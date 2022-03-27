using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WorldVision.Configuration;
using WorldVision.Hubs;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Http;

namespace WorldVision
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("3ecc3019-52f9-4f29-966b-33c32be15556")
                .Build();
            var appSettings = new ApplicationSettings();

            appSettings.Init(builder);
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
                    options.ClientId = appSettings.GoogleCredentials.ClientId;
                    options.ClientSecret = appSettings.GoogleCredentials.ClientSecret;
                })
                .AddFacebook(options =>
                {
                    options.AppId = appSettings.FacebookCredentials.AppId;
                    options.AppSecret = appSettings.FacebookCredentials.AppSecret;
                });
            services.AddAuthorization();
            services.AddSignalR();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization()
                .AddViewLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };

                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            services.AddMvc();
            services.AddControllersWithViews();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            new DependencyInjection(appSettings, services).Init();
        }

        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {

            app.UseRequestLocalization();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<CommentsHub>("/comments");
            });
        }
    }
}
