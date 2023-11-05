using System;
using System.Collections.Generic;

namespace Solution
{
    class Program
    {
        static void Main(string[] args)
        {
            List<object> result = RPN(GetInput().Replace(" ", string.Empty));
            string final = string.Empty;
            foreach (var c in result)
            {
                final += $"{c.ToString()} ";
            }
            
            Console.WriteLine(final);
        }
        
        static string GetInput()
        {
            Console.WriteLine("Напишите ваше выражение");
            return Console.ReadLine();
        }
 
        public static int GetPriority(char op)
        {
            switch (op)
            {
                case '(': return 0;
                case ')': return 0;
                case '+': return 1;
                case '-': return 1;
                case '*': return 2;
                case '/': return 2;
                default: return 3;
            }
        }

        public static bool IsOperator(char c)
        {
            bool check_operator;
            string string_operators = "+-*/()";
            return check_operator = (string_operators.Contains(Convert.ToString(c))) ? true : false;
        }

        public static List<object> RPN(string input)
        {
            List<object> rpn_output = new List<object>();
            Stack<char> operators = new Stack<char>();
            string number = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    number += input[i];
                }

                if (IsOperator(input[i]))
                {
                    rpn_output.Add(number);
                    number = string.Empty;
                    if (input[i] == '(')
                    {
                        operators.Push(input[i]);
                    }
                    else if (input[i] == ')')
                    {
                        while (operators.Peek() != '(')
                        {
                            rpn_output.Add(operators.Peek());
                            operators.Pop();
                        }
                        operators.Pop();
                    }
                    else
                    {
                        if (operators.Count > 0)
                        {
                            if (GetPriority(input[i]) <= GetPriority(operators.Peek()))
                            {
                                rpn_output.Add(operators.Peek());
                                operators.Pop();
                            }
                            
                            operators.Push(input[i]);
                        }
                        else
                        {
                            operators.Push(input[i]);
                        }
                    }
                }
            }

            rpn_output.Add(number);

            while (operators.Count != 0)
            {
                rpn_output.Add(operators.Peek());
                operators.Pop();
            }

            while (rpn_output.Contains(string.Empty)) rpn_output.Remove(string.Empty);
            return rpn_output;
        }

        public static int Calculation()
        {
            int result = 0;
            return result;
        }
    }
}