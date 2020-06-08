using FaceIT.Page;
using FaceIT.Service;
using FaceIT.ViewModels;
using faceitapi.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FaceIT.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PessoaFisicaCadastroPage : ContentPage
    {
        private SkillViewModel skill = new SkillViewModel();
        Cadastro_Pessoa_Fisica service = new Cadastro_Pessoa_Fisica();        
        public PessoaFisicaCadastroPage()
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
            await CrossMedia.Current.Initialize();
            var action = await DisplayActionSheet("Adicionar Foto", "Cancelar", null, "Escolher Imagem", "Tirar Photo");
            try
            {
                if (action == "Escolher Imagem")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await DisplayAlert("Sem suporte a essa foto", "Foto não Permitida", "OK");

                        return;
                    }
                    var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Small });

                    if (file == null)
                        return;

                    imgCamera.Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    });
                }
                else if (action == "Tirar Photo")
                {                    

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await DisplayAlert("Não encontrado a camera", "", "OK");
                        return;
                    }
                    var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        SaveToAlbum = true,
                    });
                    if (file == null)
                        return;
                    imgCamera.Source = ImageSource.FromStream(() =>
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
            string telefone = dddtel_entry.Text + telefone_entry.Text;
            string celular = dddcel_entry.Text + celular_entry.Text;

            //pegar os dados das entry para as model
            //Imagem imagem = new Imagem()
            //{
            //    Bytes = imgbyte,
            //    Nome = imgname,
            //};

            PessoaFisica pf = new PessoaFisica()
            {
                Nome = nome_entry.Text,
                CPF = cpf_entry.Text,
                RG = rg_entry.Text,
            };

            Endereco endereco = new Endereco()
            {
                CEP = cep_entry.Text,
                Pais = pais_entry.Text,
                UF = uf_entry.Text,
                Municipio = municipio_entry.Text,
                Logradouro = logradouro_entry.Text,
                Bairro = bairro_entry.Text,
                Numero = numero_entry.Text,
                Complemento = complemento_entry.Text,
            };

            Pessoa pessoa = new Pessoa()
            {
                Tipo = "pessoafisica",
                Email = email_entry.Text,
                Senha = senha_entry.Text,
                Excluido = false,
                Celular = celular,
                Telefone = telefone,
                Endereco = endereco,
                PessoaFisica = pf,
            };
            //var skillpage = new SkillPageCadastro();
            //skillpage.BindingContext = pessoa;
            //await Navigation.PushAsync(skillpage);


            //var result = service.AddPessoaFisica(pessoa);
            //if (await result)
            //{
                //await Navigation.PushAsync(new SkillPageCadastro());
            //}
            //else
            //    await DisplayAlert("Erro", "Cheque seus Dados", "OK");
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
        }

        private void SkillsSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SkillsSearchBar.Text != "")
            {
                ListaSkills.IsVisible = true;
                var texto = SkillsSearchBar.Text;
                ListaSkills.ItemsSource = this.skill.SkillsLP.Where(
                    s => s.Descricao.ToLower().Contains(texto.ToLower()));
            }
            else
                ListaSkills.IsVisible = false;
        }
        private void button_limpar_Clicked(object sender, EventArgs e)
        {
            currentSelectedItemLabel.Text = "";
            ListaSkills.SelectedItems = null;   
        }
    }
}