using System.Globalization;

namespace RpnLogic;

abstract class Token
{ 
    public new abstract string ToString();
}

class Number : Token
{
    public double Symbol { get; }

    public Number(string input)
    {
        Symbol = double.Parse(input, CultureInfo.InvariantCulture);
    }

    public Number(double num)
    {
        Symbol = num;
    }

    public override string ToString()
    {
        return Symbol.ToString(CultureInfo.InvariantCulture);
    }

    public static Number operator +(Number firstNumber, Number secondNumber)
    {
        return new Number(firstNumber.Symbol + secondNumber.Symbol);
    }

    public static Number operator -(Number firstNumber, Number secondNumber)
    {
        return new Number(firstNumber.Symbol - secondNumber.Symbol);
    }

    public static Number operator *(Number firstNumber, Number secondNumber)
    {
        return new Number(firstNumber.Symbol * secondNumber.Symbol);
    }

    public static Number operator /(Number firstNumber, Number secondNumber)
    {
        return new Number(firstNumber.Symbol / secondNumber.Symbol);
    }
}

class Paranthesis(char symbol) : Token
{ 
    public bool IsClosing { get; } = symbol == ')';
    public int Prioiry = 0;

    public override string ToString()
    {
        return IsClosing ? ")" : "(";
    }
}
    
class VarX : Token
{ 
    public char Symbol { get; }
    
    public VarX(char symbol)
    {
        Symbol = symbol;
    }

    public override string ToString()
    {
        return Symbol.ToString();
    }
}