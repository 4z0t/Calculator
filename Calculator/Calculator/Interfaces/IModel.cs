using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Interfaces
{
    public interface IModel
    {

        bool Calculate(out double result);

        bool CloseBracket();

        bool OpenBracket();

        bool AddToNumber(char c);

        bool AddOperator(Operation op);

        void Clear();


    }
}
