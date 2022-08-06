namespace NeoMath;

static public class Calculator
{
    internal class Token
    {
        public enum Type { NUMBER, BRACKET, OPERATION }

        public string Value { get; init; }
        public Type TypeToken { get; init; }

        public Token(string value, Type type)
        {
            Value = value;
            TypeToken = type;
        }
    }

    internal class Operation
    {
        private class Type
        {
            public char Symbol { get; init; }
            public int Priority { get; init; }

            public Func<Double, Double, Double> GetResult { get; init; }

            public Type(char symbol, int priority, Func<Double, Double, Double> getResult)
            {
                Symbol = symbol;
                Priority = priority;
                GetResult = getResult;
            }
        }

        private static Type[] Operations = {
            new Type(symbol: '+', priority: 1, getResult: (a, b) => a + b),
            new Type(symbol: '-', priority: 1, getResult: (a, b) => a - b),
            new Type(symbol: '*', priority: 2, getResult: (a, b) => a * b),
            new Type(symbol: '/', priority: 2, getResult: (a, b) => a / b),
        };

        private static Type? GetOperation(char opr)
        {
            foreach (var operation in Operations) if (opr == operation.Symbol) return operation;
            return null;
        }

        public static int? GetPriority(char opr) => GetOperation(opr)?.Priority;

        public static double? Calculate(char opr, double a, double b) => GetOperation(opr)?.GetResult(a, b);

        public static bool IsOperation(char opr) => GetOperation(opr) is not null? true : false;
    }

    public static double Calculate(string arg)
    {
        Stack<double> numberStack = new();
        Stack<Token> operationStack = new();

        List<Token> tokens = Lexer(arg);

        void emptyStackOperation(Token? token = null)
        {
            while (operationStack.TryPop(out var opr))
            {
                if (opr.Value != "(" || token is null)
                {
                    bool isCal;
                    if (token is null || token?.Value == ")") 
                    {
                        isCal = true;
                    }
                    else
                    {
                        isCal = Operation.GetPriority(opr.Value[0]) >= Operation.GetPriority(token?.Value[0] ?? '1');
                    }

                    if (isCal)
                    {
                        var b = numberStack.Pop();
                        var a = numberStack.Pop();

                        numberStack.Push(Operation.Calculate(opr: opr.Value[0], a: a, b: b) ?? 0);
                    }
                    else
                    {
                        operationStack.Push(opr);
                        break;
                    }
                }
                else
                {
                    operationStack.Push(opr);
                    break;
                }
            }
        }

        foreach (var token in tokens)
        {
            if (token.TypeToken == Token.Type.NUMBER)
            {
                numberStack.Push(Convert.ToDouble(token.Value));
            }
            else if (token.TypeToken == Token.Type.OPERATION)
            {
                emptyStackOperation(token: token);

                operationStack.Push(token);
            }
            else
            {
                if (token.Value == "(")
                {
                    operationStack.Push(token);
                }
                else
                {
                    emptyStackOperation(token: token);

                    if (operationStack.TryPop(out var opr))
                    {
                        if (opr.Value != "(")
                        {
                            operationStack.Push(opr);
                            operationStack.Push(token);
                        }
                    }
                }
            }
        }

        emptyStackOperation();

        return numberStack.Pop();
    }

   private static List<Token> Lexer(string arg)
    {
        List<Token> tokens = new();

        string buffer = "";
        Token.Type? type = null;

        void setType(Token.Type? t)
        {
            if (type is null)
            {
                type = t;
            }
            else if (type != t)
            {
                if (buffer != "")
                {
                    tokens.Add(new Token(buffer, type ?? Token.Type.NUMBER));
                    buffer = "";
                }
                type = t;
            }
            else if (type == Token.Type.BRACKET)
            {
                tokens.Add(new Token(buffer, type ?? Token.Type.NUMBER));
                buffer = "";
            }
        }

        foreach (char s in arg)
        {
            switch (s)
            {
                case char i when (i >= '0' && i <= '9'): setType(Token.Type.NUMBER); break;

                case var opr when Operation.IsOperation(opr): setType(Token.Type.OPERATION); break;

                case char bracket when bracket == '(' || bracket == ')': setType(Token.Type.BRACKET); break;

                default: continue;
            }

            buffer += s;
        }

        tokens.Add(new Token(value: buffer, type: type ?? Token.Type.NUMBER));

        //2(2+2) -> 2*(2+2)
        for (int i = tokens.Count - 2; i >= 0; i--)
        {
            if (tokens[i].TypeToken == Token.Type.NUMBER && tokens[i+1].Value == "("
            ||  tokens[i+1].TypeToken == Token.Type.NUMBER && tokens[i].Value == ")")
            {
                tokens.Insert(i+1, new Token("*", Token.Type.OPERATION));
            }
        }

        return tokens;
    }
}