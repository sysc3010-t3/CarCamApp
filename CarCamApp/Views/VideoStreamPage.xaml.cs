using System;

using CarCamApp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarCamApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoStreamPage : ContentPage
    {
        public static bool linked = false;

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

        protected override void OnAppearing()
        {
            linked = true;
            base.OnAppearing();
        }

        protected override void OnDisappearing() {
            linked = false;
            base.OnDisappearing();
        }
    }
}