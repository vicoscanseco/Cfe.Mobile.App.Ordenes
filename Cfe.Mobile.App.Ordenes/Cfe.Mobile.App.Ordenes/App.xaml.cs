using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cfe.Mobile.App.Ordenes {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            //MainPage = new MainPage();
            
            Cfe.Mobile.App.Ordenes.Core.Core.OS.DependencyService.Register<OS.NavigationService, Core.Core.OS.INavigationService>();
            

            MainPage = new NavigationPage(new MainPage());//Se cambia para que navege///////////

            OS.NavigationService.Navigation = MainPage.Navigation;

        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
