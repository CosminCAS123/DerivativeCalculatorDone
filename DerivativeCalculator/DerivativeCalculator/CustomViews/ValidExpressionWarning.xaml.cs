using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.ComponentModel;
using Xamarin.Essentials;

namespace DerivativeCalculator.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public  partial class ValidExpressionWarning : ContentView
    {
        public static ValidExpressionWarning Instance;
        public  Action ShowWarning;//call this on the main thread all the time.
        Timer timer = new Timer(5000);
        public ValidExpressionWarning()
        {
            InitializeComponent();
            Instance = this;
            DerivativeCalculator.Resources.UtilityClass.SetImage(ExclamationMark, "DerivativeCalculator.Resources.Images.ExclamationMarkImg.png");
            timer.Elapsed += timer_elapsed;
            ShowWarning += () => {
                MainThread.BeginInvokeOnMainThread(() => {
                this.IsVisible = true; timer.Enabled = true; 
                }); 
            };

        }

        

      
        //problem may be that timer.enabled is executed on a different thread
        //and that thread doesn't know what "timer" is 
        private void timer_elapsed(object sender , ElapsedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                this.IsVisible = false;
                timer.Enabled = false;
            });
            
        }
    }
}
