using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DerivativeCalculator.CustomPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BugReportPage : ContentPage
    {
        public BugReportPage()
        {
            InitializeComponent();
        }

        private async void SendReportButton_Clicked(object sender, EventArgs e)
        {
            //report should be longer than 50 characters.
            if (ReportText.Text.Length <= 50)
            {
                //more than 50 words error
                UserDialogs.Instance.Alert("Your message must have more than 50 characters.", "Error", "Ok");
            }
            else
            {
                await DerivativeCalculator.Resources.EmailSender.SendEmail(ReportText.Text);
            }

        }
    }
}