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
        public static string ServerIP = "10.0.0.178"; // Change to actual server IP
        public static int ServerPort = 8080;
        public static int TIMEOUT = 5000;
        public static int MAX_ATTEMPTS = 3;
    }
}
