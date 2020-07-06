using FaceIT.Service;
using faceitapi.Models;
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
    public partial class EditarProposta : ContentPage
    {
        PropostaService service = new PropostaService();
        Proposta _proposta = new Proposta();
        public EditarProposta(Proposta proposta)
        {
            //IdProposta.Text = Convert.ToString(proposta.IDProposta);
            InitializeComponent();
            picker_tipo.SelectedItem = proposta.TipoContrato;
            _proposta = proposta;
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var endereco = (logradouro_entry.Text + numero_entry.Text + uf_entry.Text + bairro_entry.Text + pais_entry.Text);
            var locations = await Geocoding.GetLocationsAsync(endereco);
            var location = locations?.FirstOrDefault();
            if (location != null)
            {
                lblteste.Text = ($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
            }

            Proposta prop = new Proposta();
            prop.Descricao = Descricao.Text;
            prop.Latitude = Convert.ToString(location.Latitude);
            prop.Longitude = Convert.ToString(location.Longitude);
            prop.Cidade = uf_entry.Text;
            prop.TipoContrato = Convert.ToString(picker_tipo.SelectedItem);
            prop.Encerrada = false;
            prop.IDEmpresa = Convert.ToInt32(IdEmpresa.Text);
            prop.IDProposta = Convert.ToInt32(IdProposta.Text);
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

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            Proposta prop = new Proposta();
            prop.Descricao = _proposta.Descricao;
            prop.Latitude = _proposta.Latitude;
            prop.Longitude = _proposta.Longitude;
            prop.Cidade = _proposta.Cidade;
            prop.TipoContrato = _proposta.TipoContrato;
            prop.Encerrada = true;
            prop.IDEmpresa = _proposta.IDEmpresa;
            prop.IDProposta = _proposta.IDProposta;
            var result = service.UpdateProposta(prop);
            if (await result)
            {
                await DisplayAlert("", "Proposta Excluida com Sucesso", "OK");
            }
            else
            {
                await DisplayAlert("", "Erro ao Excluir Proposta", "OK");
            }
        }
    }
}