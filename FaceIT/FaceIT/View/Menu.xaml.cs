using FaceIT.Page;
using faceitapi.Models;
using faceitapi.Models.ViewModel;
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
    public partial class Menu : MasterDetailPage
    {
        
        public Menu(Pessoa pessoa, Token token)
        {            
            InitializeComponent();
            Detail = new NavigationPage(new MenuDetail());
            if (pessoa.PessoaJuridica == null)
            {
                fisicanome_entry.IsVisible = true;
            }
            else if (pessoa.PessoaFisica== null)
            {
                juridicanome_entry.IsVisible = true;
            }
        }
        private async void GoToProfilePage(object sender, System.EventArgs e)
        {
            var Endereco = new Endereco
            {
                CEP = _enderecocep.Text,
                Pais = _enderecopais.Text,
                UF = _enderecouf.Text,
                Municipio = _enderecomunicipio.Text,
                Logradouro = _enderecologradouro.Text,
                Numero = _endereconumero.Text,
                Complemento = _enderecocomplemento.Text,
                Bairro = _enderecobairro.Text
            };

            var pf = new PessoaFisica()
            {
                CPF = _pfCPF.Text,
                RG = _pfRG.Text,
                Nome = fisicanome_entry.Text
            };

            var pj = new PessoaJuridica()
            {
                RazaoSocial = _pjrs.Text,
                CNPJ = _pjcnpj.Text,
                IE = _pjIE.Text,
                NomeFantasia = juridicanome_entry.Text
            };
            var _Pessoa = new Pessoa()
            {
                Email = _email.Text,
                Senha = _senha.Text,
                PessoaJuridica = pj,
                PessoaFisica = pf,
                Excluido = false,
                Endereco = Endereco,
                Telefone = _tel.Text,
                Celular = _cel.Text,
                Tipo = _tipo.Text
            };

        var pagina = new TestePage(_Pessoa)
        {
            BindingContext = _Pessoa,
        };

        await Detail.Navigation.PushAsync(new TestePage(_Pessoa));
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
            //Detail.Navigation.PushAsync(new Match());
            IsPresented = false;
        }

        private async void GoToFaqPage(object sender, System.EventArgs e)
        {
            await Detail.Navigation.PushAsync(new FaqPage());
            IsPresented = false;
        }

    }
}