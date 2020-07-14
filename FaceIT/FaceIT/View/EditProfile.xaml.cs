using FaceIT.Page;
using FaceIT.Service;
using FaceIT.ViewModels;
using faceitapi.Models;
using Plugin.FilePicker;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class EditProfile : ContentPage
    {
        SkillViewModel skill = new SkillViewModel();
        Pessoa _pessoa = new Pessoa();
        public static Anexo Anexo { get; set; } = new Anexo();
        private UpdatePessoa service = new UpdatePessoa();
        public static List<Skill> SkillsSelecionadas { get; set; } = new List<Skill>();
        public static Imagem Imagem { get; set; } = new Imagem();
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
            if (pessoa.Imagem != null)
            {
                img_entry.Source = ImageSource.FromStream(() => new MemoryStream(pessoa.Imagem.Bytes));
            }
            _pessoa = pessoa;
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
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
                    img_entry.Source = ImageSource.FromStream(() =>
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
                    img_entry.Source = ImageSource.FromStream(() =>
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Adicionar Foto", "Cancelar", null, "Manter Arquivo", "Editar o Arquivo");
            if (action == "Editar o Arquivo")
            {
                var file = await CrossFilePicker.Current.PickFile();

                if (file != null)
                {
                    Anexo.Bytes = file.DataArray;
                    Anexo.Nome = file.FileName;
                    _anexo_name_button.Text = file.FileName;
                }
            }
        }

        private async void Editar_Pessoa(object sender, EventArgs e)
        {
            var pessoa = new Pessoa();
            var pessoajuridica = new PessoaJuridica();
            var pessoafisica = new PessoaFisica();
            var endereco = new Endereco();
            string celular = celular_entry.Text;
            string telefone = telefone_entry.Text;

            var pessoaSkillAux = new List<PessoaSkill>();
            foreach (var item in SkillsSelecionadas)
            {
                pessoaSkillAux.Add(new PessoaSkill
                {
                    IDSkill = item.IDSkill,
                    IDTipoSkill = item.IDTipoSkill
                });
            }

            endereco.CEP = cep_entry.Text;
            endereco.Pais = pais_entry.Text;
            endereco.UF = uf_entry.Text;
            endereco.Municipio = munic_entry.Text;
            endereco.Logradouro = logradouro_entry.Text;
            endereco.Bairro = bairro_entry.Text;
            endereco.Numero = numero_entry.Text;
            endereco.Complemento = comp_entry.Text;

            pessoa.PessoaSkill = pessoaSkillAux;
            pessoa.Tipo = _tipo.Text;
            pessoa.Email = email_entry.Text;
            pessoa.Senha = nova_senha.Text;
            pessoa.Celular = celular;
            pessoa.Telefone = telefone;
            pessoa.Endereco = endereco;
            pessoa.Role = _role.Text;
            pessoa.IDPessoa = Convert.ToInt32(_idpessoa.Text);
            if(pessoa.Imagem == null && pessoa.Anexo == null)
            {
                pessoa.Imagem = _pessoa.Imagem;
                pessoa.Anexo = _pessoa.Anexo;
            }
            else
            {
                pessoa.Imagem = Imagem;
                pessoa.Anexo = Anexo;
            }
            pessoajuridica.RazaoSocial = pj_razaosocial.Text;
            pessoajuridica.NomeFantasia = pj_nomef.Text;
            pessoajuridica.CNPJ = pj_cnpj.Text;
            pessoajuridica.IDPessoaNavigation = pessoa;
            pessoajuridica.IDPessoa = pessoa.IDPessoa;
            pessoajuridica.IE = pj_Iestadual.Text;

            pessoafisica.CPF = pf_cpf.Text;
            pessoafisica.Nome = pf_nome.Text;
            pessoafisica.RG = pf_rg.Text;
            pessoafisica.IDPessoa = pessoa.IDPessoa;
            pessoafisica.IDPessoaNavigation = pessoa;

            if(_tipo.Text == "PJ")
            {
                var result = service.UpdatePessoaJuridicaAsync(pessoajuridica);
                if (await result)
                {
                    await DisplayAlert("OK", "Dados alterados com Sucesso", "OK");
                    await DisplayAlert("Alerta", "Para a Validação da sua nova Edição, refaça o login", "OK");
                    await Navigation.PushAsync(new LoginPage());
                }
                else
                    await DisplayAlert("Erro", "Erro ao alterar seus Dados", "OK");
            }
            else
            {
                var result = service.UpdatePessoaFisicaAsync(pessoafisica);
                if (await result)
                {
                    await DisplayAlert("OK", "Dados alterados com Sucesso", "OK");
                    await DisplayAlert("Alerta", "Para a Validação da sua nova Edição, refaça o login", "OK");
                    await Navigation.PushAsync(new LoginPage());
                }
                else
                    await DisplayAlert("Erro", "Erro ao alterar seus Dados", "OK"); 
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
            SkillsSelecionadas = currentSelectedItems.Cast<Skill>().ToList();
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
    }
}
