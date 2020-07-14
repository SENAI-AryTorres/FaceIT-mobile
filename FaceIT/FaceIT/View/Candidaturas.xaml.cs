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
    public partial class Candidaturas : ContentPage
    {
        CandidatoService Service = new CandidatoService();
        Pessoa _pessoa = new Pessoa();
        public Candidaturas(Pessoa pessoa)
        {
            InitializeComponent();
            _pessoa = pessoa;
            GetPropostas();
        }

        private void GetPropostas()
        {
            Atualizar_Clicked();
        }
        private async void Atualizar_Clicked()
        {
            var propostascridas = await Service.GetCandidatobyProposta(_pessoa.IDPessoa);
            var lista = new List<Proposta>();
            lista = propostascridas;
            CV.ItemsSource = lista;

            lbl_Att.Text = "Estas são as suas Propostas";
            await DisplayAlert("Ok", "Dados Atualizados", "OK");
        }
    }
}