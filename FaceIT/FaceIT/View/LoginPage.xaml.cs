using FaceIT.AppConstant;
using FaceIT.AuthHelpers;
using FaceIT.Service;
using faceitapi.Models.ViewModel;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace FaceIT.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginService service = new LoginService();
        Account account;
        [Obsolete]
        AccountStore store;
        public LoginPage()
        {
            InitializeComponent();
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            store = AccountStore.Create();
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
                await DisplayAlert("Olá","Seja Bem-Vindo ao FaceIT", "Ok");
            }
            else
            {
                await DisplayAlert("Erro", "E-mail ou Senha Inexistente/Incorreto", "Ok");
            }
        }
        private void btnLoginGoogle_Clicked(object sender, EventArgs e)
        {
            string clientId = null;
            string redirectUri = null;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.GoogleiOSClientId;
                    redirectUri = Constants.GoogleiOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.GoogleAndroidClientId;
                    redirectUri = Constants.GoogleAndroidRedirectUrl;
                    break;
            }
            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                Constants.GoogleScope,
                new Uri(Constants.GoogleAuthorizeUrl),
                new Uri(redirectUri),
                new Uri(Constants.GoogleAccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }
        [Obsolete]
        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }
            if (e.IsAuthenticated)
            {
                User user = null;
                var request = new OAuth2Request("GET", new Uri(Constants.GoogleUserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();

                if (response != null)
                {
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<User>(userJson);
                }

                if (account != null)
                {
                    store.Delete(account, Constants.AppName);
                }
                user.Id = user.Id.Substring(0, 10);

                var loginRequest = new LoginGet();
                loginRequest.Email = user.Email.ToString();
                loginRequest.GoogleId = Convert.ToInt32(user.Id);

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
                    var cadastro = new View.PessoaFisicaCadastroGoogle(user);
                    await Navigation.PushAsync(cadastro);
                    await DisplayAlert("Aviso", "Antes de acessar, você precisa preencher alguns dados.", "Ok");
                }
            }
        }
        [Obsolete]
        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }
            Debug.WriteLine("Authentication error: " + e.Message);
        }
    }
}