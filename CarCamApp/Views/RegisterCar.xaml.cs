using System;
using CarCamApp.Messages;
using CarCamApp.Models;
using CarCamApp.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarCamApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterCar : ContentPage
    {
        private User user;
        private WifiNetwork network;
        private bool requirePassword;

        public RegisterCar(User user, WifiNetwork network)
        {
            InitializeComponent();
            this.user = user;
            this.network = network;
            labelSSID.Text = network.SSID;

            requirePassword = true;
            if (network.Protocol == null || network.Protocol == "") {
                requirePassword = false;
                entryWifiPass.IsEnabled = false;
                entryWifiPass.BackgroundColor = Color.LightGray;
                labelWifiPass.Text = "Wi-Fi Password (N/A):";
            }
        }

        async void RegisterProcedure(object sender, EventArgs e)
        {
            if (requirePassword && (entryWifiPass.Text == null || entryWifiPass.Text == ""))
            {
                await DisplayAlert("Register Failed", "Wi-Fi Password field is empty.", "OK");
                return;
            }
            else if (entryCarName.Text == null || entryCarName.Text == "")
            {
                await DisplayAlert("Register Failed", "Car Name field is empty.", "OK");
                return;
            }

            try
            {
                connectCarToWifi();
                Application.Current.MainPage = new NavigationPage(new Dashboard(user));
            }
            catch (ServerUnreachableException)
            {
                await DisplayAlert("Register Failed", "Server is unreachable. Try again later.", "OK");
            }
            catch (Exception exception)
            {
                await DisplayAlert("Register Failed", exception.Message, "OK");
            }
        }

        private void connectCarToWifi()
        {
            WifiConn msg;
            if (requirePassword)
            {
                msg = new WifiConn(user.ID, entryCarName.Text, network.SSID, entryWifiPass.Text);
            }
            else
            {
                msg = new WifiConn(user.ID, entryCarName.Text, network.SSID);
            }

            SocketClient client;

            try
            {
                client = new SocketClient(Constants.CarAccessPointIP, Constants.CarAccessPointPort);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to create UDP client: " + e.Message);
            }

            client.SetRecvTimeout(2000); // TODO: USE CONSTANT
            int attempts = 0;
            while (attempts < 10) // TODO: USE CONSTANT
            {
                client.Send(msg);

                Message resp = client.Receive();

                if (resp is Error error)
                {
                    client.Close();
                    throw new ArgumentException(error.ErrorMsg);
                }

                if (resp is Ack)
                {
                    client.Close();
                    return;
                }

                attempts++;
            }

            client.Close();
            throw new ServerUnreachableException();
        }
    }
}