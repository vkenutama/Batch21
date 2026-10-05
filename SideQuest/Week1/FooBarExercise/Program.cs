void Generate(int num)
{
    string msg = "";
    for (int i = 1; i < num; i++)
    {
        if (i % 3 == 0)
        {
            msg += "foo";
        }
        else if (i % 5 == 0)
        {
            msg += "bar";
        }
        else if (i % 3 == 0 && i % 5 == 0)
        {
            msg += "foobar";
        }
        else
        {
            msg += i.ToString();
        }

        msg += ", ";
    }

    Console.WriteLine(msg);
}

Generate(15);