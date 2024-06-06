using System.Globalization;

namespace RpnLogic
{ 
    public class RpnCalculator
    {
        private readonly List<Token> _rpn;
        private readonly List<Char> _varNames = ['x'];

        public RpnCalculator(string expression)
        {
            _rpn = ToRpn(Tokenize(expression));
        }
        
        private List<Token> Tokenize(string input)
        {
            List<Token> tokens = new List<Token>();
            string token = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (char.IsDigit(c) || c == '.' || c == ',')
                {
                    token += (c == ',' ? '.' : c);
                }
                else if (char.IsLetter(c))
                {
                    if (!string.IsNullOrEmpty(token) && (char.IsDigit(token.Last()) || token.Last() == '.'))
                    {
                        tokens.Add(new Number(token));
                        token = string.Empty;
                    }
                    token += c;
                }
                else
                {
                    if (!string.IsNullOrEmpty(token))
                    {
                        var operation = TokenCreator.CreateOperation(token);
                        if (operation != null && operation.IsFunction)
                        {
                            tokens.Add(operation);
                        }
                        else if (char.IsLetter(token.First()))
                        {
                            tokens.Add(new VarX(token.First()));
                        }
                        else
                        {
                            tokens.Add(new Number(token));
                        }
                        token = string.Empty;
                    }

                    tokens.Add(TokenCreator.Create(c, _varNames));
                }
            }

            if (!string.IsNullOrEmpty(token))
            {
                var operation = TokenCreator.CreateOperation(token);
                if (operation != null && operation.IsFunction)
                {
                    tokens.Add(operation);
                }
                else if (char.IsLetter(token.First()))
                {
                    tokens.Add(new VarX(token.First()));
                }
                else
                {
                    tokens.Add(new Number(token));
                }
            }

            return tokens;
        }
        
        private static List<Token> ToRpn(List<Token> tokens)
        {
            List<Token> rpnOutput = new List<Token>();
            Stack<Token> operators = new Stack<Token>();

            foreach (var token in tokens)
            {
                if (operators.Count == 0 && token is not Number)
                {
                    operators.Push(token);
                    continue;
                }
                
                if (token is Number || token is VarX)
                {
                    rpnOutput.Add(token);
                }

                else if (token is Operation op)
                {
                    if (operators.Count == 0 || operators.Peek() is Paranthesis || (operators.Peek() is Operation operation && operation.Priority < op.Priority))
                    {
                        operators.Push(token);
                    }
                    else
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
            }

            while (operators.Count > 0)
            {
                rpnOutput.Add(operators.Pop());
            }

            return rpnOutput;
        }
        public double CalculateRpn(double valueX)
        {
            return Calculate(valueX).Symbol;
        }

        private Number Calculate(double valueX)
        {
            Stack<Number> tempCalc = new Stack<Number>();

            foreach (Token token in _rpn)
            {
                if (token is Number num)
                {
                    tempCalc.Push(num);
                }
                else if (token is VarX varX)
                {
                    tempCalc.Push(new Number(valueX));
                }
                else
                {
                    var op = token as Operation;

                    if (op != null)
                    {
                        var args = new Number[op.ArgsCount];
                        for (var i = op.ArgsCount - 1; i >= 0; i--) args[i] = tempCalc.Pop();

                        var result = op.Execute(args);
                        tempCalc.Push(result);
                    }
                }
            }

            return tempCalc.Peek();
        }
    }
}