namespace Day4;

class Program
{
    static void Main(string[] args)
    {
        TestInheritance();
        // TestAbstractClass();
        // TestHideInheritedMembers();
        // TestOverriderHider();
        // TestBaseInheritor();
        // TestObjectType();

        // Class1 c1 = new Class1();
        // c1.Y = 1;
        //
        // Indexer indexer = new Indexer();
        //
        // for (int i = 0; i < 9; i++)
        // {
        //     indexer.Set(i, i * 2);
        // }
        //
        // Console.WriteLine(indexer.ToString());
        //
        // Widget w = new Widget();
        //
        // ((I1)w).Foo();
        // Console.WriteLine(((I2)w).Foo());
        //
        // RichTextBox rtb = new RichTextBox();
        // rtb.Undo();
        // ((IUndoable)rtb).Undo();
        // ((TextBox)rtb).Undo();
        //
        // Console.WriteLine(LedSide.Left.ToString());

        // int digitalPins = (int)LedSide.Left | (int)LedSide.Right;
        //
        // if ((digitalPins & (int)LedSide.Left) != 0)
        // {
        //     Console.WriteLine("Left led on");
        // }
        // if ((digitalPins & (int)LedSide.Right) != 0)
        // {
        //     Console.WriteLine("Right led on");
        // }
        //
        // if((digitalPins & (int)LedSide.Center) != 0)
        // {
        //     Console.WriteLine("Center on");
        // }
        // else
        // {
        //     Console.WriteLine("Center led off");
        // }

        // Console.WriteLine(Math<int>.Max(3, 5));

        // Generics<Gen> gen = new();
        // gen.Assign(new Gen());

        Balloon b1 = new Balloon
        {
            Color = Color.Red,
            CC = 1234
        };

        Balloon? b2 = new Balloon()
        {
            Color = Color.Blue,
            CC = 9876
        };

        Console.WriteLine(b1.Equals(b2));

        if (b1 is Balloon b3)
        {
            Console.WriteLine(b1.Equals(b3));
        }
        
        List<Bear> bears = new List<Bear>()
        {
            new Bear
            {
                Name = "Bear 1"
            },
            new Bear
            {
                Name = "Bear 2"
            },
            new Bear
            {
                Name = "Bear 3"
            },
        };

        IEnumerable<Animal> animals = bears;

        object a = new object();

        Console.WriteLine(a.Equals(a));
        

    }
    
    
    #region Generics

    class Animal
    {
        public string Name { get; set; }
    }

    class Bear : Animal
    {
        
    }
    
    class Balloon : IEquatable<Balloon>
    {
        public Color Color { get; set; }
        public int CC { get; set; }

        public bool Equals(Balloon? other)
        {
            if (other is null) return false;
            return this.Color == other.Color && this.CC == other.CC;
        }
    }

    enum Color
    {
        Red,
        Blue,
        Orange
    }

    class Generics<T> where T : struct
    {
        private object? _value;

        public void Assign<T>(T? value)
        {
            _value = value;
        }
    }

    class Gen
    {
        public Gen()
        {
        }
    }

    #endregion

