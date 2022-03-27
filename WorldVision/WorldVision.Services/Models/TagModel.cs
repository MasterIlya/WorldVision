using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WorldVision.Services.Models
{
    public class TagModel
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
