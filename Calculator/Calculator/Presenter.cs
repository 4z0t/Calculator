﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calculator
{
    using Interfaces;
    public class Presenter : IPresenter
    {
        private readonly IView _view;
        private readonly IModel _model;
        private string _input = "";

        public Presenter(IView view, IModel model)
        {
            _view = view;
            _model = model;
        }




        public bool OnBracketClose()
        {
            return _model.CloseBracket();
        }

        public bool OnBracketOpen()
        {
            return _model.OpenBracket();
        }

        void _Refresh()
        {
            foreach (char c in _input)
            {

                bool isFailed = false;
                switch (c)
                {

                    case '(':
                        isFailed = OnBracketOpen();
                        break;
                    case ')':
                        isFailed = OnBracketClose();
                        break;
                    case '+':
                        isFailed = OnOperatorInput(Operation.Plus);
                        break;
                    case '-':
                        isFailed = OnOperatorInput(Operation.Minus);
                        break;
                    case '/':
                        isFailed = OnOperatorInput(Operation.Divide);
                        break;
                    case '*':
                        isFailed = OnOperatorInput(Operation.Multiply);
                        break;
                    case '^':
                        isFailed = OnOperatorInput(Operation.Power);
                        break;
                    case '\n':
                        OnEnterInput();
                        return;
                    case ' ':
                        break;
                    case '.':
                        isFailed = OnNumberInput(',');
                        break;
                    default:
                        isFailed = OnNumberInput(c);
                        break;
                }


                if (isFailed)
                {
                    _model.Clear();
                    _view.DisplayError("error");
                    return;
                }

            }
            OnEnterInput();


        }

        public void OnCharInput(char c)
        {
            _input += c;
            _view.DisplayChar(c);
            _Refresh();
        }

        public void OnBackspace()
        {
            if (_input.Length == 0) return;
           _input = _input.Remove(_input.Length - 1);
            _view.RemoveChar();
            _Refresh();
        }

        

        public void OnEnterInput()
        {
            if (_model.Calculate(out string res))
            {
                _view.DisplayError(res);
            }
            else
            {
                _view.DisplayResult(res);
            }
        }

        public bool OnNumberInput(char c)
        {
            return _model.AddToNumber(c);
        }

        public bool OnOperatorInput(Operation op)
        {
            return _model.AddOperator(op);
        }

        public void ProcessString(string s)
        {
            _input = s;
            _Refresh();
        }
    }

}
