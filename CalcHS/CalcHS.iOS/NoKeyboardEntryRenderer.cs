using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;

using Foundation;
using UIKit;
using CalcHS;
using CalcHS.iOS;

[assembly: ExportRenderer(typeof(NoKeyboardEntry), typeof(NoKeyboardEntryRenderer))]

namespace CalcHS.iOS
{
    class NoKeyboardEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            // Disabling the keyboard
            this.Control.InputView = new UIView();
        }
    }
}