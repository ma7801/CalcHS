using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using NCalc;
using Xamarin.Forms;

namespace CalcHS
{
    public partial class MainPage : ContentPage
    {
        public String display;
        public Dictionary<String, CalcButton> exprButtons;  // Indexed by text on button
        

        public MainPage()
        {
            InitializeComponent();
            display = "";

            // Initialize the "expression" buttons - i.e. any buttons that add on to an expression
            CalcButton sinButton = new CalcButton("sin(", "SIN(");
            CalcButton cosButton = new CalcButton("cos(", "COS(");
            CalcButton tanButton = new CalcButton("tan(", "TAN(");
            CalcButton logButton = new CalcButton("log(", "LOG(");
            CalcButton lnButton = new CalcButton("ln(", "LN(");
            CalcButton sqrtButton = new CalcButton("\u221a(", "SQRT(");
            //CalcButton expButton = new CalcButton("^", )






        }

        private void AppendToDisplay(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            String textToAppend;

            // Exceptions for buttons whose text doesn't quite match what should be in the display:
            switch (button.Text)
            {
                case ("\u221a"):  // Square Root symbol
                    textToAppend = "\u221a(";
                    break;
                case ("x^2"):
                    textToAppend = "^(2)";
                    break;
                case ("^"):
                    textToAppend = "^(";
                    break;
                case ("(-)"):
                    textToAppend = "-";
                    break;
                case ("sin"):
                    textToAppend = "sin(";
                    break;
                case ("cos"):
                    textToAppend = "cos(";
                    break;
                case ("tan"):
                    textToAppend = "tan(";
                    break;
                case ("log"):
                    textToAppend = "log(";
                    break;
                case ("ln"):
                    textToAppend = "ln(";
                    break;
                default:
                    textToAppend = button.Text;
                    break;
            }

            display += textToAppend;
            textDisplay.Text = display;
        }

        private void ActivateInverse(object sender, EventArgs e)
        {

        }

        private void StartTableMode(object sender, EventArgs e)
        {

        }

        private void StartStatMode(object sender, EventArgs e)
        {

        }

        private void ClearDisplay(object sender, EventArgs e)
        {
            display = "";
            textDisplay.Text = display;

        }


        private void ParseAndEvalDisplay(object sender, EventArgs e)
        {
            //Expression exp = new Expression(display);
            //String temp = exp.Evaluate().ToString();
            //textDisplay.Text = temp;
            //display = "";

        }

        private void BackSpace(object sender, EventArgs e)
        {
            display = display.Substring(0, display.Length - 1);
            textDisplay.Text = display;
        }

        private void ToggleDegRad(object sender, EventArgs e)
        {

        }
    }

    public class CalcButton
    {
        public String displayText;
        public String expressionText;

        /*
        public CalcButton(int orderNum, String displayText, String expressionText, int columnSpan)
        {
            this.orderNum = orderNum;
            this.columnSpan = columnSpan;
            this.displayText = displayText;
            this.expressionText = expressionText;
        }
        
        public CalcButton(int orderNum)
        {
            this.orderNum = orderNum;
            this.columnSpan = 1;
            this.displayText = "";
            this.expressionText = "";
        }
        */
        public CalcButton(String displayText, String expressionText)
        {
            this.displayText = displayText;
            this.expressionText = expressionText;
        }

    }
}