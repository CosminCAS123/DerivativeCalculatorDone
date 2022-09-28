using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DerivativeCalculator.Resources;
namespace DerivativeCalculator.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayDerivative : ContentView, INotifyPropertyChanged
    {
        private new event PropertyChangedEventHandler PropertyChanged;
        public DisplayDerivative()
        {
            InitializeComponent();
            UtilityClass.SetImage(CloseButton, UtilityClass.ImagesLocation + "CloseImg.png");
           
        }
        public string ExecutionTime
        {
            get
            {
                return ExecutionTimeLabel.Text;
            }

            set
            {
                ExecutionTimeLabel.Text = $"(Execution Time : {value}s)";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ExecutionTimeLabel.Text"));
            }
        }
        public string Derivative
        {
            get
            {
                return ResultLabel.Text;
            }

            set
            {
                ResultLabel.Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ResultLabel.Text"));
            }
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            StackLayout my_parent = (StackLayout)Parent;
            DisplayDerivative toClose = (DisplayDerivative)UtilityClass.FindChildOfType<DisplayDerivative>(my_parent);
            my_parent.Children.Remove(toClose);
             

        }
    }
}