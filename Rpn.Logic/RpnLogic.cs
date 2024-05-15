namespace RpnLogic
{
    class Token;

    class Number : Token
    {
        public double Symbol { get; }
        public char SymbolX { get; }

        public Number(double num)
        {
            Symbol = num;
        }
        
        public Number(char symbolX)
        {
            SymbolX = symbolX;
        }

        public static bool CheckX(char symbolX)
        {
            return symbolX is 'x' or 'X' or 'х' or 'Х';
        }
    }

    class Operation(char symbol) : Token
    {
        public char Symbol { get; } = symbol;
        public int Priority { get; } = GetPriority(symbol);

        private static int GetPriority(char symbol)
        {
            switch (symbol)
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
    }

    class Paranthesis(char symbol) : Token
    {
        public bool IsClosing { get; } = symbol == ')';
    }

    public class RpnCalculator
    {
        public readonly double Result;
        public RpnCalculator(string expression)
        {
            List<Token> rpn = ToRpn(Tokenize(expression));
            Result = CalculateWithoutX(rpn);
        }
        public RpnCalculator(string expression, int varX)
        {
            List<Token> rpn = ToRpn(Tokenize(expression));
            Result = CalculateWithX(rpn, varX);
        }
        private List<Token> Tokenize(string input)
        {
            List<Token> tokens = new List<Token>();
            string number = string.Empty;
            foreach (var c in input)
            {
                if (char.IsDigit(c))
                {
                    number += c;
                }
                else if (c == ',' || c == '.')
                {
                    number += ",";
                }
                else if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    if (number != string.Empty)
                    {
                        tokens.Add(new Number(double.Parse(number)));
                        number = string.Empty;
                    }
                    tokens.Add(new Operation(c));
                }
                else if (c == '(' || c == ')')
                {
                    if (number != string.Empty)
                    {
                        tokens.Add(new Number(double.Parse(number)));
                        number = string.Empty;
                    }
                    tokens.Add(new Paranthesis(c));
                }
                else if (Number.CheckX(c))
                {
                    tokens.Add(new Number(c));
                }
            }

            if (number != string.Empty)
            {
                tokens.Add(new Number(double.Parse(number)));
            }

            return tokens;
        }


        private static List<Token> ToRpn(List<Token> tokens)
        {
            List<Token> rpnOutput = new List<Token>();
            Stack<Token> operators = new Stack<Token>();

            foreach (Token token in tokens)
            {
                if (operators.Count == 0 && token is not Number)
                {
                    operators.Push(token);
                    continue;
                }

                if (token is Operation)
                {
                    if (operators.Peek() is Paranthesis)
                    {
                        operators.Push(token);
                        continue;
                    }

                    Operation first = (Operation)token;
                    Operation second = (Operation)operators.Peek();

                    if (first.Priority > second.Priority)
                    {
                        operators.Push(token);
                    }
                    else if (first.Priority <= second.Priority)
                    {
                        while (operators.Count > 0 && token is not Paranthesis)
                        {
                            rpnOutput.Add(operators.Pop());
                        }
                        operators.Push(token);
                    }
                }
                else if (token is Paranthesis paranthesis)
                {
                    if (paranthesis.IsClosing)
                    {
                        while (operators.Peek() is not Paranthesis)
                        {
                            rpnOutput.Add(operators.Pop());
                        }

                        operators.Pop();
                    }
                    else
                    {
                        operators.Push(paranthesis);
                    }
                }
                else if (token is Number num)
                {
                    rpnOutput.Add(num);
                }
            }

            while (operators.Count > 0)
            {
                rpnOutput.Add(operators.Pop());
            }
            return rpnOutput;
        }
        private static double CalculateOperation(double first, double second, char operation)
        {
            double result = 0;

            switch (operation)
            {
                case '+': result = first + second; break;
                case '-': result = second - first; break;
                case '*': result = first * second; break;
                case '/': result = second / first; break;
            }

            return result;
        }

        private static double CalculateWithoutX(List<Token> rpnCalc)
        {
            Stack<double> tempCalc = new Stack<double>();

            foreach (Token token in rpnCalc)
            {
                if (token is Number num)
                {
                    tempCalc.Push(num.Symbol);
                }
                else if (token is Operation)
                {
                    double first = tempCalc.Pop();
                    double second = tempCalc.Pop();
                    var op = (Operation)token;
                    double result = CalculateOperation(first, second, op.Symbol);
                    tempCalc.Push(result);
                }
            }

            return tempCalc.Peek();
        }

        private static double CalculateWithX(List<Token> rpnCalc, int varX)
        {
            Stack<double> tempCalc = new Stack<double>();

            foreach (Token token in rpnCalc)
            {
                if (token is Number num)
                {
                    if (Number.CheckX(num.SymbolX))
                    {
                        tempCalc.Push(varX);
                    }
                    else
                    {
                        tempCalc.Push(num.Symbol);
                    }
                }
                else if (token is Operation)
                {
                    double first = tempCalc.Pop();
                    double second = tempCalc.Pop();
                    var op = (Operation)token;
                    double result = CalculateOperation(first, second, op.Symbol);
                    tempCalc.Push(result);
                }
            }

            return tempCalc.Peek();
        }

    }
}
