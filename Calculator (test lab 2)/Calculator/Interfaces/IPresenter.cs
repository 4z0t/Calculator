﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Interfaces
{
    interface IPresenter
    {

        void OnCharInput(char c);

        bool OnOperatorInput(Operation op);

        void OnEnterInput();

        bool OnNumberInput(char c);

        bool OnBracketOpen();

        bool OnBracketClose();

    }
}