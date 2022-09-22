using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Interfaces
{
    public interface IPresenter
    {

        void OnCharInput(char c);

        bool OnOperatorInput(Operation op);

        void OnBackspace();

        void OnEnterInput();

        bool OnNumberInput(char c);

        bool OnBracketOpen();

        bool OnBracketClose();

        (double, bool) ProcessString(string s);

    }
}
