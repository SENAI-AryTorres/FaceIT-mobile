using FaceIT.Service;
using faceitapi.Models;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace FaceIT.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuDetail : ContentPage
    {
        Map mapa;
        Pessoa _pessoa = new Pessoa();
        PropostaService service = new PropostaService();
        private Pin pin;
        private string _IdProposta;
        private string _descricao;
        private string[] strnomes;
        private string[] _idprop;
        private string[] _idemp;
        private string[] _desc;
        private string[] _tipocontrato;
        private string[] _cidade;
        private string[] _posicao;
        private string posicao;
        public MenuDetail(Pessoa pessoa)
        {
            service.GetPropostaAsync();
            InitializeComponent();
            CriarMapa();
            _ = PermissaoGPSAsync();
            _pessoa = pessoa;
        }

        public void CriarMapa()
        {
            mapa = new Map(MapSpan.FromCenterAndRadius(new Position(-26.0313007, -52.4692816), Distance.FromKilometers(100)));
            mapa.MapType = MapType.Hybrid;
            MapContainer.Children.Add(mapa);
        }

        //Metodo de permissao do GPS
        [Obsolete]
        public async Task PermissaoGPSAsync()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

                //Verifica se a permissao é diferente de aceita
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }

                //Permissao for aceita
                if (status == PermissionStatus.Granted)
                {
                    MapContainer.Children.RemoveAt(0);
                    GPS gps = new GPS();
                    if (gps.IsLocationAvailable())
                    {
                        Plugin.Geolocator.Abstractions.Position pos = gps.GetCurrentPosition().GetAwaiter().GetResult();

                        mapa = new Map(MapSpan.FromCenterAndRadius(new Position(pos.Latitude, pos.Longitude), Distance.FromMeters(100)));
                        MapContainer.Children.Add(mapa);

                        var propostas = await service.GetPropostaAsync();

                        foreach (var item in propostas)
                        {                            
                            mapa.Pins.Add(pin = new Pin
                            {
                                Position = new Position(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)),
                                Label = "ID da Vaga:" + item.IDProposta + "," +"\nID da Empresa:"+ item.IDEmpresa + "," + "\nDescricao da Vaga:" + item.Descricao + "," +
                                        "\nTipo de Contrato:" + item.TipoContrato + "," + "\nCidade:" + item.Cidade,
                                Address = item.Latitude + "," + item.Longitude,
                                Type = PinType.Place,
                            });
                            
                            pin.InfoWindowClicked += async (s, args) =>
                            {
                                try
                                {
                                    _descricao = ((Pin)s).Label;
                                    posicao = ((Pin)s).Address;
                                    _posicao = posicao.Split(',');
                                    strnomes = _descricao.Split(',');
                                    _idprop = strnomes[0].Split(':');
                                    _idemp = strnomes[1].Split(':');
                                    _desc = strnomes[2].Split(':');
                                    _tipocontrato = strnomes[3].Split(':');
                                    _cidade = strnomes[4].Split(':');
                                    await DisplayAlert("Informações da Vaga", $"{_descricao}", "OK");
                                    _IdProposta = _idprop[1];
                                }
                                catch (Exception)
                                {
                                    throw;
                                }                                
                            };
                            pin.Clicked += Pin_Clicked;
                        }    
                    }
                }
                //Não aceitaa
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception)
            {

            }
        }
        private bool isOpen = false;
        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (isOpen == false)
            {
                isOpen = true;
                //Scale to smaller
                await ((Frame)sender).ScaleTo(0.8, 50, Easing.Linear);
                //Wait a moment
                await Task.Delay(100);
                //Scale to normal
                await ((Frame)sender).ScaleTo(1, 50, Easing.Linear);

                //Show FloatMenuItem1
                FloatMenuItem1.IsVisible = true;
                await FloatMenuItem1.TranslateTo(0, 0, 100);
                await FloatMenuItem1.TranslateTo(0, -20, 100);
                await FloatMenuItem1.TranslateTo(0, 0, 200);
            }
            else
            {
                isOpen = false;
                //Scale to smaller
                await ((Frame)sender).ScaleTo(0.8, 50, Easing.Linear);
                //Wait a moment
                await Task.Delay(100);
                //Scale to normal
                await ((Frame)sender).ScaleTo(1, 50, Easing.Linear);

                //Hide FloatMenuItem1
                await FloatMenuItem1.TranslateTo(0, 0, 100);
                await FloatMenuItem1.TranslateTo(0, -20, 100);
                await FloatMenuItem1.TranslateTo(0, 0, 200);
                FloatMenuItem1.IsVisible = false;
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if(_pessoa.Tipo == "PF")
            {
                DisplayAlert("Erro", "Uma Pessoa Fisica não pode administrar propostas", "Ok");
            }
            else
            {
                Navigation.PushAsync(new AddProposta(_pessoa));
            }            
        }

        private async void Atualizar_Mapa_Clicked(object sender, EventArgs e)
        {
            PermissaoGPSAsync();
        }

        private async void Pin_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Deseja se Candidatar a Vaga?", "Ok", null, "Sim", "Não");
            if (action == "Sim")
            {
                CandidatoService service = new CandidatoService();
                Candidato candidato;
                Proposta proposta;
                proposta = new Proposta()
                {
                    IDProposta = Convert.ToInt32(_idprop[1]),
                    IDEmpresa = Convert.ToInt32(_idemp[1]),
                    Descricao = _desc[1],
                    TipoContrato = _tipocontrato[1],
                    Cidade = _cidade[1],
                    Encerrada = false,
                    Latitude = _posicao[0],
                    Longitude = _posicao[1],                     
                };

                candidato = new Candidato()
                {
                    IDPessoa = _pessoa.IDPessoa,
                    IDProposta = Convert.ToInt32(_idprop[1]),
                };
                if (_pessoa.Tipo == "PJ")
                {
                    await DisplayAlert("Erro", "Desculpe, Pessoas Juridicas não podem cadastrar a Vagas", "Ok");
                }
                else
                {
                    var result = service.AddCandidato(candidato);
                    if (await result)
                    {
                        await DisplayAlert("Parabéns", "Cadastrado com Sucesso", "Boa Sorte");
                    }
                    else
                    {
                        await DisplayAlert("Erro", "Houve erro no seu Cadastro", "Ok");
                    }
                }                
            }
            else
            {
                await DisplayAlert("Poxa, que Pena", "Você encontrará outras Vagas ;)", "Fechar");
            }
        }
    }
}