using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FaceIT.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            SetValue(NavigationPage.HasNavigationBarProperty, false);
        }

        private async void NavegarLoginPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}