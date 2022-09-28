using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.Android;
using DerivativeCalculator.Droid.CustomRenderers;
using Xamarin.Forms;
using Android.Graphics.Drawables;
[assembly : ExportRenderer(typeof(DerivativeCalculator.CustomViews.OrderPicker) , typeof(OrderPickerRenderer))]
namespace DerivativeCalculator.Droid.CustomRenderers
{
     internal class OrderPickerRenderer : PickerRenderer
    {
        public OrderPickerRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var d = new GradientDrawable();
                d.SetColor(Android.Graphics.Color.Transparent);
                this.Control.SetBackground(d);
            }

            
        }
    }
}