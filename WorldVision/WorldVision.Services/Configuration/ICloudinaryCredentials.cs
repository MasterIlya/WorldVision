using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldVision.Services.Configuration
{
    public interface ICloudinaryCredentials
    {
        public string Key { get; }
        public string Secret { get; }
        public string Name { get; }
    }
}
