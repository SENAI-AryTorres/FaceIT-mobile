using FaceIT.Service;
using FaceIT.ViewModels;
using faceitapi.Models;
using GoogleMaps.LocationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FaceIT.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProposta : ContentPage
    {
        PropostaService service = new PropostaService();

        Pessoa _pessoa = new Pessoa();
        public AddProposta(Pessoa pessoa)
        {
            InitializeComponent();
            _pessoa = pessoa;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var endereco = (logradouro_entry.Text + numero_entry.Text + uf_entry.Text + bairro_entry.Text + pais_entry.Text  );

            var locations = await Geocoding.GetLocationsAsync(endereco);

            var location = locations?.FirstOrDefault();
            if (location != null)
            {                
                lblteste.Text = ($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
            }

            Proposta prop = new Proposta();
            prop.Descricao = descricao.Text;
            prop.Latitude = Convert.ToString(location.Latitude);
            prop.Longitude = Convert.ToString(location.Longitude);
            prop.Cidade = uf_entry.Text;
            prop.TipoContrato = Convert.ToString(tipo_contrato.SelectedItem);
            prop.Encerrada = false;
            prop.IDEmpresa = _pessoa.IDPessoa;
            //prop.IDEmpresaNavigation = _pessoa;
            
            var result = service.AddProposta(prop);
            if (await result)
            {
                await DisplayAlert("", "Proposta adicionada com Sucesso", "OK");
            }
            else
            {
                await DisplayAlert("", "Erro ao Adicionar Proposta", "OK");
            }
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