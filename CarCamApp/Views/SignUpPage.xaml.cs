using CarCamApp.Models;
using CarCamApp.Views.Menu;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarCamApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        async void SignUpProcedure(object sender, EventArgs e)
        {
            User user = new User(Entry_Username.Text, Entry_Password.Text);
            try
            {
                user.Register();
                Application.Current.MainPage = new NavigationPage(new MasterDetail());
            }
            catch (ServerUnreachableException)
            {
                await DisplayAlert("Login", "Server is unreachable. Try again later.", "OK");
            }
            catch (Exception exception)
            {
                await DisplayAlert("Login", exception.Message, "OK");
            }
        }

    }
}