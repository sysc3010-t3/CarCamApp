using System;
namespace CarCamApp.Messages
{
    public class WifiConn : Message
    {
        public int UserID { get; set; }
        public string CarName { get; set; }
        public string SSID { get; set; }
        public string Password { get; set; }

        public WifiConn(int userID, string carName, string ssid, string pasword)
        {
            this.Type = MsgType.WIFI_CONN;
            this.UserID = userID;
            this.CarName = carName;
            this.SSID = ssid;
            this.Password = pasword;
        }
    }
}
