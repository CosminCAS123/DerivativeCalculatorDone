using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AngouriMath;
using Acr.UserDialogs;
using DerivativeCalculator.Resources;
using DerivativeCalculator.CustomViews;
using Xamarin.Essentials;
namespace DerivativeCalculator
{
    //BUG
    //AFTER CLICKING SIMPLIFIED FORM
    //DERIVATE DISPLAYER WONT WORK
    //its got to do smth with the calculating label
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private string mathtext = null;
        public new event PropertyChangedEventHandler PropertyChanged;
        private const string instagram_uri = "instagram://user?username=_cosmin.andrei";
        private const string facebook_uri = "https://www.facebook.com/cosmin.paraschiv.73";
        public StackLayout MainLayout => (StackLayout)Content;
        public static MainPage instance;
        public MainPage()
        {
            
            InitializeComponent();
            instance = this;
            TextHolder.SetBinding(Entry.TextProperty, new Binding("math_text", source: this));
            UtilityClass.SetImage(InstaImage, "DerivativeCalculator.Resources.Images.instagram_logo.png");
            UtilityClass.SetImage(FacebookImage, "DerivativeCalculator.Resources.Images.facebook_logo.png");
            
        }

       
        private void Notations_Clicked(object sender, EventArgs e)
        {
            int a = 5;
            InstaImage.IsVisible = !InstaImage.IsVisible;
            FacebookImage.IsVisible = !FacebookImage.IsVisible;
            BugsButton.IsVisible = !BugsButton.IsVisible;
            Notations.IsVisible = !Notations.IsVisible;
        }

