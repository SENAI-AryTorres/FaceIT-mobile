using FaceIT.ViewModels;
using faceitapi.Models;
using Plugin.Media;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FaceIT.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PessoaJuridicaCadastroPage : ContentPage
    {
        SkillViewModel skills = new SkillViewModel();
        public PessoaJuridicaCadastroPage()
        {
            InitializeComponent();
            PopupNavigation.Instance.PopAsync();
            this.btnFoto.Clicked += btnFoto_Clicked;
            UpdateSelectionData(Enumerable.Empty<Skill>(), Enumerable.Empty<Skill>());
        }
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var faq = e.Item as Skill;
            var vm = BindingContext as SkillViewModel;
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
            //await Navigation.PushAsync(new SkillPageCadastro());
        }

        public static byte[] ReadFully(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSelectionData(e.PreviousSelection, e.CurrentSelection);
        }
        void UpdateSelectionData(IEnumerable<object> previousSelectedItems, IEnumerable<object> currentSelectedItems)
        {
            var anterior = ToList(previousSelectedItems);
            var atual = ToList(currentSelectedItems);
            previousSelectedItemLabel.Text = string.IsNullOrWhiteSpace(anterior) ? "[-]" : anterior;
            currentSelectedItemLabel.Text = string.IsNullOrWhiteSpace(atual) ? "[-]" : atual;
        }
        static string ToList(IEnumerable<object> items)
        {
            if (items == null)
            {
                return string.Empty;
            }

            return items.Aggregate(string.Empty,
                (sender, obj) => sender + (sender.Length == 0 ? "" : ", ")
                + ((Skill)obj).Descricao);
        }        private void SkillsSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SkillsSearchBar.Text != "")
            {
                ListaSkills.IsVisible = true;
                var texto = SkillsSearchBar.Text;
                ListaSkills.ItemsSource = this.skills.SkillsLP.Where(
                    s => s.Descricao.ToLower().Contains(texto.ToLower()));
            }
            else
                ListaSkills.IsVisible = false;
        }        private void button_limpar_Clicked(object sender, EventArgs e)
        {
            currentSelectedItemLabel.Text = "";
        }
    }
}