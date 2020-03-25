using System;

namespace Ctyar.Ef.Contrib
{
    internal static class Print
    {
        public static void Info(string message)
        {
            var previousColor = Console.ForegroundColor;
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);

            Console.ForegroundColor = previousColor;
        }

        public static void Error(string message)
        {
            var previousColor = Console.ForegroundColor;
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);

            Console.ForegroundColor = previousColor;
        }
    }
}
