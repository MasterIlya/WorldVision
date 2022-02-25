using Microsoft.Extensions.Configuration;

namespace WorldVision.Configuration
{
    public class ApplicationSettings
    {
        public string ConnectionString { get; set; }

        public void Init(IConfiguration configuration)
        {
            ConnectionString = configuration[nameof(ConnectionString)];
        }
    }
}
