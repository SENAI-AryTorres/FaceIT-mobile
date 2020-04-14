using FaceIT.Page;
using Plugin.Media;
using Rg.Plugins.Popup.Services;
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
    public partial class PessoaFisicaCadastroPage : ContentPage
    {

        public PessoaFisicaCadastroPage()
        {
            InitializeComponent();
            PopupNavigation.Instance.PopAsync();
            this.btnFoto.Clicked += btnFoto_Clicked;

        }

        private async void btnFoto_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Adicionar Foto", "Cancelar", null, "Escolher Imagem", "Tirar Photo");
            try
            {
                if (action == "Escolher Imagem")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await DisplayAlert("Photos Not Supported", "Permission not granted to photos", "OK");

                        return;
                    }
                    var file = Plugin.Media.CrossMedia.Current.PickPhotoAsync(new
                                      Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
                    });
                    if (file == null)
                        return;
                    imgCamera.Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.Result.GetStream();
                        file.Result.Dispose();
                        return stream;
                    });
                }
                else if (action == "Tirar Photo")
                {
                    await CrossMedia.Current.Initialize();

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await DisplayAlert("Não encontrado a camera", "", "OK");
                        return;
                    }

                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        SaveToAlbum = true,
                    });

                    if (file == null)
                        return;
                    this.imgCamera.Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        return stream;
                    });

                    await DisplayAlert("Foto Localizada", "Local: " + file.AlbumPath, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Permissão Negada", "Dê Permissão de camera para o Dispositivo.\nError:" + ex.Message, "OK");
            }
        }
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SkillPageCadastro());
        }
        
    }
}