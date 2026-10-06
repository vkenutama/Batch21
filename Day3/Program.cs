namespace Day3;

using System.Numerics;
using Day3Reference;

static class Program
{
    #region Testing

    static void Main()
    {
        // TestShapeAreaAndCircumferenceCalculator();
        // TestInternalClass();
        // TestBaseAndInheritorRelation();
        // TestMethodOverriding();
        // TestSealedMethod();
        // TestOverloadingMethods();
        
        // Constructor overloading
        // TestConstructorOverloading();
        
        // Deconstructing
        Rectangle rectangle = new Rectangle(10, 10);
        (float width, float height) = rectangle;
        var (w, h) = rectangle; // Implicit typing
        rectangle.Deconstruct(out float w2, out float h2); // Explicit

        Console.WriteLine($"width: {width}, height: {height}");
        
        // Paramless class
        Bunny bunny = new Bunny
        {
            Name = "Bunny",
            LikesHumans = false
        };
        
        // This reference
        Person p1 = new Person
        {
            Name = "Peter",
        };
        Person p2 = new Person()
        {
            Name = "Agnes"
        };
        
        p1.Marry(p2);

        UIText uiText = new UIText();
        uiText.Text = "John";
        uiText.Text = "John";
        uiText.Text = "John Cena";
        
        Stock bmri = new Stock(2_000, 4000);

        Console.WriteLine($"BMRI {bmri.Worth}");
        
        TestClass testClass = new TestClass
        {
            Name = "Hello, i can set this"
        };

        testClass.Numbers[0] = 1;

        Console.WriteLine(testClass.Name);

        testClass[2] = "silver";

        foreach (var word in testClass[..]) 
        {
            Console.WriteLine(word);
        }


    }

    class TestClass
    {
        public string Name { get; init; }
        public int[] Numbers = new int[10];
        private string[] _words = "The quick brown fox".Split();

        public string this[int wordNum]
        {
            get => _words[wordNum];
            set => _words[wordNum] = value;
        }

        public string this[Index index] => _words[index];
        public string[] this[Range range] => _words[range];

    }

    class Stock
    {
        public Stock(decimal currentPrice, decimal sharesOwned)
        {
            _currentPrice = currentPrice;
            _sharesOwned = sharesOwned;
        }
        
        private decimal _currentPrice, _sharesOwned;
        public decimal Worth => _currentPrice * _sharesOwned;
    }

    class UIText
    {
        private string? text;

        public string? Text
        {
            get => text;
            set
            {
                if (value != null && value != text)
                {
                    Console.WriteLine($"Updated text from {text} to {value}");
                    text = value;
                }
            }
            
        }
    }

    class Person
    {
        public string Name { get; set; }
        public Person? Partner { get; set; }

        public void Marry(Person person)
        {
            Partner = person;
            person.Partner = this;
        }
    }

    class Bunny
    {
        public string Name;
        public bool LikesHumans;
    }

    class Rectangle
    {
        public readonly float Width, Height;

        public Rectangle(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public void Deconstruct(out float width, out float height)
        {
            width = Width;
            height = Height;
        }
    }

    private static void TestConstructorOverloading()
    {
        Wine w1 = new Wine(2_000_000M);
        Wine w2 = new Wine(2_000_000M, 1997);
        
        w1.PrintDetail();
        w2.PrintDetail();

    }

    private static void TestOverloadingMethods()
    {
        int intAdd = CustomAdder.Add(2, 2);
        float floatAdd = CustomAdder.Add(2.0f, 2.0f);
        double doubleAdd = CustomAdder.Add(2.0, 2.0);
        decimal decimalAdd = CustomAdder.Add(2000M, 2000M);
        
        CustomAdder.Add(2, 2, out int intResult);

        Console.WriteLine("====================");
        Console.WriteLine("Adder: ");
        Console.WriteLine($"Int: {intAdd}");
        Console.WriteLine($"Float: {floatAdd}");
        Console.WriteLine($"Double: {doubleAdd}");
        Console.WriteLine($"Decimal: {decimalAdd}");
        Console.WriteLine($"Int with Out: {intResult}");
        Console.WriteLine("====================");
        }


    private static void TestSealedMethod()
    {
        Worker worker = new Worker();
        worker.DoTask("Asking manager about the project");

        Manager manager = new Manager();
        manager.DoTask("Tell assistant to support");

        AssistantManager assistantManager = new AssistantManager();
        assistantManager.DoTask("Assisting manager");
    }

    private static void TestMethodOverriding()
    {
        User john = new User
        {
            FullName = "John Smith",
            Nickname = "John"
        };

        Greeter greeter = new();
        greeter.Greet(john);

        FrenchGreeter frenchGreeter = new();
        frenchGreeter.Greet(john);

        GermanyGreeter germanyGreeter = new();
        germanyGreeter.Greet(john);
    }


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