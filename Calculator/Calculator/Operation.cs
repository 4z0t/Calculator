using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public enum Operation
    {
        Plus,
        Minus,
        Divide,
        Multiply,
        Open,
        Close,
        Power,
    }

    static class OperationFunctions
    {

        public static int GetOperationPriority(Operation op)
        {
            return op switch
            {
                Operation.Divide => 2,
                Operation.Multiply => 2,
                Operation.Plus => 1,
                Operation.Minus => 1,
                Operation.Open => 0,
                Operation.Close => 0,
                Operation.Power => 3,
                _ => throw new ArgumentException("Unknow operation")
            };
        }

        public static bool Compare(Operation op1, Operation op2)
        {
            return GetOperationPriority(op1) > GetOperationPriority(op2);
        }

        public static double DoOperation(Operation op, double a, double b)
        {
            return op switch
            {
                Operation.Divide => a / b,
                Operation.Multiply => a * b,
                Operation.Plus => a + b,
                Operation.Minus => a - b,
                Operation.Power => Math.Pow(a, b),
                _ => throw new ArgumentException("Illegal operation"),
            };
        }

    }
}