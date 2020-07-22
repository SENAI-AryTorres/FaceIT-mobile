using FaceIT.Models;
using System;
using System.Collections.ObjectModel;

namespace FaceIT.ViewModels
{
    public class FaqViewModel
    {
        private FaqModel _oldProduct;

        public ObservableCollection<FaqModel> Faqs { get; set; }

        public FaqViewModel()
        {
            Faqs = new ObservableCollection<FaqModel>
            {
                new FaqModel
                {
                    Pergunta = "Como Funciona? ",
                    IsVisible = false,
                    Resposta = "O FaceIT é uma plataforma que uma empresa ou pessoa física pode utilizar para benefício próprio. É um lugar onde você pode ser empregado ou contratar alguém para sua empresa.",
                },

                new FaqModel
                {
                    Pergunta = "Qual a intenção do App?",
                    IsVisible = false,
                    Resposta = "O principal objetivo é facilitar o contato entre as empresas e os interessados, para que assim seja mais fácil o fechamento de projetos ou oportunidades.",
                },

                new FaqModel
                {
                    Pergunta = "O App tem mais alguma funcionalidade?",
                    IsVisible = false,
                    Resposta = "Por ser a primeira versão, o app foca nessa possibilidade do contato entre empresas e pessoas, mas em versões futuras, pretendemos possibilitar o contato entre pessoas, permitir o vínculo com outros sistemas (github e linkedin), além de permitir a elaboração de grupos de estudo e uma comunidade para sanar dúvidas e/ou disponibilizar cursos.",
                },
            };
        }



        public void ShowOrHidePoducts(FaqModel faq)
        {
            if (_oldProduct == faq)
            {
                faq.IsVisible = !faq.IsVisible;
                UpdateProducts(faq);
            }
            else
            {
                if (_oldProduct != null)
                {
                    _oldProduct.IsVisible = false;
                    UpdateProducts(_oldProduct);
                }
                faq.IsVisible = true;
                UpdateProducts(faq);
            }

            _oldProduct = faq;
        }

        private void UpdateProducts(FaqModel product)
        {
            var index = Faqs.IndexOf(product);
            Faqs.Remove(product);
            Faqs.Insert(index, product);
        }
    }
}
