using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldVision.Services.Configuration
{
    public class CloudinaryCredentials : ICloudinaryCredentials
    {
        public string Key { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
    }
}
