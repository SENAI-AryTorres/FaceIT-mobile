using FaceIT.Service;
using FaceIT.View;
using faceitapi.Models;
using faceitapi.Models.ViewModel;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Markup;
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

        protected override bool OnBackButtonPressed() => true;
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
            
            var result = await service.Logar(loginRequest);
            if (result != null)
            {
                var pessoa = result.pessoa;
                Security.Security.TokenValue = result.token.Value;

                var pagina = new View.Menu(result.pessoa, result.token)
                {
                    BindingContext = result.pessoa
                };
                await Navigation.PushAsync(pagina);
                await DisplayAlert("Olá", "Seja Bem-Vindo ao FaceIT", "Ok");
            }
            else
            {
                await DisplayAlert("Erro", "E-mail ou Senha Inexistente/Incorreto", "Ok");
            }
        }
    }
}