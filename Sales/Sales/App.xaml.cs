using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Sales
{
    using Sales.Helpers;
    using Sales.Views;
    using System;

    using ViewModels;
    public partial class App : Application
	{
        public static NavigationPage Navigator { get; internal set; }

        public App ()
		{
			InitializeComponent();
            MainViewModel.GetInstance().Login = new LoginViewModel();
			MainPage = new NavigationPage( new LoginPage());

            if (Settings.IsRemembered && !string.IsNullOrEmpty(Settings.AccessToken))
            {
                MainViewModel.GetInstance().Products = new ProductsViewModel();
                MainPage = new MasterPage();
            }
            else
            {
                MainViewModel.GetInstance().Login = new LoginViewModel();
                MainPage = new NavigationPage(new LoginPage());
            }
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
