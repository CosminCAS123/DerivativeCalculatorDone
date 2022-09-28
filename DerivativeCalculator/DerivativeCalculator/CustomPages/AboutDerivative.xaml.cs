using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Windows.Input;
using Xamarin.Forms.Xaml;

namespace DerivativeCalculator.CustomPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutDerivative : ContentPage
    {
        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
        public ICommand MainPageCode => new Command<string>(async (url) => await Launcher.OpenAsync(url));
        public AboutDerivative()
        {
            BindingContext = this;
            InitializeComponent();
        }
    }
}