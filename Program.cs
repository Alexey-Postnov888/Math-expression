using System;
using System.Collections.Generic;

namespace Solution
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = GetInput().Replace(" ", String.Empty);
            char[] availableOps = new [] { '+', '-', '*', '/', '(', ')'};

            List<int> numbers = new List<int>();
            List<char> operators = new List<char>();
            string buffer = string.Empty;
            for(int i = 0; i < input.Length; i++)
            {
                if (!char.IsDigit(input[i]))
                {
                    operators.Add(input[i]);
                    buffer = SaveBuffer(numbers, buffer);
                }
                else
                {
                    buffer += input[i];
                }
            }
            
            SaveBuffer(numbers, buffer);

            // output
            Console.Write("Ваши числа: " + String.Join(", ", numbers));
            Console.WriteLine();
            Console.Write("Операнды: " + String.Join(", ", operators));
            Console.WriteLine();
            Console.WriteLine("Значение вашего выражения: " + Calculations(operators, numbers).ToString());
            
        }
        static string SaveBuffer(List<int> numbers, string buffer)
        {
            if (!string.IsNullOrEmpty(buffer))
            {
                numbers.Add(int.Parse(buffer));
            }
            
            return string.Empty;
        }

        static int Calculations(List<char> operators, List<int> numbers)
        {
            for (int i = 0; i < operators.Count; i++)
            {
                char op = operators[i];
                
                if ((op == '*') || (op == '/'))
                {
                    int result = (op == '*') ? numbers[i] * numbers[i + 1] : numbers[i] / numbers[i + 1];

                    Remove(numbers, operators, i, result);
                }
            }

            for (int i = 0; i < operators.Count; i++)
            {
                char op = operators[i];
                
                if ((op == '+') || (op == '-'))
                {
                    int result = (op == '+') ? numbers[i] + numbers[i + 1] : numbers[i] - numbers[i + 1];

                    Remove(numbers, operators, i, result);
                }
            }

            return numbers[0];
        }

        public static void Remove(List<int> numbers, List<char> operators, int i, int result)
        {
            numbers[i] = result;
            numbers.RemoveAt(i + 1);
            operators.RemoveAt(i);
            i--;
        }

        static string GetInput()
        {
            Console.WriteLine("Напишите ваше выражение");
            return Console.ReadLine();
        }
    }
}