using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Interfaces
{
    public interface IModel
    {

        bool Calculate(out string result);

        bool CloseBracket();

        bool OpenBracket();

        bool AddToNumber(char c);

        bool AddOperator(Operation op);



    }
}
