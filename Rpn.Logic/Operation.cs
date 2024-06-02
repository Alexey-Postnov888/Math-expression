namespace RpnLogic;

abstract class Operation : Token
{
    public abstract string Name { get; }
    public abstract int Priority { get; }
    public abstract int ArgsCount { get; }
    public abstract bool IsFunction { get; }

    public abstract Number Execute(params Number[] numbers);

    public override string ToString()
    {
        return Name;
    }
}

class Plus : Operation
{
    public override string Name => "+"; 
    public override int Priority => 1;
    public override int ArgsCount => 2;
    public override bool IsFunction => false;

    public override Number Execute(params Number[] numbers)
    {
        var num1 = numbers[0];
        var num2 = numbers[1];

        return num1 + num2;
    }
}

class Minus : Operation
{
    public override string Name => "-";
    public override int Priority => 1;
    public override int ArgsCount => 2;
    public override bool IsFunction => false;
    public override Number Execute(params Number[] numbers)
    {
        var num1 = numbers[0];
        var num2 = numbers[1];

        return num1 - num2;
    }
}

class Multiply : Operation
{
    public override string Name => "*";
    public override int Priority => 1;
    public override int ArgsCount => 2;
    public override bool IsFunction => false;
    public override Number Execute(params Number[] numbers)
    {
        var num1 = numbers[0];
        var num2 = numbers[1];

        return num1 * num2;
    }
}

class Devision : Operation
{
    public override string Name => "/";
    public override int Priority => 1;
    public override int ArgsCount => 2;
    public override bool IsFunction => false;
    public override Number Execute(params Number[] numbers)
    {
        var num1 = numbers[0];
        var num2 = numbers[1];

        return num1 / num2;
    }
}

class Sin : Operation
{
    public override string Name => "sin";
    public override int Priority => 1;
    public override int ArgsCount => 1;
    public override bool IsFunction => true;
    public override Number Execute(params Number[] numbers)
    {
        var num1 = numbers[0];

        return new Number(Math.Sin(num1.Symbol));
    }
}

class Cos : Operation
{
    public override string Name => "cos";
    public override int Priority => 1;
    public override int ArgsCount => 1;
    public override bool IsFunction => true;
    public override Number Execute(params Number[] numbers)
    {
        var num1 = numbers[0];

        return new Number(Math.Cos(num1.Symbol));
    }
}

class Tg : Operation
{
    public override string Name => "tg";
    public override int Priority => 1;
    public override int ArgsCount => 1;
    public override bool IsFunction => true;
    public override Number Execute(params Number[] numbers)
    {
        var num1 = numbers[0];

        return new Number(Math.Tan(num1.Symbol));
    }
}

class Ctg : Operation
{
    public override string Name => "ctg";
    public override int Priority => 1;
    public override int ArgsCount => 1;
    public override bool IsFunction => true;
    public override Number Execute(params Number[] numbers)
    {
        var num1 = numbers[0];

        return new Number(1/Math.Tan(num1.Symbol));
    }
}

class log : Operation
{
    public override string Name => "log";
    public override int Priority => 1;
    public override int ArgsCount => 2;
    public override bool IsFunction => true;
    public override Number Execute(params Number[] numbers)
    {
        var num1 = numbers[0];
        var num2 = numbers[1];

        return new Number(Math.Log(num1.Symbol, num2.Symbol));
    }
}

class Power : Operation
{
    public override string Name => "^";
    public override int Priority => 1;
    public override int ArgsCount => 2;
    public override bool IsFunction => true;
    public override Number Execute(params Number[] numbers)
    {
        var num1 = numbers[0];
        var num2 = numbers[1];

        return new Number(Math.Pow(num1.Symbol, num2.Symbol));
    }
}

class Sqrt : Operation
{
    public override string Name => "sqrt";
    public override int Priority => 1;
    public override int ArgsCount => 1;
    public override bool IsFunction => true;
    public override Number Execute(params Number[] numbers)
    {
        var num1 = numbers[0];

        return new Number(Math.Sqrt(num1.Symbol));
    }
}

class Rt : Operation
{
    public override string Name => "rt";
    public override int Priority => 1;
    public override int ArgsCount => 2;
    public override bool IsFunction => true;
    public override Number Execute(params Number[] numbers)
    {
        var num1 = numbers[0];
        var num2 = numbers[1];

        return new Number(Math.Pow(num2.Symbol, 1/num1.Symbol));
    }
}