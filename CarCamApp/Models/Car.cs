using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CarCamApp.Models
{
    [JsonObject(
        ItemNullValueHandling = NullValueHandling.Ignore,
        NamingStrategyType = typeof(Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy)
    )]
    public class Car
    {
        public int ID;
        public string Name;
        public bool IsOn;
    }
}
