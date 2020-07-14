using FaceIT.Page;
using faceitapi.Models;
using faceitapi.Models.ViewModel;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FaceIT.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : MasterDetailPage
    {
        private Pessoa _pessoa = new Pessoa();

        public Menu(Pessoa pessoa, Token token)
        {
            InitializeComponent();
            Detail = new NavigationPage(new MenuDetail(pessoa));
            if (pessoa.PessoaJuridica == null)
            {
                fisicanome_entry.IsVisible = true;
            }
            else if (pessoa.PessoaFisica == null)
            {
                juridicanome_entry.IsVisible = true;
            }
            if (pessoa.Imagem != null)
            {
                image_entry.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(pessoa.Imagem.Bytes));
            }
            if (pessoa.Anexo != null)
            {
                _anexo.Text = pessoa.Anexo.Nome;
            }
            if (pessoa.Tipo == "PF")
            {
                MenuPessoaFisica.IsVisible = true;
                MenuPessoaJuridica.IsVisible = false;
            }
            else
            {
                MenuPessoaFisica.IsVisible = false;
                MenuPessoaJuridica.IsVisible = true;
            }
            _pessoa = pessoa;
        }

        private async void GoToProfilePage(object sender, System.EventArgs e)
        {
            var Imagem = new Imagem();
            var Anexo = new Anexo();
            if (_pessoa.Imagem != null)
            {
                Imagem.Bytes = _pessoa.Imagem.Bytes;
            }
            if (_pessoa.Anexo != null)
            {
                Anexo.Bytes = _pessoa.Anexo.Bytes;
                Anexo.Nome = _pessoa.Anexo.Nome;
            }

            var pagina = new TestePage(_pessoa)
            {
                BindingContext = _pessoa,
            };

            await Detail.Navigation.PushAsync(new TestePage(_pessoa));
            IsPresented = false;
        }

        private async void GoToSettingsPage(object sender, System.EventArgs e)
        {
            await Detail.Navigation.PushAsync(new SettingsPage());
            IsPresented = false;
        }

        private void GoToRankingPopUp(object sender, System.EventArgs e)
        {
            Detail.Navigation.PushAsync(new RankingPage());
            IsPresented = false;
        }

        private void GoToMatchPopUp(object sender, System.EventArgs e)
        {
            Detail.Navigation.PushAsync(new PropostaLista(_pessoa));
            IsPresented = false;
        }

        private async void GoToFaqPage(object sender, System.EventArgs e)
        {
            await Detail.Navigation.PushAsync(new FaqPage());
            IsPresented = false;
        }

        private async void ViewCell_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Candidaturas(_pessoa));
        }
    }
}