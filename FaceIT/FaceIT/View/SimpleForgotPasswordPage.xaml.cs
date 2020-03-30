using FaceIT.Page;
using FaceIT.Views.Forms;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FaceIT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleForgotPasswordPage : ContentPage
    {
        public SimpleForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SimpleResetPasswordPage());
        }

        private async void ToRegisterPage(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new RegisterPage());
        }
    }
}