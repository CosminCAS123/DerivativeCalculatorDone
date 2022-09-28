using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Reflection;

namespace DerivativeCalculator.Resources
{
    public static class UtilityClass
    {

        public enum ViewTypes
        {
            Label,
            Image,
            Button,
            ImageButton,
            Slider,
            Stepper,
            Picker,
            Entry,
            Editor,
            ListView,
            Null
        }
       
        public static string ImagesLocation => "DerivativeCalculator.Resources.Images.";
        /// <summary>
        /// Sets the image property to an image you have stored at the specified location in your project
        /// </summary>
        public static void SetImage(View view, string location)
        {

            ViewTypes type = new Func<ViewTypes>(() => {
                if (view is Image) return ViewTypes.Image;
                else if (view is Button) return ViewTypes.Button;
                else if (view is ImageButton) return ViewTypes.ImageButton;
                //etc
                return ViewTypes.Null;
            })();

            switch (type)
            {
                case ViewTypes.Image:
                    ((Image)view).Source = ImageSource.FromResource(location, typeof(UtilityClass).GetTypeInfo().Assembly);
                    return;
                case ViewTypes.Button:
                    ((Button)view).ImageSource = ImageSource.FromResource(location, typeof(UtilityClass).GetTypeInfo().Assembly);
                    return;
                case ViewTypes.ImageButton:
                    ((ImageButton)view).Source = ImageSource.FromResource(location, typeof(UtilityClass).GetTypeInfo().Assembly);
                    return;      
            }

            
            
            
        }
        /// <summary>
        /// Returns the first child of type T 
        /// </summary>
        public static object FindChildOfType<T>(Layout<View> parent)
        {
            
            foreach (View x in parent.Children)
                if (x is T) return x;
            return null;
        }
        public static List<object> FindChildrenOfType<T>(Layout<View> parent)
        {
            List<object> toReturn = null;
            foreach (View x in parent.Children)
                if (x is T) toReturn.Add(x);
            return toReturn;
        }
    }
}
