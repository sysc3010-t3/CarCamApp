using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarCamApp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarCamApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoStreamPage : ContentPage
    {
        public VideoStreamPage(int carID)
        {
            InitializeComponent();
            string url = String.Format(
                "http://{0}:{1}/{2}/stream.mjpg",
                Constants.ServerIP,
                Constants.ServerStreamPort,
                carID);
            webView.Source = url;
            webView.VerticalOptions = LayoutOptions.Fill;
            webView.HorizontalOptions = LayoutOptions.Fill;
        }
    }
}