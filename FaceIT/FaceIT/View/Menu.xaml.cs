using FaceIT.Page;
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
    public partial class Menu : MasterDetailPage
    {
        public Menu()
        {
            InitializeComponent();
            Detail = new NavigationPage(new MenuDetail());
        }
        private async void GoToProfilePage(object sender, System.EventArgs e)
        {
            await Detail.Navigation.PushAsync(new TestePage());
            IsPresented = false;
        }

        private async void GoToSettingsPage(object sender, System.EventArgs e)
        {
            await Detail.Navigation.PushAsync(new SettingsPage());
            IsPresented = false;
        }

        private void GoToRankingPopUp(object sender, System.EventArgs e)
        {
            Detail.Navigation.PushAsync(new RankingPage());
            IsPresented = false;
        }

        private void GoToMatchPopUp(object sender, System.EventArgs e)
        {
            //Detail.Navigation.PushAsync(new Match());
            IsPresented = false;
        }

        private async void GoToFaqPage(object sender, System.EventArgs e)
        {
            await Detail.Navigation.PushAsync(new FaqPage());
            IsPresented = false;
        }

    }
}