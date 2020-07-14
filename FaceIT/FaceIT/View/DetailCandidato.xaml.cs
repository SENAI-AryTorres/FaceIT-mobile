using FaceIT.Service;
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
    public partial class DetailCandidato : ContentPage
    {
        private int _id;
        public DetailCandidato(int idpessoa)
        {
            InitializeComponent();
            _id = idpessoa;
            GetPessoa();
        }
        private void GetPessoa()
        {
            Atualizar();
        }
        private async void Atualizar()
        {
            PessoaService service = new PessoaService();
            var result = await service.GetPessoaAsync(_id);
            if (result.Nome != null)
            {
                pessoaNome.Text = result.Nome;
            }
            if (result.CPF != null)
            {
                pessoacpf.Text = result.CPF;
            }
            if (result.RG != null)
            {
                pessoarg.Text = result.RG;
            }
            if (result.IDPessoaNavigation.Email != null)
            {
                pessoaemail.Text = result.IDPessoaNavigation.Email;
            }
            if (result.IDPessoaNavigation.Celular != null)
            {
                pessoacelular.Text = result.IDPessoaNavigation.Celular;
            }
            if (result.IDPessoaNavigation.Telefone != null)
            {
                pessoatelefone.Text = result.IDPessoaNavigation.Telefone;
            }            
            if (result.IDPessoaNavigation.Anexo != null)
            {
                pessoaanexo.Text = result.IDPessoaNavigation.Anexo.Nome;
            }
            else { }
            if (result.IDPessoaNavigation.Imagem != null)
            {
                pessoafoto.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(result.IDPessoaNavigation.Imagem.Bytes));
            }
            else { }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                Xamarin.Essentials.PhoneDialer.Open(pessoacelular.Text);
            }
            catch (Exception)
            {
                await DisplayAlert("", "Indisponivel para Ligação Telefônica", "OK");
            }
        }
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                Xamarin.Essentials.PhoneDialer.Open(pessoatelefone.Text);
            }
            catch (Exception)
            {
                await DisplayAlert("", "Indisponivel para Ligação Telefônica", "OK");
            }
        }
    }
}