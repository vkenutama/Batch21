void Generate(int num)
{
    string msg = "";
    for (int i = 1; i <= num; i++)
    {
        if (i % 3 == 0)
        {
            msg += "foo";
        }

        if (i % 5 == 0)
        {
            msg += "bar";
        }

        if (i % 7 == 0)
        {
            msg += "jazz";
        }

        if (i % 3 != 0 && i % 5 != 0 && i % 7 != 0)
        {
            msg += i.ToString();
        }

        if (i != num)
            msg += ", ";
    }

    Console.WriteLine(msg);
}

Generate(21);
Generate(35);
Generate(105);