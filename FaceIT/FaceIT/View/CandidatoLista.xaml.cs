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
    public partial class CandidatoLista : ContentPage
    {
        public int id;
        public CandidatoLista(int idproposta)
        {
            InitializeComponent();
            id = idproposta;
            GetCandidato();
        }

        private async void GetCandidato()
        {
            Atualizar_Clicked();
        }
        private async void Atualizar_Clicked()
        {
            CandidatoService service = new CandidatoService();
            var result = await service.GetCandidato(id);
            var lista = new List<Candidato>();
            lista = result;
            CV.ItemsSource = lista;
        }

        private void CV_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            UpdateSelectionData(e.AddedItems);
        }
        void UpdateSelectionData(IEnumerable<object> currentSelectedItems)
        {
            string atual = Convert.ToString((currentSelectedItems.FirstOrDefault() as Candidato)?.IDPessoa);
            currentSelectedItemLabel.Text = string.IsNullOrWhiteSpace(atual) ? "" : atual;
        }

        private async void CV_ItemHolding(object sender, Syncfusion.ListView.XForms.ItemHoldingEventArgs e)
        {
            await Navigation.PushAsync(new DetailCandidato(Convert.ToInt32(currentSelectedItemLabel.Text)));
        }
    }
}