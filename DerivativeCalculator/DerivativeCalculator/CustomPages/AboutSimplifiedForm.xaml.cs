using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using Xamarin.Essentials;
namespace DerivativeCalculator.CustomPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutSimplifiedForm : ContentPage
    {
        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
       
        public AboutSimplifiedForm()
        {
            InitializeComponent();
            BindingContext = this;
            LogoImage.Source = ImageSource.FromResource("DerivativeCalculator.Resources.Images.AngouriMathz.png", typeof(AboutSimplifiedForm).GetTypeInfo().Assembly);
            
        }

        private void AngouriLogo_Tapped(object sender, EventArgs e)
        {
             Browser.OpenAsync("https://am.angouri.org/", BrowserLaunchMode.SystemPreferred);

        }
    }
}