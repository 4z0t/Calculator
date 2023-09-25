using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    using Interfaces;
    using System.Collections;

    public class Model : IModel
    {
        interface IFunction
        {
            double Call(List<double> args);
        }

        class LogFunction : IFunction
        {
            public double Call(List<double> args)
            {
                if (args.Count != 2)
                {
                    throw new ArgumentException(string.Format("Function Log: expected 2 arguments, got %d", args.Count));
                }
                double a = args[0];
                double b = args[1];
                return Math.Log(a, b);
            }
        }



        class StringBuffer
        {
            public StringBuffer()
            {
                _buff = new StringBuilder();
            }
            public string Flush()
            {
                string s = _buff.ToString();
                _buff.Clear();
                return s;
            }

            public void Clear()
            {
                _buff.Clear();
            }

            public void Append(char c)
            {
                _buff.Append(c);
            }

            public override string ToString()
            {
                return _buff.ToString();
            }

            private StringBuilder _buff;
        }


        enum LastInput
        {
            Operation,
            Number,
            Open,
            Close,
            Comma,
            Function,
        }

        public Model()
        {
            _last = LastInput.Open;
            _ops = new Stack<Operation>();
            _res = new Stack<object>();
            _buff = new StringBuffer();
            _func = new StringBuffer();
        }

        static private Dictionary<string, IFunction> FUNCTIONS = new Dictionary<string, IFunction>
        {
            {"log", new LogFunction() },
        };

        private Stack<Operation> _ops;
        private StringBuffer _buff;
        private StringBuffer _func;
        private LastInput _last;
        private Stack<object> _res;

        public bool AddOperator(Operation op)
        {
            if (_last == LastInput.Function)
            {
                return true;
            }
            if (_last == LastInput.Open && op == Operation.Minus)
            {
                AddToNumber('0');
            }
            if (_last == LastInput.Operation)
                return true;
            if (_last == LastInput.Number)
            {
                _res.Push(_buff.Flush());
            }
            if (_ops.Count == 0 || OperationFunctions.GetOperationPriority(op) > OperationFunctions.GetOperationPriority(_ops.Peek()))
            {
                _ops.Push(op);
            }
            else
            {
                while (_ops.Count != 0 && OperationFunctions.GetOperationPriority(op) <= OperationFunctions.GetOperationPriority(_ops.Peek()))
                {
                    _res.Push(_ops.Pop());
                }
                _ops.Push(op);
            }

            _last = LastInput.Operation;
            return false;
        }

        public bool AddToNumber(char c)
        {
            if (_last == LastInput.Close)
                return true;
            _buff.Append(c);
            _last = LastInput.Number;
            return false;
        }

        public bool AddSymbol(char c)
        {
            if (_last == LastInput.Close || _last == LastInput.Number)
                return true;
            _func.Append(c);
            _last = LastInput.Function;
            return false;
        }

        public bool Calculate(out double result)
        {
            if (_last != LastInput.Close && _last != LastInput.Number)
            {
                result = 0;
                _Clear();
                return true;
            }
            if (_last == LastInput.Number)
                _res.Push(_buff.ToString());
            while (_ops.Count != 0)
                _res.Push(_ops.Pop());

            if (_Process(out double res))
            {
                result = 0;
                _Clear();
                return true;
            }
            result = res;
            _Clear();
            return false;
        }

        public bool CloseBracket()
        {
            if (_last == LastInput.Open || _last == LastInput.Operation || _ops.Count == 0)
                return true;
            if (_last == LastInput.Number)
            {
                _res.Push(_buff.Flush());
            }

            while (_ops.Peek() != Operation.Open)
            {
                _res.Push(_ops.Pop());
                if (_ops.Count == 0)
                {
                    _Clear();
                    return true;
                }
            }
            _ops.Pop();
            _last = LastInput.Close;

            return false;
        }

        public bool OpenBracket()
        {
            if (_last == LastInput.Function)
            {
                if (!FUNCTIONS.ContainsKey(_func.ToString()))
                {
                    return true;
                }
                var func = FUNCTIONS[_func.Flush()];

                _res.Push(func);
                _ops.Push(Operation.Function);
            }
            else
            if (_last == LastInput.Close || _last == LastInput.Number)
                return true;
            _ops.Push(Operation.Open);
            _last = LastInput.Open;
            return false;
        }


        private bool _Process(out double res)
        {
            res = 0;
            Stack<object> reversedRes = new Stack<object>(_res);
            Stack<double> nums = new Stack<double>();

            object top;
            while (reversedRes.Count != 0)
            {
                top = reversedRes.Pop();
                if (top is string s)
                {

                    if (!double.TryParse(s, out double d))
                    {
                        return true;
                    }
                    nums.Push(d);
                }
                else
                {
                    Operation op = (Operation)top;
                    if (op == Operation.Close || op == Operation.Open)
                        return true;

                    double d1 = nums.Pop();
                    if (nums.Count == 0) return true;
                    double d2 = nums.Pop();
                    double r = OperationFunctions.DoOperation(op, d2, d1);
                    if (double.IsInfinity(r) || double.IsNaN(r)) return true;
                    nums.Push(r);
                }
            }
            res = nums.Pop();
            return false;
        }



        public void Clear() => _Clear();
        private void _Clear()
        {
            _buff.Clear();
            _func.Clear();
            _last = LastInput.Open;
            _ops.Clear();
            _res.Clear();
        }

    }
}
