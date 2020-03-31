using FaceIT.Service;
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
        public MenuDetail()
        {
            InitializeComponent();
            CriarMapa();
            PermissaoGPSAsync();

        }

        public void CriarMapa()
        {
            mapa = new Map(MapSpan.FromCenterAndRadius(new Position(-15.8260571, -48.060058), Distance.FromKilometers(10000)));

            mapa.MapType = MapType.Street;
            MapContainer.Children.Add(mapa);
        }

        //Metodo de permissao do GPS
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

                        if (pos != null)
                        {
                            var MyPos = new Pin()
                            {
                                Position = new Position(pos.Latitude, pos.Longitude),
                                Label = "Minha Posição"
                            };

                            mapa = new Map(MapSpan.FromCenterAndRadius(new Position(pos.Latitude, pos.Longitude), Distance.FromMeters(100)));
                            mapa.Pins.Add(MyPos);
                            MapContainer.Children.Add(mapa);
                        }

                    }
                }

                //Não aceita
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}