        /// <summary>
        /// Returns true if the Notations and DerivativeDisplayer views are closed
        /// </summary>
        private bool AllPopUpsClosed()
        {
            bool a = Notations.IsVisible;
            bool b =  UtilityClass.FindChildOfType<DisplayDerivative>((Layout<View>)TextHolder.Parent) == null;
            int c = 5;
            return !a && b;
        }
        private async void ExpressionEntry_Enter(object sender, EventArgs e)
        {
            string message = "Choose one of the below options.";
            await DisplayAlert("No command selected", message, "Ok");
            
        }
        public string math_text
        {
             set
            {
                if (this.mathtext != value)
                {
                    this.mathtext = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("math_text"));
                   
                }
            }
             get
            {
                return this.mathtext;
             
            }
        }

        private async void About_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new CustomPages.About());
        private  async void Insta_Tapped(object sender, EventArgs e)
        {
            if (!await Launcher.TryOpenAsync(instagram_uri))
            {
                await DisplayAlert("ERROR", "Some error has just occured.Check if you have Instagram installed.", "Ok");
            }
        }
        private async void Facebook_Tapped(object sender, EventArgs e)
        {
            try
            {
                await Browser.OpenAsync(facebook_uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception)
            {
                // An unexpected error occured. No browser may be installed on the device.
                await DisplayAlert("ERROR", "Some error has just occured.Check if you have any browsers installed.", "Ok");
            }
        }

        private async void BugsButton_Clicked(object sender, EventArgs e) =>  await Navigation.PushAsync(new CustomPages.BugReportPage());
        private void Simplify_Clicked(object sender, EventArgs e)
        {
            //good
            if (!NoBlockUILoading.isRunning)
            {
                if (AllPopUpsClosed())
                {
                    NoBlockUILoading.isRunning = true;
                    Task.Run(async () =>
                    {
                        Entry MathBox = TextHolder;
                        var loading_view = new NoBlockUILoading();
                        CalculatingLabel.SetBinding(Label.TextProperty, new Binding(path: "Text", source: loading_view.get_label));
                        string simplified_expression = null;
                        try
                        {
                            loading_view.RunLoading();//throwed :) exception
                            simplified_expression = ((Entity)(MathBox.Text)).Simplify().Evaled.ToString();
                            if (simplified_expression == "NaN") simplified_expression = "Undefined";
                            if (simplified_expression.Contains('.')) MathFunctions.RemoveExtraDecimals(ref simplified_expression);
                            loading_view.StopLoading();
                            //MainThread.BeginInvokeOnMainThread(() => MainLayout.Children.Remove(CalculatingLabel));
                           // MainThread.BeginInvokeOnMainThread(() => CalculatingLabel.IsVisible = false);
                            var result = await UserDialogs.Instance.ConfirmAsync(simplified_expression, "Paste into textbox?", "Yes", "No");
                            if (result) math_text = simplified_expression;
                        }
                        catch(Exception err)
                        {
                            TextHolder.Text = err.Message; 
                            loading_view.StopLoading();
                            ValidExpressionWarning.Instance.ShowWarning();
                        }
                       


                    });
                }
                else CloseNotationsOrDisplayerAlert();
                NoBlockUILoading.isRunning = false;
            }


        }
        private void CloseNotationsOrDisplayerAlert()  =>  DisplayAlert("Error", "Please close notations or derivative displayer before calculating anything else.", "Got it!");
        
        private void CreateDerivativeDisplayer(out DisplayDerivative displayer , string derivative , string executionTime)
        { 
            var layout = (StackLayout)TextHolder.Parent;// the stacklayout in mainpage.xaml
            displayer = new DisplayDerivative { Derivative = derivative, ExecutionTime = executionTime };
            layout.Children.Add(displayer);
            
        }//also puts it in the stacklayout


        private void DerivativeButton_Clicked(object sender, EventArgs e)
        {

            if (!NoBlockUILoading.isRunning)
            {
                if (AllPopUpsClosed())
                {
                    NoBlockUILoading.isRunning = true;
                    if (order_picker.SelectedItem != null)//see if order is selected
                    {
                        Task.Run(() =>
                        {
                            string expression = TextHolder.Text;
                            var loading_view = new NoBlockUILoading();
                            try
                            {
                                //take care of constant case
                                loading_view.RunLoading();
                                var timer = new System.Diagnostics.Stopwatch();//keep track of how much time it takes to execute
                                timer.Start();
                                Entity calculated_expression = MathFunctions.CalculateDerivative(expression, new Func<MathFunctions.DerivativeType>(() =>
                            {
                                switch (order_picker.SelectedIndex)
                                {
                                    case 0:
                                        return MathFunctions.DerivativeType.FirstDerivative;
                                    case 1:
                                        return MathFunctions.DerivativeType.SecondDerivative;
                                    case 2:
                                        return MathFunctions.DerivativeType.ThirdDerivative;
                                    default:
                                        return MathFunctions.DerivativeType.Default;

                                }

                            })());//may throw 
                                timer.Stop();
                                //string execution_time = string.Format("{0:0}.{1:00}", timer.Elapsed.Seconds, timer.ElapsedMilliseconds);
                                string execution_time = timer.Elapsed.ToString("s\\.ff");
                                loading_view.StopLoading();
                                DisplayDerivative displayer;
                                  MainThread.BeginInvokeOnMainThread(() => {  CreateDerivativeDisplayer(out displayer, calculated_expression.ToString(), execution_time); });//this shit 
                                //int a = 5;

                            }
                            catch (Exception ex)
                            {
                                TextHolder.Text = ex.Message;
                                ValidExpressionWarning.Instance.ShowWarning();
                            }



                        });
                    }
                    else
                    {
                        DisplayAlert("No Input Given", "Please select the order of your derivative.", "Ok");
                    }
                }
                else CloseNotationsOrDisplayerAlert();
                NoBlockUILoading.isRunning = false;
             }
        }

        private void CalculateIntegral_Clicked(object sender, EventArgs e)
        {
            
            if (!NoBlockUILoading.isRunning)
            {
                if (AllPopUpsClosed())
                {
                    try
                    {
                        NoBlockUILoading.isRunning = true;
                        Task.Run(() =>
                        {
                            string math = TextHolder.Text;
                            var loading_view = new NoBlockUILoading();
                            loading_view.RunLoading();
                            var timer = new System.Diagnostics.Stopwatch();//keep track of how much time it takes to execute
                            timer.Start();
                            Entity calculated_expression = MathFunctions.CalculateIntegral(math);
                            timer.Stop();
                        //string execution_time = string.Format("{0:0}.{1:00}", timer.Elapsed.Seconds, timer.ElapsedMilliseconds);
                            string execution_time = timer.Elapsed.ToString("s\\.ff");
                            loading_view.StopLoading();
                            DisplayDerivative displayer;
                            MainThread.BeginInvokeOnMainThread(() => { CreateDerivativeDisplayer(out displayer, calculated_expression.ToString(), execution_time); });
                        });
                    }
                    catch (Exception ex)
                    {
                        TextHolder.Text = ex.Message;
                        ValidExpressionWarning.Instance.ShowWarning();
                    }
                    finally
                    {
                        NoBlockUILoading.isRunning = false;
                    }

                }
                else CloseNotationsOrDisplayerAlert();
            }










        }
    }
}
