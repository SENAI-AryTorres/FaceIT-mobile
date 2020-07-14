using FaceIT.Page;
using faceitapi.Models;
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
        Pessoa _pessoa = new Pessoa();
        public TestePage(Pessoa pessoa)
        {
            InitializeComponent();
            if (pessoa.PessoaFisica == null)
            {
                juridiconome_entry.IsVisible = true;
                fisicanome_entry.IsVisible = false;
            }
            else if (pessoa.PessoaJuridica == null)
            {
                fisicanome_entry.IsVisible = true;
                juridiconome_entry.IsVisible = false;
            }
            if (pessoa.Imagem != null)
            {
                img_entry.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(pessoa.Imagem.Bytes));
            }
            _pessoa = pessoa;
            
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
            var pagina = new EditProfile(_pessoa)
            {
                BindingContext = _pessoa
            };
            await Navigation.PushAsync(new EditProfile(_pessoa));
        }

        private async void Sair(object sender, EventArgs e)
        {
            var choice = await DisplayAlert("", "Deseja Realmente Sair?", "Sim", "Não");
            if (choice)
            {
                await Navigation.PushAsync(new LoginPage());
            }
        }
    }
}