using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    using Interfaces;
    class Calculator
    {
        private IView _view;
        private IPresenter _presenter;
        private IModel _model;

        public Calculator()
        {
            _model = new Model();
            _view = new View();
            _presenter = new Presenter(_view, _model);
        }

        public void Start()
        {
            char c;
            while(true)
            {
                var info = Console.ReadKey(true);
                c = info.KeyChar;
                bool isEnter = info.Key == ConsoleKey.Enter;
                if (c == '#') break;
                if (isEnter)
                    _presenter.OnEnterInput();
                else
                    _presenter.OnCharInput(c);

            }
        }




    }
}
