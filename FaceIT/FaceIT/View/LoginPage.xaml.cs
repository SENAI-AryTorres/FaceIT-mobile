using FaceIT.Service;
using faceitapi.Models;
using faceitapi.Models.ViewModel;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FaceIT.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginService service = new LoginService();

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
            var loginRequest = new LoginGet();
            loginRequest.Email = entry_email.Text;
            loginRequest.Senha = entry_senha.Text;
            
            var result = service.Logar(loginRequest);
            if (await result)
            {
                await DisplayAlert("Olá", "Seja Bem-Vindo ao FaceIT", "Ok");
                await Navigation.PushAsync(new View.Menu());
            }
            else
            {
                await DisplayAlert("Erro", "E-mail ou Senha Inexistente/Incorreto", "Ok");
            }
        }
    }
}