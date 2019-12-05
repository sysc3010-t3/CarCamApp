using System;
using System.Collections.Generic;
using CarCamApp.Messages;
using CarCamApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarCamApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectCar : ContentPage
    {
        public ConnectCar()
        {
            InitializeComponent();
        }

        async void ConnectProcedure(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> networks = getWiFiList(); // TODO: HANDLE EXCEPTION
                wifiListDisplay.Children.Clear();
                foreach (KeyValuePair<string, string> network in networks)
                {
                    Button button = new Button
                    {
                        Text = network.Key,
                        Margin = new Thickness(0)
                    };
                    button.Clicked += networkClicked;
                    button.CommandParameter = new WifiNetwork(network.Key, network.Value);
                    wifiListDisplay.Children.Add(button);
                }
            }
            catch (ServerUnreachableException)
            {
                await DisplayAlert("Connect Failed", "Server is unreachable. Try again later or try a different Wi-Fi network.", "OK");
            }
            catch (Exception exception)
            {
                await DisplayAlert("Connect Failed", exception.Message, "OK");
            }
        }

        async private void networkClicked(object sender, EventArgs args)
        {
            Button btn = (Button) sender;
            WifiNetwork network = (WifiNetwork) btn.CommandParameter;
            await Navigation.PushAsync(new RegisterCar(network));
        }

        private Dictionary<string, string> getWiFiList()
        {
            GetSSID msg = new GetSSID();

            SocketClient client;

            try
            {
                client = new SocketClient(Constants.CarAccessPointIP, Constants.CarAccessPointPort);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to create UDP client: " + e.Message);
            }

            client.SetRecvTimeout(5000); // TODO: USE CONSTANT
            int attempts = 0;
            while (attempts < 3) // TODO: USE CONSTANT
            {
                client.Send(msg);

                Message resp = client.Receive();

                if (resp is Error error)
                {
                    client.Close();
                    throw new ArgumentException(error.ErrorMsg);
                }

                if (resp is Ack ack && ack.Networks != null)
                {
                    client.Close();
                    return ack.Networks;
                }

                attempts++;
            }

            client.Close();
            throw new ServerUnreachableException();
        }
    }
}