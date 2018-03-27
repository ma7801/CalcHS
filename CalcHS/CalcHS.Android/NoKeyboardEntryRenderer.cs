using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CalcHS.Droid;
using CalcHS;
using Android.Views.InputMethods;
using System.Reflection;

[assembly: ExportRenderer(typeof(NoKeyboardEntry), typeof(NoKeyboardEntryRenderer))]
namespace CalcHS.Droid
{

    public class NoKeyboardEntryRenderer : EntryRenderer
    {
        public NoKeyboardEntryRenderer(Context context) : base(context)
        {
            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            this.Control.ShowSoftInputOnFocus = false;
            /*
            if (e.OldElement == null)
            {
                var field = this.GetType().GetField("HandleKeyboardOnFocus", BindingFlags.Instance | BindingFlags.NonPublic);
                field.SetValue(this, false);
            }
            */

            /*
            if (e.NewElement != null)
            {
                ((NoKeyboardEntry)e.NewElement).PropertyChanging += OnPropertyChanging;
            }

            if (e.OldElement != null)
            {
                ((NoKeyboardEntry)e.OldElement).PropertyChanging -= OnPropertyChanging;
            }



            // Disable the Keyboard on Focus
            
            this.Control.ShowSoftInputOnFocus = false;
    
            */
        }

        private void OnPropertyChanging(object sender, PropertyChangingEventArgs propertyChangingEventArgs)
        {
            // Check if the view is about to get Focus
            if (propertyChangingEventArgs.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
            {
                // incase if the focus was moved from another Entry
                // Forcefully dismiss the Keyboard 
                InputMethodManager imm = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(this.Control.WindowToken, 0);
            }
        }

        /*
         * Added this from: https://forums.xamarin.com/discussion/comment/167452/#Comment_167452
         * Not sure that I need it
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Entry.TextProperty.PropertyName)
            {
                base.Control.Text = base.Element.Text;
                if (base.Control.IsFocused)
                {
                    base.Control.SetSelection(base.Control.Text.Length);
                }
                return;
            }
            base.OnElementPropertyChanged(sender, e);
        }
        */

    }

}