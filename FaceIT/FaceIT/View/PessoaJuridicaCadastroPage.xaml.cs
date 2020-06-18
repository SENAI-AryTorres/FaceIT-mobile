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
    public partial class PessoaJuridicaCadastroPage : ContentPage
    {
        SkillViewModel skills = new SkillViewModel();
        Cadastro_Pessoa_Fisica service = new Cadastro_Pessoa_Fisica();
        public static Imagem Imagem { get; set; } = new Imagem();
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
                    var imgAux = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Small });

                    if (imgAux == null)
                        return;
                    imgCamera.Source = ImageSource.FromStream(() =>
                    {
                        var stream = imgAux.GetStream();
                        return stream;
                    });
                    using (var memoryStream = new MemoryStream())
                    {
                        imgAux.GetStream().CopyTo(memoryStream);
                        imgAux.Dispose();
                        Imagem.Bytes = memoryStream.ToArray();
                    }
                }
                else if (action == "Tirar Photo")
                {
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await DisplayAlert("Não encontrado a camera", "", "OK");
                        return;
                    }
                    var imgAux = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        SaveToAlbum = true,
                    });
                    if (imgAux == null)
                        return;
                    imgCamera.Source = ImageSource.FromStream(() =>
                    {
                        var stream = imgAux.GetStream();
                        return stream;
                    });
                    using (var memoryStream = new MemoryStream())
                    {
                        imgAux.GetStream().CopyTo(memoryStream);
                        //imgAux.Dispose();
                        //Imagem.Nome = imgAux.AlbumPath;
                        Imagem.Bytes = memoryStream.ToArray();
                    }

                    await DisplayAlert("Foto Localizada", "Local: " + imgAux.AlbumPath, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Permissão Negada", "Dê Permissão de camera para o Dispositivo.\nError:" + ex.Message, "OK");
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            Endereco endereco = new Endereco();
            Pessoa pessoa = new Pessoa();
            PessoaJuridica pj = new PessoaJuridica();
            string telefone = dddtel_entry.Text + telefone_entry.Text;
            string celular = dddcel_entry.Text + celular_entry.Text;

            endereco.CEP = cep_entry.Text;
            endereco.Pais = pais_entry.Text;
            endereco.UF = uf_entry.Text;
            endereco.Municipio = municipio_entry.Text;
            endereco.Logradouro = logradouro_entry.Text;
            endereco.Bairro = bairro_entry.Text;
            endereco.Numero = numero_entry.Text;
            endereco.Complemento = complemento_entry.Text;

            pessoa.Tipo = "PJ";
            pessoa.Email = email_entry.Text;
            pessoa.Senha = senha_entry.Text;
            pessoa.Celular = celular;
            pessoa.Telefone = telefone;
            pessoa.Endereco = endereco;
            pessoa.Imagem = Imagem;
            pessoa.Role = "user";
            //pessoa.Imagem.IDPessoaNavigation = pessoa;

            pj.CNPJ = cnpj_entry.Text;
            pj.RazaoSocial = rsocial_entry.Text;
            pj.NomeFantasia = Nfantasia_entry.Text;
            pj.IDPessoaNavigation = pessoa;
            pj.IE = Iestadual_entry.Text;

            var result = service.AddPessoaJuridica(pj);
            if (await result)
            {
                await DisplayAlert("Olá", "Cadastrado com Sucesso", "OK");
                await Navigation.PushAsync(new LoginPage());
            }
            else
                await DisplayAlert("Erro", "Cheque seus Dados", "OK");
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
                ListaSkills.ItemsSource = this.skills.SkillsLP.Where(
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
                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }
        }
    }
}