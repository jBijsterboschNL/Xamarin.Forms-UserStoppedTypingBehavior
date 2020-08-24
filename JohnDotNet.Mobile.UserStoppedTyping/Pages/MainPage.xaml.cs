using JohnDotNet.Mobile.UserStoppedTyping.ViewModels;
using Xamarin.Forms;

namespace JohnDotNet.Mobile.UserStoppedTyping.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new MainPageViewModel();
            
            InitializeComponent();
        }
    }
}