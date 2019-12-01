using CarCamApp.Models;
using CarCamApp.Views.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (user.CheckInformation())
            {
                DisplayAlert("Login", "Login Successful", "Logged in");
                Application.Current.MainPage = new NavigationPage(new MasterDetail());
            }
            else
            {
                DisplayAlert("Login", "Login Not Successful, empty username or password", "Logged in");
            }


        }

    }
}