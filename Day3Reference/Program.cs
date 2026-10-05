namespace Day3Reference;

class Program
{
    static void Main(string[] args)
    {
        InternalClass ic = new InternalClass();
        ic.Test(); // Can access 

        // Test defining protected internal class
        BaseClass2.ProtectedInternalClass pic = new BaseClass2.ProtectedInternalClass();
        pic.Test();
    }
}

public class BaseClass2
{
    protected internal class ProtectedInternalClass
    {
        public void Test()
        {
            Console.WriteLine("Hello from ProtectedInternalClass");
        }
    }

    private protected class PrivateProtectedClass
    {
        public void Test()
        {
            Console.WriteLine("Hello from PrivateProtectedClass");
        }
    }

    public void CallingPrivateProtectedClass()
    {
        PrivateProtectedClass ppc = new PrivateProtectedClass();
        ppc.Test();
    }

}

public class DerivedClass2 : BaseClass2
{
    public void Test()
    {
        ProtectedInternalClass pic = new ProtectedInternalClass();
        pic.Test();
    }
}

internal class InternalClass
{
    public void Test()
    {
        Console.WriteLine("Hello from method in internal class");
    }
}

public class PublicClass
{
    public void Test()
    {
        Console.WriteLine("Hello from method in public class");
    }
}