    class Math<T>
    {
        public static T Max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }
    }

    [Flags]
    enum LedSide
    {
        None,
        Left = 1 << 0,
        Center = 1 << 1,
        Right = 1 << 2,
        LeftCenter = Left | Center,
        LeftRight = Left | Right,
        RightCenter = Right | Center,
        All = Left | Center | Right
    }

    #region Interface

    interface IUndoable
    {
        void Undo();
    }

    public class TextBox : IUndoable
    {
        public virtual void Undo()
        {
            Console.WriteLine("Textbox undo");
        }
    }

    public class RichTextBox : TextBox, IUndoable
    {
        // public override void Undo()
        // {
        //     Console.WriteLine("Rich textbox undo");
        // }
        public new void Undo()
        {
            Console.WriteLine("RichTextBox undo");
        }
    }

    #endregion

    interface I1
    {
        void Foo();
    }

    interface I2
    {
        int Foo();
    }

    class Widget : I1, I2
    {
        public void Foo()
        {
            Console.WriteLine("Widget Foo");
        }

        int I2.Foo()
        {
            return 1;
        }
    }


    #region Access Modifiers

    class Indexer
    {
        private int[] _data = new int[10];

        public void Set(int index, int value)
        {
            _data[index] = value;
        }

        public int this[int index] => _data[index];
        public int this[Index index] => _data[index];
        public int[] this[Range range] => _data[range];

        public override string ToString()
        {
            string result = "Data [";
            foreach (var d in _data)
            {
                result += $"{d}, ";
            }

            result += "]";

            return result;
        }
    }


    class Class1
    {
        internal int A { get; set; }
        protected internal int Y;
        protected virtual int X { get; set; } = 0;

        private protected int Z;

        public Class1(int x)
        {
            X = x;
        }

        public Class1()
        {
        }
    }

    class Class2 : Class1
    {
        protected override int X { get; set; } = 10;

        public Class2()
        {
            Y = 10;
            Z = 20;
        }
    }

    #endregion


    #region The object Type

    public class Stack2
    {
        private int _position = 0;
        private object[] data = new object[10];

        public void Push(object obj)
        {
            data[_position++] = obj;
        }

        public object Pop()
        {
            return data[--_position];
        }
    }


    public static void TestObjectType()
    {
        Stack2 stack = new Stack2();
        stack.Push(1);
        stack.Push(2);
        stack.Push("Hello");
        stack.Push(2.0f);
        stack.Push(8M);
        stack.Push("String");

        string s = (string)stack.Pop();

        // Boxing 
        object o = 1000;
        object o2 = 3.14;

        // Unboxing
        long
            l = (int)o; // Although it is converted to long, it needs (int) casting as unboxing need specific type that match

        // Unboxing double cast
        int pi = (int)(double)o2; // Also OK, convert to double and then to int

        // Overriding object base class
        Person p = new Person
        {
            Name = "John smith",
            Age = 34
        };

        Console.WriteLine(p.ToString());
    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $@"Person Name: {Name}, Age: {Age}";
        }
    }

    #endregion

    #region Testing 2

    public static void TestBaseInheritor()
    {
        InheritorClass2 ic2 = new InheritorClass2(10);

        Asset a = new();
        House b = new House();
        Asset a1 = new House();

        WhatIsThisClass(a);
        WhatIsThisClass(b);
        WhatIsThisClass(a1);
        WhatIsThisClass((dynamic)a1);
    }

    static void WhatIsThisClass(Asset asset)
    {
        Console.WriteLine("This class is Asset");
    }

    static void WhatIsThisClass(House house)
    {
        Console.WriteLine("This class is House");
    }

    class BaseClass2
    {
        public int X { get; set; }

        public BaseClass2(int x)
        {
            X = x;
        }
    }

    class InheritorClass2 : BaseClass2
    {
        public int Y { get; set; }

        public InheritorClass2(int y) : base(y + 1)
        {
            this.Y = y;
        }
    }

    #endregion

    #region TestingSealing

    public static void TestSealing()
    {
        SealSubSub sss = new()
        {
            Number3 = 30
        };

        SealSub ss = new();
        // sss.Liability = 10; // Still can access the Liability in SealSub.Liability

        Console.WriteLine(sss.Liability);
        Console.WriteLine(sss.GetBaseNumber2());
        Console.WriteLine(sss.GetSealBaseNumber());
        Console.WriteLine(sss.Number3);

        sss.Number3 = 10; // Ok, required can be modified afterward
        Console.WriteLine(sss.Number3);

        SealSubSub? sss2 = ss as SealSubSub;
    }

    #endregion

    #region Testing Overrider vs Hider

    public static void TestOverriderHider()
    {
        // Overrider
        Overrider over = new Overrider();
        BaseClass b1 = over;
        over.Foo();
        b1.Foo(); // Notes: when upcasting to baseclass, if there's an override, use the override


        Hider h = new Hider();
        BaseClass b2 = h;
        h.Foo();
        b2.Foo(); // Notes: when upcasting to baseclass, the method hider is not available in baseclass, hence calling the BaseClass.Foo
    }

    public class BaseClass
    {
        public virtual void Foo()
        {
            Console.WriteLine("BaseClass.Foo");
        }
    }

    public class Overrider : BaseClass
    {
        public override void Foo()
        {
            Console.WriteLine("Overrider.Foo");
        }
    }

    public class Hider : BaseClass
    {
        public new void Foo()
        {
            Console.WriteLine("Hider.Foo");
        }
    }

    #endregion

    #region Testing1

    public class A
    {
        public readonly int Counter = 1;
    }

    public class B : A
    {
        public int Counter = 2; // B. Counter hides A.counter
    }

    private static void TestHideInheritedMembers()
    {
        A a = new B();
        Console.WriteLine(a.Counter);
    }

    private static void TestAbstractClass()
    {
    }


    private static void TestInheritance()
    {
        Asset asset = new Asset();
        Stock stock = new Stock();

        // Upcasting
        Asset a = stock;

        // Downcasting
        Stock s = (Stock)a;

        // Pattern variable 
        if (a is Stock s1)
        {
            Console.WriteLine(s1);
        }

        // Covariant return type
        House h1 = new House
        {
            Name = "House 1",
            Mortgage = 2000
        };

        House h2 = h1.Clone();

        var assets = new List<Asset>()
        {
            h1,
            h2
        };

        foreach (Asset a1 in assets)
        {
            Console.WriteLine(a1.Name);
        }
    }

    #endregion
}