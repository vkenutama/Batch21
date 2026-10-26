namespace FooBarExercise4;

public class FooBarGenerator
{
    private Dictionary<int, string> _ruleTable = new Dictionary<int, string>();

    public void AddRule(int divisor, string output)
    {
        _ruleTable.TryAdd(divisor, output);
        _ruleTable = _ruleTable.OrderBy(r => r.Key).ToDictionary(r => r.Key, r => r.Value);
    }

    public bool Evaluate(int number)
    {
        return _ruleTable.All(r => number % r.Key == 0);        
    }

    public void GenerateSequence(int start, int end)
    {
        string msg = "";
        for (int i = start; i <= end; i++)
        {
            bool divisible = false;
            foreach (var rule in _ruleTable)
            {
                if (i % rule.Key == 0)
                {
                    msg += rule.Value;
                    divisible = true;
                }
            }

            if (!divisible)
            {
                msg += i.ToString();
            }
        
            if (i != end)
                msg += ", ";
        }

        Console.WriteLine(msg); 
    }
}