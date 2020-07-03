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
            InitializeComponent();
            _pessoa = pessoa;            
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
            SL_Prop.IsVisible = true;
            string atual = Convert.ToString((currentSelectedItems.FirstOrDefault() as Proposta)?.IDProposta);
            currentSelectedItemLabel.Text = string.IsNullOrWhiteSpace(atual) ? "" : atual;

            string atualemp = Convert.ToString((currentSelectedItems.FirstOrDefault() as Proposta)?.IDEmpresa);
            _IdEmp.Text = string.IsNullOrWhiteSpace(atualemp) ? "" : atualemp;

            string Descricao = (currentSelectedItems.FirstOrDefault() as Proposta)?.Descricao;
            _desc.Text = string.IsNullOrWhiteSpace(Descricao) ? "" : Descricao;

            string Cidade = (currentSelectedItems.FirstOrDefault() as Proposta)?.Cidade;
            _Cidade.Text = string.IsNullOrWhiteSpace(Cidade) ? "" : Cidade;

            string Latitude = (currentSelectedItems.FirstOrDefault() as Proposta)?.Latitude;
            _Latitude.Text = string.IsNullOrWhiteSpace(Latitude) ? "" : Latitude;

            string Longitude = (currentSelectedItems.FirstOrDefault() as Proposta)?.Longitude;
            _Longitude.Text = string.IsNullOrWhiteSpace(Longitude) ? "" : Longitude;

        }
        private async void Atualizar_Clicked(object sender, EventArgs e)
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Proposta prop = new Proposta();
            prop.Descricao = _desc.Text;
            prop.Latitude = Convert.ToString(_Latitude.Text);
            prop.Longitude = Convert.ToString(_Longitude.Text);
            prop.Cidade = _Cidade.Text;
            prop.TipoContrato = Convert.ToString(_TipoContrato.SelectedItem);
            prop.Encerrada = false;
            prop.IDEmpresa = _pessoa.IDPessoa;
            prop.IDProposta = Convert.ToInt32(currentSelectedItemLabel.Text);
            var result = service.UpdateProposta(prop);
            if (await result)
            {
                await DisplayAlert("", "Proposta Atualizada com Sucesso", "OK");
            }
            else
            {
                await DisplayAlert("", "Erro ao Atualizar Proposta", "OK");
            }
        }
    }
}