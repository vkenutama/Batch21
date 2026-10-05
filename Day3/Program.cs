namespace Day3;

using System.Numerics;
using Day3Reference;


static class Program
{
    static void Main()
    {
        // TestShapeAreaAndCircumferenceCalculator();
        // TestInternalClass();
        TestBaseAndInheritorRelation();
    }


    #region Testing

    private static void TestBaseAndInheritorRelation()
    {
        BaseClass baseClass = new BaseClass();
        BaseClass i1 = new InheritorClass();
        InheritorClass inheritorClass = new InheritorClass();
        InheritorClass2 inheritorClass2 = new InheritorClass2();
        
        // Method hiding
        baseClass.TestMethodHiding(); // Hello from Method Hiding method in BaseClass
        i1.TestMethodHiding(); // Hello from Method Hiding method in BaseClass
        inheritorClass.TestMethodHiding(); // Hello from Inheritor class
        
        // Test calling protected class from inheritor
        inheritorClass.TestProtectedClass();
        
        // Test calling private class from base class
        baseClass.CallPrivateClass();
        
        // Test calling protected internal class
        inheritorClass2.TestProtectedInternalClass();
        


    }

    static void TestShapeAreaAndCircumferenceCalculator()
    {
        Square<double> s = new Square<double>(5.0);
        Triangle<double> t = new Triangle<double>(12.0, 8.0, null, triangleType: TriangleType.RightAngled);
        Circle<double> c = new Circle<double>(7.0);

        var getAreaAndCircumferenceText = (IShape<double> shape) =>
            $"Area: {shape.GetArea():F2}, Circumference: {shape.GetCircumference():F2}";

        Console.WriteLine($"Square {getAreaAndCircumferenceText(s)}");
        Console.WriteLine($"Triangle {getAreaAndCircumferenceText(t)}");
        Console.WriteLine($"Circle {getAreaAndCircumferenceText(c)}");


        TriangleType tType = TriangleType.RightAngled;

        string triangleType = tType switch
        {
            TriangleType.RightAngled => "Right-Angled",
            // TriangleType.Isosceles => "Isosceles",
            _ => throw new ArgumentOutOfRangeException()
        };

        Console.WriteLine($"Triangle {triangleType}");
    }

    static void TestInternalClass()
    {
        PublicClass pc = new PublicClass();
        pc.Test();

        /*
         * Error
         * Cannot access internal class 'InternalClass' here
         */
        // InternalClass ic = new InternalClass();
        // ic.Test(); 
    }

    #endregion

    class FooClass
    {
        public int Foo(int x) => x * 2;
        // public void Foo(int x) => Console.WriteLine(x); // Error Member with the same signature is already declared
    }

    class Version
    {
        public readonly decimal ProgramVersion = 4.2M;
    }

    interface IShape<T>
    {
        T GetArea();
        T GetCircumference();
    }

    class Square<T> : IShape<T> where T : IFloatingPointIeee754<T>
    {
        private T _sideLength;

        public Square(T sideLength)
        {
            _sideLength = sideLength;
        }

        public T GetArea() => _sideLength * _sideLength;

        public T GetCircumference() => T.CreateChecked(4) * _sideLength;
    }

    class Triangle<T> : IShape<T> where T : struct, IFloatingPointIeee754<T>
    {
        private T _base;
        private T _height;
        private T? _hypotenuse;

        private TriangleType _triangleType = TriangleType.RightAngled;

        public Triangle(T baseLength, T height, T? hypotenuse, TriangleType triangleType)
        {
            _base = baseLength;
            _height = height;
            _triangleType = triangleType;
            _hypotenuse ??= hypotenuse;
        }

        public T GetArea()
        {
            return T.CreateChecked(0.5f) * _base * _height;
        }

        public T GetCircumference()
        {
            T hypotenuse = _hypotenuse ?? T.Sqrt((_base * _base) + (_height * _height));
            return _base + _height + hypotenuse;
        }
    }

    class Circle<T>(T radius) : IShape<T>
        where T : struct, IFloatingPointIeee754<T>
    {
        private T _diameter = T.CreateChecked(radius) * radius;

        private const double Pi = Math.PI;

        public T GetArea() => T.CreateChecked(Pi) * radius * radius;
        public T GetCircumference() => T.CreateChecked(2 * Pi) * radius;
        public T GetDiameter() => _diameter;
    }

    enum TriangleType
    {
        RightAngled,
        Isosceles,
    }

    static class Console2
    {
        static void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}

class BaseClass
{
    protected void Test()
    {
        Console.WriteLine("Hello from Protected Method in BaseClass");
    }

    public void TestMethodHiding()
    {
        Console.WriteLine("Hello from Method Hiding method in BaseClass");
    }

    public void CallPrivateClass()
    {
        PrivateClass pc = new PrivateClass();
        pc.Test();
    }

    #region Classes
    protected class ProtectedClass
    {
        public void Test()
        {
            Console.WriteLine("Hello from protected class");
        }
    }

    private class PrivateClass
    {
        public void Test()
        {
            Console.WriteLine("Hello from private class");
        }
    }
    #endregion
    
    
    
}

class InheritorClass2 : BaseClass2
{
    public void TestProtectedInternalClass()
    {
        ProtectedInternalClass pic = new ProtectedInternalClass();
        pic.Test();
    }
}

class InheritorClass : BaseClass
{
    public void CallPrivateClass()
    {
        /*
         * Error
         * Cannot access private class 'PrivateClass' here
         */
        // PrivateClass pc = new PrivateClass();
        // pc.Test();   
    }
    
    public void TestProtectedClass()
    {
        ProtectedClass p = new ProtectedClass(); // allowed 
        p.Test();        
    }

    public new void TestMethodHiding()
    {
        Console.WriteLine("Hello from Inheritor class");
    }
}

class NonInheritorClass
{
    void Test()
    {
        BaseClass b = new BaseClass();
        
        /*
         * Error:
         * Cannot access protected class 'ProtectedClass' here
         */
        // BaseClass.ProtectedClass protectedClass= new BaseClass().ProtectedClass();

    }
}