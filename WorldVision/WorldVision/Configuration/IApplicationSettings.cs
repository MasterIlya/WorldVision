using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldVision.Services.Configuration;

namespace WorldVision.Configuration
{
    public interface IApplicationSettings
    {
        string ConnectionString { get; set; }
        ICloudinaryCredentials CloudinaryCredentials { get; set; }
        GoogleCredentials GoogleCredentials { get; set; }
        FacebookCredentials FacebookCredentials { get; set; }
    }
}
