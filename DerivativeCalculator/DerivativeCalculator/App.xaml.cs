using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DerivativeCalculator.CustomPages;
[assembly: ExportFont("LeagueSpartan-Bold.otf" , Alias = "BossFont")]
[assembly: ExportFont("Quicksand-Light.otf", Alias = "CoolFont")]
namespace DerivativeCalculator
{
     
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CustomNavigationPage(new MainPage());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
