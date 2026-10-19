Dictionary<int, string> ruleTable = new Dictionary<int, string>()
{
    {3, "foo"},
    {4, "baz"},
    {5, "bar"},
    {7, "jazz"},
    {9, "huzz"},
};


void Generate(int num)
{
    string msg = "";
    for (int i = 1; i <= num; i++)
    {
        bool divisible = false;
        foreach (var rule in ruleTable)
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
        
        if (i != num)
            msg += ", ";
    }

    Console.WriteLine(msg);
}

Generate(36);