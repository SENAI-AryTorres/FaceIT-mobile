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
        public PropostaLista(Pessoa pessoa)
        {
            InitializeComponent();
            _pessoa = pessoa;
            GetPropostas();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var propostascridas = await service.GetPropostabyID(_pessoa.IDPessoa);
            var lista = new List<Proposta>();
            lista = propostascridas;
            CV.ItemsSource = lista;
            UpdateSelectionData(Enumerable.Empty<Proposta>());            
        }
        void UpdateSelectionData(IEnumerable<object> currentSelectedItems)
        {
            string atual = Convert.ToString((currentSelectedItems.FirstOrDefault() as Proposta)?.IDProposta);
            currentSelectedItemLabel.Text = string.IsNullOrWhiteSpace(atual) ? "" : atual;

            string atualemp = Convert.ToString((currentSelectedItems.FirstOrDefault() as Proposta)?.IDEmpresa);
            _IdEmp.Text = string.IsNullOrWhiteSpace(atualemp) ? "" : atualemp;

            string Descricao = (currentSelectedItems.FirstOrDefault() as Proposta)?.Descricao;
            _desc.Text = string.IsNullOrWhiteSpace(Descricao) ? "" : Descricao;

            string TipoProposta = (currentSelectedItems.FirstOrDefault() as Proposta)?.TipoContrato;
            _TipoContrato.Text = string.IsNullOrWhiteSpace(TipoProposta) ? "" : TipoProposta;

            string Cidade = (currentSelectedItems.FirstOrDefault() as Proposta)?.Cidade;
            _Cidade.Text = string.IsNullOrWhiteSpace(Cidade) ? "" : Cidade;

            string Latitude = (currentSelectedItems.FirstOrDefault() as Proposta)?.Latitude;
            _Latitude.Text = string.IsNullOrWhiteSpace(Latitude) ? "" : Latitude;

            string Longitude = (currentSelectedItems.FirstOrDefault() as Proposta)?.Longitude;
            _Longitude.Text = string.IsNullOrWhiteSpace(Longitude) ? "" : Longitude;

        }
        private async void GetPropostas()
        {
            Atualizar_Clicked();
        }
        private async void Atualizar_Clicked()
        {            
            var propostascridas = await service.GetPropostabyID(_pessoa.IDPessoa);
            var lista = new List<Proposta>();
            lista = propostascridas;
            CV.ItemsSource = lista;
            UpdateSelectionData(Enumerable.Empty<Proposta>());
            lbl_Att.Text = "Estas são as suas Propostas";
            lbl_edit.IsVisible = true;
            await DisplayAlert("Ok", "Dados Atualizados", "OK");    
        }

        private void CV_SelectionChanged_1(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            UpdateSelectionData(e.AddedItems);
        }
        private async void CV_ItemHolding(object sender, Syncfusion.ListView.XForms.ItemHoldingEventArgs e)
        {
            var action = await DisplayActionSheet($"Proposta {currentSelectedItemLabel.Text}  Selecionada", "Cancelar", null, "Editar Proposta", "Ver Candidatos");
            if(action == "Editar Proposta")
            {
                Proposta prop = new Proposta();
                prop.Descricao = _desc.Text;
                prop.Latitude = Convert.ToString(_Latitude.Text);
                prop.Longitude = Convert.ToString(_Longitude.Text);
                prop.Cidade = _Cidade.Text;
                prop.TipoContrato = Convert.ToString(_TipoContrato.Text);
                prop.Encerrada = false;
                prop.IDEmpresa = _pessoa.IDPessoa;
                prop.IDProposta = Convert.ToInt32(currentSelectedItemLabel.Text);

                var pagina = new EditarProposta(prop)
                {
                    BindingContext = prop,
                };
                await Navigation.PushAsync(pagina);
            }
            else if(action == "Ver Candidatos")
            {
                await Navigation.PushAsync(new CandidatoLista(Convert.ToInt32(currentSelectedItemLabel.Text)));
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Atualizar_Clicked();
        }
    }
}