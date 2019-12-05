using System;
using System.Collections.Generic;
using System.Text;

namespace CarCamApp.Models
{
    public class WifiNetwork
    {
        public string SSID;
        public string Protocol;

        public WifiNetwork(string ssid, string protocol)
        {
            SSID = ssid;
            Protocol = protocol;
        }
    }
}
