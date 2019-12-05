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
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsOn { get; set; }

        public Car(int id, string name, bool isOn)
        {
            ID = id;
            Name = name;
            IsOn = isOn;
        }
    }
}
