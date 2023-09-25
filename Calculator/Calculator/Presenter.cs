using System;
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
                    case ',':
                        isFailed = OnOperatorInput(Operation.Comma);
                        break;
                    case '^':
                        isFailed = OnOperatorInput(Operation.Power);
                        break;
                    case '\n':
                        OnEnterInput();
                        return;
                    case ' ':
                    case '\t':
                        break;
                    case '.':
                        isFailed = OnNumberInput(',');
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        isFailed = OnNumberInput(c);
                        break;
                    default:
                        isFailed = OnSymbolInput(c);
                        break;
                }


                if (isFailed)
                {
                    _model.Clear();
                    _view.DisplayError();
                    return;
                }

            }



        }

        public void OnCharInput(char c)
        {
            if (c == ' ' || c == '\t') return;
            if (c != '\n')
            {
                _input += c;
                _view.DisplayChar(c);
            }
            _Refresh();
            OnEnterInput();
        }

        public void OnBackspace()
        {
            if (_input.Length == 0) return;
            _input = _input.Remove(_input.Length - 1);
            _view.RemoveChar();
            _Refresh();
            OnEnterInput();
        }



        public void OnEnterInput()
        {
            if (_model.Calculate(out double res))
            {
                _view.DisplayError();
            }
            else
            {
                _view.DisplayResult(res.ToString());
            }
        }
        public bool OnSymbolInput(char c)
        {
            return _model.AddSymbol(c);
        }

        public bool OnNumberInput(char c)
        {
            return _model.AddToNumber(c);
        }

        public bool OnOperatorInput(Operation op)
        {
            return _model.AddOperator(op);
        }

        public (double, bool) ProcessString(string s)
        {
            _input = s;
            _Refresh();
            bool err = _model.Calculate(out double r);
            return (r, err);
        }
    }

}
