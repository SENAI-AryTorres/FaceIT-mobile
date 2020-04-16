﻿using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FaceIT.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            SetValue(NavigationPage.HasNavigationBarProperty, false);
        }

        private async void NavegarRegisterPage(object sender, EventArgs e)
        {
           await PopupNavigation.Instance.PushAsync(new RegisterPage());
           
        }

        private async void NavegarParaEsqueciSenha(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SimpleForgotPasswordPage());
        }

        private async void NavegarParaMenuPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.Menu());
        }
    }
}