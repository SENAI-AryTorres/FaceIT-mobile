using FaceIT.Models;
using FaceIT.ViewModels;
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
    public partial class FaqPage : ContentPage
    {
        public FaqPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var faq = e.Item as FaqModel;
            var vm = BindingContext as FaqViewModel;
            vm?.ShowOrHidePoducts(faq);
        }
    }
}