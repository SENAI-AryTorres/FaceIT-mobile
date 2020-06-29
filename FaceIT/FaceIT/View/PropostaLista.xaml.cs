using FaceIT.Service;
using faceitapi.Models;
using Syncfusion.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FaceIT.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PropostaLista : ContentPage
    {
        PropostaService service = new PropostaService();
        Pessoa _pessoa = new Pessoa();
        List<Proposta> teste = new List<Proposta>();
        public PropostaLista(Pessoa pessoa)
        {
            service.GetPropostabyID(pessoa.IDPessoa);
            InitializeComponent();
            BindList();
            _pessoa = pessoa;
        }

        async void BindList()
        {
            teste = await service.GetPropostabyID(_pessoa.IDPessoa);
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var propostascridas = await service.GetPropostabyID(_pessoa.IDPessoa);


        }
    }
}