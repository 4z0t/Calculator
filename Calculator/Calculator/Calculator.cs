using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    using Interfaces;
    public class Calculator
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
            
            while (true)
            {
                var info = Console.ReadKey(true);
                c = info.KeyChar;
                
                bool isBackspace = info.Key == ConsoleKey.Backspace;
                if (c == '#') break;
                if (isBackspace)
                    _presenter.OnBackspace();
                else
                    _presenter.OnCharInput(c);

            }
        }




    }
}
