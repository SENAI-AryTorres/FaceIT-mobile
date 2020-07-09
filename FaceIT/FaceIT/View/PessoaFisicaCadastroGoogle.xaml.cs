using FaceIT.AuthHelpers;
using FaceIT.Page;
using FaceIT.Service;
using FaceIT.ViewModels;
using faceitapi.Models;
using Plugin.FilePicker;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FaceIT.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PessoaFisicaCadastroGoogle : ContentPage
    {

        private SkillViewModel skill = new SkillViewModel();
        private Cadastro_Pessoa_Fisica service = new Cadastro_Pessoa_Fisica();
        public static Imagem Imagem { get; set; } = new Imagem();

        public static Anexo Anexo { get; set; } = new Anexo();

        public User userGoogle;
        public MD5 md5 = new MD5CryptoServiceProvider();


        public PessoaFisicaCadastroGoogle(User userGoogle_)
        {

            InitializeComponent();

            //PopupNavigation.Instance.PopAsync();
            //this.btnFoto.Clicked += btnFoto_Clicked;

            DownloadImage(userGoogle_.Picture);
            UpdateSelectionData(Enumerable.Empty<Skill>(), Enumerable.Empty<Skill>());

            userGoogle = userGoogle_;
        }



        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var faq = e.Item as Skill;
            var vm = BindingContext as SkillViewModel;
        }

        private async void AddAnexo_Clicked(object sender, EventArgs e)
        {
            try
            {
                var file = await CrossFilePicker.Current.PickFile();

                if (file != null)
                {
                    Anexo.Bytes = file.DataArray;
                    Anexo.Nome = file.FileName;
                    lbl.Text = file.FileName;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DownloadImage(string url)
        {
            Image image = new Image();

            HttpWebRequest wr = (HttpWebRequest)HttpWebRequest.Create(url);

            wr.AllowWriteStreamBuffering = true;
            wr.Timeout = 30000;

            WebResponse webResponse = wr.GetResponse();

            using (var memoryStream = new MemoryStream())
            {
                webResponse.GetResponseStream().CopyTo(memoryStream);

                //imgAux.Dispose();
                //Imagem.Nome = imgAux.AlbumPath;
                Imagem.Bytes = memoryStream.ToArray();
            }

            webResponse.Close();


        }

        public async void Button_Clicked(object sender, EventArgs e)
        {

            Endereco endereco = new Endereco();
            Pessoa pessoa = new Pessoa();
            PessoaFisica pf = new PessoaFisica();


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

            pessoa.Tipo = "PF";
            pessoa.Email = userGoogle.Email;

            pessoa.Senha = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(userGoogle.Id.ToString())));

            pessoa.googleID = Convert.ToInt32(userGoogle.Id.ToString());

            pessoa.Celular = celular;
            pessoa.Telefone = telefone;
            pessoa.Endereco = endereco;

            pessoa.Imagem = Imagem;

            pessoa.Role = "user";
            pessoa.Anexo = Anexo;

            //pessoa.Imagem.IDPessoaNavigation = pessoa;


            pf.Nome = userGoogle.Name;

            pf.CPF = cpf_entry.Text;
            pf.RG = rg_entry.Text;
            pf.IDPessoaNavigation = pessoa;



            var result = service.AddPessoaFisica(pf);
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

        private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSelectionData(e.PreviousSelection, e.CurrentSelection);
        }

        private void UpdateSelectionData(IEnumerable<object> previousSelectedItems, IEnumerable<object> currentSelectedItems)
        {
            var anterior = ToList(previousSelectedItems);
            var atual = ToList(currentSelectedItems);
            previousSelectedItemLabel.Text = string.IsNullOrWhiteSpace(anterior) ? "[-]" : anterior;
            currentSelectedItemLabel.Text = string.IsNullOrWhiteSpace(atual) ? "[-]" : atual;
        }

        private static string ToList(IEnumerable<object> items)
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