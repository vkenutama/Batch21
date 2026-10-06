namespace Day4;

public class Stock : Asset
{
    public int SharesOwned { get; set; }

    protected override void DangerOverride()
    {
        Console.WriteLine($"Danger... {_cost}");
    }
}

public class Stock2 : AssetAbstract
{
    public long SharesOwned { get; set; }
    public decimal CurrentPrice { get; set; }
    public override decimal NetValue { get; }
}