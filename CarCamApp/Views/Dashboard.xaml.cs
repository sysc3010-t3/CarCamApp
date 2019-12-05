using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarCamApp.Models;
using CarCamApp.Messages;

namespace CarCamApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        private User _User;

        public Dashboard(User user)
        {
            this._User = user;

            NavigationPage.SetTitleView(this, new FlexLayout
            {
                Direction = FlexDirection.Row,
                AlignContent = FlexAlignContent.Center,
                AlignItems = FlexAlignItems.Center,
                JustifyContent = FlexJustify.SpaceBetween,
                Padding = new Thickness(10, 0),
                Children =
                {
                    new Label
                    {
                        Text = "RC Camera Car",
                        TextColor = Color.White,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 24
                    },
                    new Button
                    {
                        Text = "Log Out",
                        FontAttributes = FontAttributes.Bold,
                        ImageSource = "log_out.png",
                        TextColor = Color.White,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Command = new Command(() => OnLogOut()),
                        BackgroundColor = Color.Transparent,
                        ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Right, 20)
                    }
                }
            });

            StackLayout carList = new StackLayout();

            BindableLayout.SetItemTemplate(carList, new DataTemplate(() =>
            {
                Button carBtn = new Button {
                    BackgroundColor = Color.Transparent,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold
                };
                carBtn.SetBinding(Button.TextProperty, "Name");
                carBtn.SetBinding(IsEnabledProperty, "IsOn");
                carBtn.SetBinding(Button.CommandParameterProperty, "ID");

                carBtn.Clicked += OnCarSelect;

                return carBtn;
            }));

            RefreshView refresh = new RefreshView
            {
                Content = new ScrollView { Content = carList }
            };

            refresh.Command = new Command(() =>
            {
                RefreshCars(carList, refresh);
            });

            RefreshCars(carList, refresh);

            this.Content = new FlexLayout
            {
                Direction = FlexDirection.Column,
                JustifyContent = FlexJustify.SpaceBetween,
                Padding = new Thickness(20),
                Children =
                {
                    new StackLayout
                    {
                        Children = {
                            new FlexLayout
                            {
                                Direction = FlexDirection.Row,
                                JustifyContent = FlexJustify.SpaceBetween,
                                AlignItems = FlexAlignItems.Center,
                                Margin = new Thickness(0, 0, 0, 10),
                                Children =
                                {
                                    new Label
                                    {
                                        Text = "Your Cars",
                                        TextColor = Color.Black,
                                        FontSize = 24,
                                        FontAttributes = FontAttributes.Bold
                                    },
                                    new ImageButton
                                    {
                                        Source = "add.png",
                                        BackgroundColor = Color.Transparent,
                                        Scale = 0.5,
                                        Command = new Command(() => OnRegisterCar())
                                    }
                                }
                            },
                            refresh
                        }
                    },
                    new Label
                    {
                        Text = "Press the + button to register a new car!",
                        FontSize = 18,
                        TextColor = Color.DarkGray,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        Margin = new Thickness(0, 20)
                    }
                }
            };
        }

        async private Task RefreshCars(StackLayout carList, RefreshView refresh)
        {
            SocketClient client;

            try
            {
                client = new SocketClient(Constants.ServerIP, Constants.ServerPort);
            }
            catch (Exception e)
            {
                refresh.IsRefreshing = false;
                await DisplayAlert("Car Refresh", e.Message, "OK");
                return;
            }

            client.SetRecvTimeout(Constants.TIMEOUT);

            int attempts = 0;

            while (attempts < Constants.MAX_ATTEMPTS)
            {
                client.Send(new GetCars(this._User.ID));

                Message resp = client.Receive();

                if (resp is Error error)
                {
                    client.Close();
                    refresh.IsRefreshing = false;
                    await DisplayAlert("Car Refresh", error.ErrorMsg, "OK");
                    return;
                }

                if (resp is Ack ack && ack.Cars != null)
                {
                    client.Close();
                    BindableLayout.SetItemsSource(carList, ack.Cars);
                    refresh.IsRefreshing = false;
                    return;
                }

                attempts++;
            }

            client.Close();
            refresh.IsRefreshing = false;
            await DisplayAlert("Car Refresh", "Server is unreachable. Try again later.", "OK");
            return;
        }

        private void OnRegisterCar()
        {
            Navigation.PushAsync(new ConnectCar(_User));
        }

        private void OnCarSelect(object sender, EventArgs args)
        {
            Button carBtn = (Button)sender;

            int carID = (int)carBtn.CommandParameter;

            SocketClient client;

            try
            {
                client = new SocketClient(Constants.ServerIP, Constants.ServerPort);
            }
            catch (Exception e)
            {
                DisplayAlert("Car Link", e.Message, "OK");
                return;
            }

            client.SetRecvTimeout(Constants.TIMEOUT);

            int attempts = 0;

            while (attempts < Constants.MAX_ATTEMPTS)
            {
                client.Send(new Link(carID, this._User.ID));

                Message resp = client.Receive();

                if (resp is Error error)
                {
                    client.Close();
                    DisplayAlert("Car Link", error.ErrorMsg, "OK");
                    return;
                }

                if (resp is Ack)
                {
                    client.Close();
                    Navigation.PushAsync(new VideoStreamPage(carID));
                    return;
                }

                attempts++;
            }

            client.Close();
            DisplayAlert("Car Link", "Server is unreachable. Try again later.", "OK");
            return;
        }

        private void OnLogOut()
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}