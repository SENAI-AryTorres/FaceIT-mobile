using FaceIT.View;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace FaceIT.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : PopupPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void CloseRegisterPage(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.RemovePageAsync(this);
        }

        private async void ToPessoaFisicaPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PessoaFisicaCadastroPage());
        }

        private async void ToPessoaJuridicaPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PessoaJuridicaCadastroPage());
        }

        
    }
}