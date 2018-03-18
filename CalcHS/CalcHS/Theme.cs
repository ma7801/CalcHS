using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CalcHS
{
    class Theme
    {
        public Color MainBGColor { get; set; }
        public Color DisplayBGColor { get; set; }
        public Color DisplayTextColor { get; set; }
        public Color ButtonBGColor { get; set; }
        public Color ButtonTextColor { get; set; }

        public Theme(Color MainBGColor, Color DisplayBGColor, Color DisplayTextColor, Color ButtonBGColor, Color ButtonTextColor)
        {
            this.MainBGColor = MainBGColor;
            this.DisplayBGColor = DisplayBGColor;
            this.DisplayTextColor = DisplayTextColor;
            this.ButtonBGColor = ButtonBGColor;
            this.ButtonTextColor = ButtonTextColor;
        }
    }
}
