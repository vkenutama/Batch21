namespace Day4;

public class Asset
{
    public string Name { get; set; }
    public virtual decimal Liability => 0;

    protected float _cost;

    public Asset()
    {
        DangerOverride();
    }

    protected virtual void DangerOverride()
    {
        _cost = 1f;
    }

    public virtual Asset Clone() => new() { Name = Name};
}

public abstract class AssetAbstract
{
    public string Name { get; set; }
    public abstract decimal NetValue { get; }
}