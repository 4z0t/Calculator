using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.Write("\b");
            Console.Write("a");
            Console.Write("\b");
            Console.Write(" c");
            (int a, int b) = Console.GetCursorPosition();
            Console.SetCursorPosition(a - 1, b);
            Console.Read();
        }
    }
}
