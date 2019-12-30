
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sales
{
    using ViewModels;
    using Views;
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            /*MainViewModel.GetInstance().Login = new LoginViewModel();
            MainPage = new LoginPage();*/
            this.MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
