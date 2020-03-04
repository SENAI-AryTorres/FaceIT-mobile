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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void EsqueceuASenha_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Teste", "Isso é um Teste", "ok");
        }

        private void Cadastro_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CadastroPJ());
        }
    }
}