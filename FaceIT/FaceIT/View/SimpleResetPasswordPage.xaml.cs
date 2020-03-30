using FaceIT.Page;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace FaceIT.Views.Forms
{
    /// <summary>
    /// Page to reset old password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleResetPasswordPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleResetPasswordPage" /> class.
        /// </summary>
        public SimpleResetPasswordPage()
        {
            InitializeComponent();
        }

        private async void ToRegisterPage(object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new RegisterPage());
        }
    }
}