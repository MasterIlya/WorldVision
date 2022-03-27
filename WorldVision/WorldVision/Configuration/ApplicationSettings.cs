using Microsoft.Extensions.Configuration;
using WorldVision.Services.Configuration;

namespace WorldVision.Configuration
{
    public class ApplicationSettings : IApplicationSettings 
    {
        public string ConnectionString { get; set; }
        public ICloudinaryCredentials CloudinaryCredentials { get; set; }
        public GoogleCredentials GoogleCredentials { get; set; }
        public FacebookCredentials FacebookCredentials { get; set; }

        public void Init(IConfiguration configuration)
        {
            ConnectionString = configuration[nameof(ConnectionString)];
            CloudinaryCredentials = configuration.GetSection(nameof(CloudinaryCredentials)).Get<CloudinaryCredentials>();
            GoogleCredentials = configuration.GetSection(nameof(GoogleCredentials)).Get<GoogleCredentials>();
            FacebookCredentials = configuration.GetSection(nameof(FacebookCredentials)).Get<FacebookCredentials>();
        }
    }
}
