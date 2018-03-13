using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCalc;
using Xamarin.Forms;

namespace CalcHS
{
    public enum DisplayState { Inputting, Answer };

    public partial class MainPage : ContentPage
    {
        Label display;
        DisplayState displayState;
        String expressionText;
        String answer;
        private List<Button> buttons;
        private List<CalcButton> calcButtons;
        private Dictionary<System.Guid, CalcButton> buttonDict = new Dictionary<System.Guid, CalcButton> { };  // Hash lookup table for click events


        public MainPage()
        {
            //InitializeComponent();  // Gets UI from XAML? -- not using anymore

            //BindingContext = new Calculator();

            answer = "";
            expressionText = "";

            calcButtons = new List<CalcButton>
            {
                new CalcButton(ButtonType.Special, 0, 0, "Inv"),
                new CalcButton(ButtonType.Special, 0, 1, "Table"),
                new CalcButton(ButtonType.Special, 0, 2, "Stat"),
                new CalcButton(ButtonType.Special, 0, 3, "Clear", 2),
                new CalcButton(ButtonType.Function, 1, 0, "sin", "sin(", "Sin("),
                new CalcButton(ButtonType.Function, 1, 1, "cos", "cos(", "Cos("),
                new CalcButton(ButtonType.Function, 1, 2, "tan", "tan(", "Tan("),
                new CalcButton(ButtonType.Function, 1, 3, "log", "log(", "Log("),
                new CalcButton(ButtonType.Function, 1, 4, "ln", "ln(", "Ln("),
                new CalcButton(ButtonType.Special, 2, 0, "Deg/Rad"),
                new CalcButton(ButtonType.Function, 2, 1, "\u221a", "\u221a(", "Sqrt("),
                new CalcButton(ButtonType.Power, 2, 2, "^", "^(", "Pow("),   
                new CalcButton(ButtonType.Power, 2, 3, "x^2", "^(2)", "^(2)"),
                new CalcButton(ButtonType.Special, 2, 4, "Del"),
                new CalcButton(ButtonType.Number, 3, 0, "7", "7", "7"),
                new CalcButton(ButtonType.Number, 3, 1, "8", "8", "8"),
                new CalcButton(ButtonType.Number, 3, 2, "9", "9", "9"),
                new CalcButton(ButtonType.Grouping, 3, 3, "(", "(", "("),
                new CalcButton(ButtonType.Grouping, 3, 4, ")", ")", ")"),
                new CalcButton(ButtonType.Number, 4, 0, "4", "4", "4"),
                new CalcButton(ButtonType.Number, 4, 1, "5", "5", "5"),
                new CalcButton(ButtonType.Number, 4, 2, "6", "6", "6"),
                new CalcButton(ButtonType.Operator, 4, 3, "\u00d7", "\u00d7", "*"),
                new CalcButton(ButtonType.Operator, 4, 4, "\u00f7", "\u00f7", "/"),
                new CalcButton(ButtonType.Number, 5, 0, "1", "1", "1"),
                new CalcButton(ButtonType.Number, 5, 1, "2", "2", "2"),
                new CalcButton(ButtonType.Number, 5, 2, "3", "3", "3"),
                new CalcButton(ButtonType.Operator, 5, 3, "+", "+", "+"),
                new CalcButton(ButtonType.Operator, 5, 4, "\u2212", "\u2212", "-"),
                new CalcButton(ButtonType.Operator, 6, 0, "%", "%", "%"),
                new CalcButton(ButtonType.Number, 6, 1, "0", "0", "0"),
                new CalcButton(ButtonType.Number, 6, 2, ".", ".", "."),
                new CalcButton(ButtonType.Number, 6, 3, "(-)", "-", "-"),
                new CalcButton(ButtonType.Special, 6, 4, "Enter\n(=)")
            };


            display = new Label
            {
                Text = "",
                FontSize = 12
            };

            var grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(4, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            grid.Children.Add(display, 0, 0);   // Add display to grid
            Grid.SetColumnSpan(display, 5);

            // Add the buttons
            buttons = new List<Button> { };
          
            for (int i = 0; i < calcButtons.Count; i++) {
                buttons.Add(new Button
                {
                    Text = calcButtons[i].buttonText
                });
                
                if (calcButtons[i].displayable)
                {
                    buttons[i].Clicked += AppendToDisplay;
                }
                else
                {
                    switch(calcButtons[i].buttonText)
                    {
                        case ("Clear"):
                            buttons[i].Clicked += ClearDisplay;
                            break;
                        case ("Del"):
                            buttons[i].Clicked += BackSpace;
                            break;
                        case ("Inv"):
                            buttons[i].Clicked += ActivateInverse;
                            break;
                        case ("Deg/Rad"):
                            buttons[i].Clicked += ToggleDegRad;
                            break;
                        case ("Enter\n(=)"):
                            buttons[i].Clicked += ParseAndEvalDisplay;
                            break;
                    }
                }

                // Add to hash to lookup later for click events:
                buttonDict.Add(buttons[i].Id, calcButtons[i]);
                
                // Add to grid:
                grid.Children.Add(buttons[i], calcButtons[i].col, calcButtons[i].row+1);
                Grid.SetColumnSpan(buttons[i], calcButtons[i].colSpan);

            }

            Content = grid;
        
            
          
        }

        private void AppendToDisplay(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            /*if(displayState == DisplayState.Answer)
            {
                ClearDisplay(null, null);
            }*/

            if (buttonDict[button.Id].buttonType == ButtonType.Operator && expressionText == "" && answer != "")
            {
                display.Text += "ans";
                expressionText = answer;
            }
            display.Text += buttonDict[button.Id].displayText;
            expressionText += buttonDict[button.Id].expressionText;
            
        }

        public void ActivateInverse(object sender, EventArgs e)
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
            display.Text = "";
            expressionText = "";
        }


        private void ParseAndEvalDisplay(object sender, EventArgs e)
        {
            Expression exp;

            if (expressionText == "")    // If empty expression
            {
                return;
            }

            try
            {
                exp = new Expression(expressionText);
            }
            catch (ArgumentException error)
            {
                return;
            }

            try
            {
                answer = exp.Evaluate().ToString();
            }
            catch (EvaluationException error)
            {
                display.Text += "\nError in expression.\n";
                expressionText = "";
                return;
            }

            display.Text += "\n" + answer + "\n";
            expressionText = "";
            //displayState = DisplayState.Answer;

            

        }

        private void BackSpace(object sender, EventArgs e)
        {
            display.Text = display.Text.Substring(0, display.Text.Length - 1);
        }

        private void ToggleDegRad(object sender, EventArgs e)
        {

        }
    }

    
}