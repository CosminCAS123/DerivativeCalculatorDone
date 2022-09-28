//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
namespace DerivativeCalculator.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoBlockUILoading : ContentView, INotifyPropertyChanged
    {
        
        public new event PropertyChangedEventHandler PropertyChanged;
        private byte dots = 0;
        private Timer timer = new Timer(300);
        public  NoBlockUILoading()
        {
            InitializeComponent();
            timer.Elapsed += (s, e) =>
              {
               if (dots < 3) { text_property += " ."; dots++; }
               else { text_property = "Calculating"; dots = 0; }
              };
            

        }
        public static bool isRunning { get;  set; }
        private string text_property
        {
            get { return label.Text; }
            set
            {
                label.Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(text_property));
            }
        }
        public Label get_label { get => label; } 
        public void RunLoading()
        {
            text_property = "Calculating";
            isRunning = true;
            timer.Start();
        }
        public void StopLoading()
        {
            timer.Stop();
            isRunning = false;

        }

     
    }
}