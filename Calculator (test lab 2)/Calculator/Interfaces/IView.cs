using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Interfaces
{
    interface IView
    {

        void DisplayResult(string result);

        void DisplayChar(char c);

        void DisplayError(string err);

        
    }
}
