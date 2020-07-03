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
    public partial class EditarProposta : ContentPage
    {
        PropostaService service = new PropostaService();
        int _id;
        public EditarProposta(int id)
        {
            InitializeComponent();
            EditProposta(id);
        }
        private async void EditProposta(int id)
        {
            var propostascridas = await service.GetPropostabyIDProp(id);
            var lista = new List<Proposta>();
            lista = propostascridas;
            CV.ItemsSource = lista;
        }
    }
}