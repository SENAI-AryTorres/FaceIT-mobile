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
            pessoaNome.Text = result.Nome;
            pessoacpf.Text = result.CPF;
            pessoarg.Text = result.RG;
            pessoaemail.Text = result.IDPessoaNavigation.Email;
            pessoacelular.Text = result.IDPessoaNavigation.Celular;
            pessoatelefone.Text = result.IDPessoaNavigation.Telefone;
            pessoaanexo.Text = result.IDPessoaNavigation.Anexo.Nome;
            pessoafoto.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(result.IDPessoaNavigation.Imagem.Bytes)); 
        }
    }
}