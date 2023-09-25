using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    using Interfaces;
    public class Model : IModel
    {

        enum LastInput
        {
            Operation,
            Number,
            Open,
            Close,
        }

        public Model()
        {
            _ops = new Stack<Operation>();
            _res = new Stack<object>();
        }

        private Stack<Operation> _ops;
        private string _buff;
        private LastInput _last;
        private Stack<object> _res;

        public bool AddOperator(Operation op)
        {
            if (_last == LastInput.Open && op == Operation.Minus)
            {
                AddToNumber('0');
            }
            if (_last == LastInput.Operation)
                return true;
            if (_last == LastInput.Number)
            {
                _res.Push(_buff);
                _buff = "";
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
            _buff += c;
            _last = LastInput.Number;
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
            if (_last == LastInput.Number) _res.Push(_buff);
            while (_ops.Count != 0) _res.Push(_ops.Pop());

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
                _res.Push(_buff);
                _buff = "";
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
            _buff = "";
            _last = LastInput.Open;
            _ops.Clear();
            _res.Clear();
        }
    }
}
