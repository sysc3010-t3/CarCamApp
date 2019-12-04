﻿using CarCamApp.Models;
using CarCamApp.Views.Menu;
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

        void SignUpProcedure(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new SignUpPage());
        }
    }
}