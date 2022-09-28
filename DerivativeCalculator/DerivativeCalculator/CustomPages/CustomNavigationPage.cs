using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;
namespace DerivativeCalculator.CustomPages
{
    public class CustomNavigationPage:Xamarin.Forms.NavigationPage
    {
        public CustomNavigationPage(Page page)
        {
            On<Android>().SetBarHeight(100);
            BarBackgroundColor = Color.Red;
            PushAsync(page);
        }
    }
}
