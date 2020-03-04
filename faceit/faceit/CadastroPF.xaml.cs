using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace faceit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroPF : ContentPage
    {
        public CadastroPF()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Aviso", "Deseja Excluir?", "Sim","Não");
        }
    }
}