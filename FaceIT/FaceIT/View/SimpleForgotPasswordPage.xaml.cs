using FaceIT.Page;
using FaceIT.Service;
using FaceIT.Views.Forms;
using faceitapi.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FaceIT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleForgotPasswordPage : ContentPage
    {
        RecuperarSenha_Service service = new RecuperarSenha_Service();
        public SimpleForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var recuperar = email_entry.Text;
            var result = service.Recuperar(recuperar);
            if(await result)
            {
                await DisplayAlert("E-mail Enviado", "Cheque seu E-mail para sua nova Senha", "Ok");
            }
            else
            {
                await DisplayAlert("Erro", "E-mail Inexistente/Incorreto", "Ok");
            }
        }

        private async void ToRegisterPage(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new RegisterPage());
        }
    }
}