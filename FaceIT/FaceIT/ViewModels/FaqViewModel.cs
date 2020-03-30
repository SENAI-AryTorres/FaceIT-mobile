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
                    Pergunta = "Pergunta 01",
                    IsVisible = false,
                    Resposta = "Resposta 01",
                },

                new FaqModel
                {
                    Pergunta = "Pergunta 02",
                    IsVisible = false,
                    Resposta = "Resposta 02",
                },

                new FaqModel
                {
                    Pergunta = "Pergunta 03",
                    IsVisible = false,
                    Resposta = "Resposta 03",
                },

                new FaqModel
                {
                    Pergunta = "Pergunta 04",
                    IsVisible = false,
                    Resposta = "Resposta 04",
                },

                new FaqModel
                {
                    Pergunta = "Pergunta 05",
                    IsVisible = false,
                    Resposta = "Resposta 05",
                },
            };
        }



        public void ShowOrHidePoducts(FaqModel faq)
        {
            if (_oldProduct == faq)
            {
                // click twice on the same item will hide it
                faq.IsVisible = !faq.IsVisible;
                UpdateProducts(faq);
            }
            else
            {
                if (_oldProduct != null)
                {
                    // hide previous selected item
                    _oldProduct.IsVisible = false;
                    UpdateProducts(_oldProduct);
                }
                // show selected item
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
