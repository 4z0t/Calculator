using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    using Interfaces;
    public class View : IView
    {
       
        private int _left, _top;
        public View()
        {
            UpdateCursor();
        }

        public void UpdateCursor()
        {
            (_left, _top) = Console.GetCursorPosition();
        }
        public void DisplayChar(char c)
        {
            Console.Write(c);
            UpdateCursor();
        }

        public void RemoveChar()
        {
            Console.Write("\b \b");
            UpdateCursor();
        }

        public void DisplayError()
        {
            Console.SetCursorPosition(0, _top + 1);
            _ClearLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Incorrect Input");
            Console.ResetColor();
            Console.SetCursorPosition(_left, _top);
        }

        public void DisplayResult(string result)
        {
            Console.SetCursorPosition(0, _top + 1);
            _ClearLine();
            Console.WriteLine(result);
            Console.SetCursorPosition(_left, _top);
        }

        private void _ClearLine()
        {
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, _top + 1);
        }
    }
}
