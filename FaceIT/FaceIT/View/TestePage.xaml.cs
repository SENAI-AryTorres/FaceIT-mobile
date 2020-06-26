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
            if (pessoa.PessoaFisica.Nome == null)
            {
                juridiconome_entry.IsVisible = true;
                fisicanome_entry.IsVisible = false;
            }
            else if (pessoa.PessoaJuridica.NomeFantasia == null)
            {
                fisicanome_entry.IsVisible = true;
                juridiconome_entry.IsVisible = false;
            }
            if (pessoa.Imagem != null)
            {
                img_entry.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(pessoa.Imagem.Bytes));
            }
            if (pessoa.Anexo != null)
            {
                _anexo_name.Text = pessoa.Anexo.Nome;
            }
            _pessoa.Imagem = pessoa.Imagem;
            _pessoa.Anexo = pessoa.Anexo;
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
            var Imagem = new Imagem()
            {
                Bytes = _pessoa.Imagem.Bytes
            };
            var Anexo = new Anexo()
            {
                Bytes = _pessoa.Anexo.Bytes,
                Nome = _pessoa.Anexo.Nome
            };

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
                NomeFantasia = juridiconome_entry.Text
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
                Tipo = _tipo.Text,
                Imagem = Imagem,
                Anexo = Anexo
            };

            var pagina = new EditProfile(_Pessoa)
            {
                BindingContext = _Pessoa
            };
            await Navigation.PushAsync(new EditProfile(_Pessoa));
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