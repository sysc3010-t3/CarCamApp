using CarCamApp.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarCamApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void SignInProcedure(object sender, EventArgs e)
        {
            User user = new User(Entry_Username.Text, Entry_Password.Text);
            try
            {
                user.Login();
                Application.Current.MainPage = new NavigationPage(new Dashboard(user));
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

        async void SignUpProcedure(object sender, EventArgs e)
        {
            User user = new User(Entry_Username.Text, Entry_Password.Text);
            try
            {
                user.Register();
                Application.Current.MainPage = new NavigationPage(new Dashboard(user));
            }
            catch (ServerUnreachableException)
            {
                await DisplayAlert("Sign Up", "Server is unreachable. Try again later.", "OK");
            }
            catch (Exception exception)
            {
                await DisplayAlert("Sign Up", exception.Message, "OK");
            }
        }
    }
}