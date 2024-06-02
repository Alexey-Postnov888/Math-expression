namespace RpnLogic;

class TokenCreator
{
    private static List<Operation> _availableOperations;

    public static Token Create(string input)
    {
        if (char.IsDigit(input.First())) return new Number(input);
        return CreateOperation(input);
    }

    public static Token Create(char symbol, List<Char> varNames)
    {
        if (varNames.Contains(symbol)) return new VarX(symbol);
        if (symbol == '(' || symbol == ')') return new Paranthesis(symbol);

        return CreateOperation(symbol.ToString());
    }

    public static Operation CreateOperation(string name)
    {
        var operation = FindAvailableOperationByName(name);
        return operation;
    }

    private static Operation FindAvailableOperationByName(string name)
    {
        if (_availableOperations == null)
        {
            var parent = typeof(Operation);
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = allAssemblies.SelectMany(x => x.GetTypes());
            var inheritingTypes = types.Where(t => parent.IsAssignableFrom(t) && !t.IsAbstract).ToList();

            _availableOperations =
                inheritingTypes.Select(type => (Operation)Activator.CreateInstance(type)).ToList();
        }

        return _availableOperations.FirstOrDefault(op => op.Name.Equals(name));
    }
}