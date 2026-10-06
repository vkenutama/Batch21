using System.Diagnostics.CodeAnalysis;

namespace Day4;

public class SealBase
{
    public virtual int Liability { get; set; } = 0;
    public int Number { get; set; } = 0;
    public virtual int Number2 { get; set; } = 0;
    
    public required int Number3 { get; set; } = 0;

    public SealBase()
    {
        Number = 1;
        Number2 = 2;
    }
    
    public SealBase(int number, int number2)
    {
        this.Number = number;
        this.Number2 = number2;
    }
}

public class SealSub : SealBase
{
    public SealSub(int number, int number2, int liability) : base(number, number2)
    {
        Liability = liability;
        Number = number;
        Number2 = number2;
    }
    
    [SetsRequiredMembers]
    public SealSub()
    {
        Number3 = 3;
    }

    public sealed override int Liability { get; set; } = 100;
    public new int Number { get; set; } = 100;
    public override int Number2 { get; set; } = 50;

    public virtual SealSub Clone() => new(){Number3 = 3};
}

public sealed class SealSubSub : SealSub
{
    // Cannot override inherited property 'int Day4.SealSub.Liability' because it is sealed
    // public sealed override int Liability { get; set; }
    // public sealed int Number { get; set; } = 100;
    public SealSubSub(int number, int number2, int liability) : base(number, number2, liability)
    {
        Number2 = number2;
    }

    [SetsRequiredMembers]
    public SealSubSub()
    {
        Number3 = 3;
    }

    public override int Number2 { get; set; } = 80;

    public int GetBaseNumber2()
    {
        return base.Number2;
    }

    public int GetSealBaseNumber()
    {
        SealBase s = base.Clone();
        return s.Number2;
    }
}


// Cannot inherit from sealed class 'SealSubSub'
// public class SealSubSubSub : SealSubSub
// {
//     
// }
