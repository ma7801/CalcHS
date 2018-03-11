using System;
using System.Collections.Generic;
using System.Text;

namespace CalcHS
{
    public enum ButtonType { Number, Operator, Function, Power, Grouping, Special };

    public class CalcButton
    {
        public String buttonText;
        public String displayText;
        public String expressionText;
        public ButtonType buttonType;
        public int colSpan;
        public int row;
        public int col;
        public Boolean displayable;
 
        public CalcButton(ButtonType buttonType, int row, int col, String buttonText, String displayText, String expressionText, int colSpan)
        { 
            this.colSpan = colSpan;
            this.row = row;
            this.col = col;
            this.buttonText = buttonText;
            this.displayText = displayText;
            this.expressionText = expressionText;
            this.buttonType = buttonType;
            displayable = true;
        }

        public CalcButton(ButtonType buttonType, int row, int col, String buttonText)
        {
            this.colSpan = 1;
            this.row = row;
            this.col = col;
            this.buttonText = buttonText;
            this.buttonType = buttonType;
            displayable = false;
        }

        public CalcButton(ButtonType buttonType, int row, int col, String buttonText, int colSpan)
        {
            this.colSpan = colSpan;
            this.row = row;
            this.col = col;
            this.buttonText = buttonText;
            this.buttonType = buttonType;
            displayable = false;
        }


        public CalcButton(ButtonType buttonType, int row, int col, String buttonText, String displayText, String expressionText)
        {
            this.colSpan = 1;
            this.row = row;
            this.col = col;
            this.buttonText = buttonText;
            this.displayText = displayText;
            this.expressionText = expressionText;
            this.buttonType = buttonType;
            displayable = true;
        }

        

    }
}
