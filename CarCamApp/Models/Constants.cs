using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CarCamApp.Models
{
    public class Constants
    {
        public static bool isDev = true;
        public static Color BackgroundColour = Color.FromRgb(58,153,215);
        public static Color MainTextColour = Color.White;
        public static string ServerIP = "192.168.43.19"; // Change to actual server IP
        public static int ServerPort = 6006;
        public static int ServerStreamPort = 5000;
        public static string CarAccessPointIP = "192.168.4.1";
        public static int CarAccessPointPort = 5005;
        public static int TIMEOUT = 5000;
        public static int MAX_ATTEMPTS = 3;
    }
}
