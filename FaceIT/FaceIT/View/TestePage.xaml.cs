using FaceIT.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FaceIT.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestePage : ContentPage
    {
        public TestePage()
        {
            InitializeComponent();
        }

        private async void GoToSettingsPage(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void GoToFaqPage(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new FaqPage());
        }

        private async void GoToEditProfile(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new EditProfile());
        }

        private async void Sair(object sender, EventArgs e)
        {
            var choice = await DisplayAlert("", "Deseja Realmente Sair", "Sim", "Não");
            if (choice)
            {
                await Navigation.PushAsync(new LoginPage());
            }
        }
    }
}