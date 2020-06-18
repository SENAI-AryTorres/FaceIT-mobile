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
    public partial class EditProfile : ContentPage
    {
        public EditProfile(Pessoa pessoa)
        {                       
            InitializeComponent();
            if (pessoa.Tipo != "PF")
            {
                pf_data.IsVisible = false;
            }
            else if (pessoa.Tipo != "PJ")
            {
                pj_data.IsVisible = false;
            }                
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Erro", "Não é possivel fazer a troca do Email", "OK");
        }

        private void BuscarCEP(object sender, TextChangedEventArgs args)
        {
            string cep = cep_entry.Text.Trim();
            if (cep.Length == 9)
            {
                try
                {
                    Endereco end = ViaCepService.BuscarEnderecoViaCep(cep);
                    if (end != null)
                    {
                        resultado.Text = string.Format("Endereço: {0}, {1}, {2}", end.UF, end.Logradouro, end.Bairro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: " + cep, "OK");
                    }
                    pais_entry.Text = "Brasil";
                    uf_entry.Text = end.UF;
                    logradouro_entry.Text = end.Logradouro;
                    bairro_entry.Text = end.Bairro;
                    munic_entry.Text = "";
                    numero_entry.Text = "";
                    comp_entry.Text = "";
                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }
        }
    }
}
