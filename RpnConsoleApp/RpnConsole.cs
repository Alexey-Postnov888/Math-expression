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
            /*static void Main(string[] args)
            {
                string input = GetInput().Replace(" ", string.Empty);
                int varX = int.Parse(GetVarX());
                /*RpnCalculator rpn = new RpnCalculator(input, varX);
                double result = rpn.Result;
                
                Console.WriteLine($"Результат: {result}");#1#

            }*/
            static void Main()
            {
                Console.Write("Введите выражение: ");
                string expression = Console.ReadLine();
                Console.Write("Введите значение переменной: ");
                string argument = Console.ReadLine();
                RpnCalculator calculator = new RpnCalculator(expression);
                double answer = calculator.CalculateRpn(double.Parse(argument));
                Console.WriteLine($"Ответ: {answer}");
            }
        }
    }
}