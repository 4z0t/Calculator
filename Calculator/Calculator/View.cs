using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    using Interfaces;
    class View : IView
    {
        public void DisplayChar(char c)
        {
            Console.Write(c);
        }

        public void DisplayError(string err)
        {
            Console.Write('\n');
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(err);
            Console.ResetColor();
        }

        public void DisplayResult(string result)
        {
            Console.Write('\n');
            Console.WriteLine(result);
        }
    }
}
