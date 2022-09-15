using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    using Interfaces;
    class Model : IModel
    {
        public bool AddOperator(Operation op)
        {
            throw new NotImplementedException();
        }

        public bool AddToNumber(char c)
        {
            throw new NotImplementedException();
        }

        public bool Calculate(out string result)
        {
            throw new NotImplementedException();
        }

        public bool CloseBracket()
        {
            throw new NotImplementedException();
        }

        public bool OpenBracket()
        {
            throw new NotImplementedException();
        }
    }
}
