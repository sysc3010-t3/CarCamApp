using Newtonsoft.Json;
namespace CarCamApp.Messages
{
    [JsonObject(
        ItemNullValueHandling = NullValueHandling.Ignore,
        NamingStrategyType = typeof(Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy)
    )]
    public class Message
    {
        public enum MsgType
        {
            ACK = 0,
            ERROR = 1,
            REG_USER = 2,
            LOGIN = 3,
            GET_CARS = 4,
            LINK = 5,
            MOVE = 8,
            GET_SSID = 9,
            WIFI_CONN = 10,
            SET_LED = 11
        }

        public MsgType Type { get; set; }

        override public string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
