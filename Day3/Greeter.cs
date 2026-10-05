namespace Day3;

public class Greeter
{
    public virtual void Greet(User user)
    {
        Console.WriteLine($"Hello {user.Nickname}!");
    }
}

public class GermanyGreeter : Greeter
{
    public override void Greet(User user)
    {
        Console.WriteLine($"Guten Tag {user.Nickname}!");
    }
}

public class FrenchGreeter : Greeter
{
    public override void Greet(User user)
    {
        Console.WriteLine($"Bonjour {user.Nickname}!");
    }
}

