namespace Day4;

public class House : Asset
{
    public decimal Mortgage;
    public override decimal Liability => Mortgage;

    public override House Clone() => new House { Name = Name, Mortgage = Mortgage };
}

