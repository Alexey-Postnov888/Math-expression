using System;
using System.Collections.Generic;
using RpnLogic;

namespace RpnConsoleApp
{
    class RpnConsole
    {
        class RPN
        {
            static string GetInput()
            {
                Console.Write("Ваше выражение: ");
                return Console.ReadLine();
            }
            static string GetVarX()
            {
                Console.Write("Ваше значение X: ");
                return Console.ReadLine();
            }
            static void Main(string[] args)
            {
                string input = GetInput().Replace(" ", string.Empty);
                int varX = int.Parse(GetVarX());
                RpnCalculator rpn = new RpnCalculator(input, varX);
                double result = rpn.Result;

                // Console.WriteLine($"Ваше выражение в ОПЗ: {string.Join(" ", toRPN(tokens))}"); //// to do normal output
                Console.WriteLine($"Результат: {result}");

            }
        }
    }
}