namespace Day2;

#region Encapsulaion

class Student
{
    private string? _name;

    public string? Name
    {
        get => _name;
        set => _name = value;
    }
}

#endregion

#region Inheritance

public class Person
{
    public string Name { get; set; }
}

// public class Employee : Person
// {
//     public string Department { get; set; }
// }

#endregion

#region Polymorphism

// Static polymorphism
class Calculator
{
    public int Add(int a, int b) => a + b;
    public double Add(double a, double b) => a + b;
}

// Dynamic polymorphism
class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("Animal speaks");
    }
}

sealed class Bird : Animal
{
    public void Fly()
    {
        Console.WriteLine("Bird flying");
    }

    public override void Speak()
    {
        Console.WriteLine("Bird speak");
    }
}

#endregion

#region Abstraction

public abstract class Human
{
    public abstract void MakeSound();
}

public class Baby : Human
{
    public override void MakeSound()
    {
        Console.WriteLine("Baby noises");
    }
}

public class Kid : Human
{
    public override void MakeSound()
    {
        Console.WriteLine("Hello, I am a kid");
    }
}

#endregion

#region ClassConstructor

// Default constructor
class DefaultConstructor
{
    public DefaultConstructor()
    {
        Console.WriteLine("Default constructor called");
    }

    // Static constructor
    static DefaultConstructor()
    {
        Console.WriteLine("Static constructor called");
    }

    // Private Constructor
    private DefaultConstructor(string message)
    {
        Console.WriteLine(message);
    }
}

// ParameterizedConstructor
class ParameterizedConstructor
{
    private float _discountBase;

    public ParameterizedConstructor(float discountBase)
    {
        _discountBase = discountBase;
        Console.WriteLine($"Discount base is set to {discountBase}");
    }
}

#endregion

#region Solid

// Open Closed Principles
public interface IDiscount
{
    decimal ApplyDiscount(decimal price);
}

public class ChristmasDiscount : IDiscount
{
    public decimal ApplyDiscount(decimal price)
    {
        return price * 0.8M;
    }
}

public class EidDiscount : IDiscount
{
    public decimal ApplyDiscount(decimal price)
    {
        return price * 0.85M;
    }
}

public class PriceCalculator(IDiscount discount)
{
    private IDiscount _discount = discount;

    public void ChangeDiscount(IDiscount discount)
    {
        _discount = discount;
    }

    public decimal CalculatePrice(IDiscountable @object)
    {
        return _discount.ApplyDiscount(@object.GetPrice());
    }
}

// Interface Segregation Principle (ISP)
public interface IWorkable
{
    void Work();
}

public interface IRemoteable
{
    void RemoteWork();
}

public class Employee : IWorkable, IRemoteable
{
    public void RemoteWork()
    {
        Console.WriteLine("Doing remote work");
    }

    public void Work()
    {
        Console.WriteLine("Doing work at office");
    }
}

#endregion

public interface IDiscountable
{
    decimal GetPrice();
    void SetPrice(decimal price);
}

class Car(string name, decimal price = 0M) : IDiscountable
{
    private string _car = name;
    private decimal _price = price;

    public void SetPrice(decimal price) => _price = price;
    public decimal GetPrice() => _price;
}

static class Program
{
    static void Main(string[] args)
    {
        // SOLID
        // TestInterface();
        // TestISP();

        // OOP
        // TestInheritance();
        // TestStaticPolymorphism();
        // TestDynamicPolymorphism();
        // TestAbstraction();

        //Etc
        Test();

        // IDiscountable
        PriceCalculator priceCalculator = new PriceCalculator(new ChristmasDiscount());
        Car car = new Car("Innova", 200_000_000_000);

        decimal carPrice = priceCalculator.CalculatePrice(car);
        Console.WriteLine(carPrice);

    }

    static void Test()
    {
        // int a = 0;
        // int b = 1;
        // System.Console.WriteLine(System.Convert.ToBoolean(a ^ b));

        // Employee e1 = new Employee();
        // Employee e2 = e1;

        // System.Console.WriteLine($"{object.Equals(float.NaN, float.NaN)}");        
        // System.Console.WriteLine($"{e1 == e2} {object.ReferenceEquals(e1, e2)}");  
        // System.Console.WriteLine($"{MathF.Equals(0.1f, 0.1f)}");  

        // char a = 'a';
        // byte b = (byte) a;
        // System.Console.WriteLine(b);

        // Console.WriteLine($$"""{ "TimeStamp": "{{DateTime.Now}}" }""");

        // var utf8 = "ab->cd"u8;
        // char[] vowels = ['a', 'i', 'u', 'e', 'o'];
        // System.Console.WriteLine(Contains("aku", vowels));
        // System.Console.WriteLine(vowels[^1]);

        // Index first = 0;
        // Index last = ^1;
        // Index secondToLast = ^2;
        // System.Console.WriteLine($"{vowels[first]} {vowels[last]} {vowels[secondToLast]}");


        // char[] firstTwo = vowels[..2];
        // char[] lastThree = vowels[2..];
        // char[] middleOne = vowels[2..3];
        // System.Console.WriteLine($"{firstTwo} {lastThree} {middleOne}");

        // int[,] notMatrix = new int[3, 2];

        // int[][] jagged = new int[3][]
        // {
        //     [1, 2, 3],
        //     [4, 5],
        //     [6, 7, 8]
        // };

        // int x = 0;
        // decimal d = default;
        // // System.Console.WriteLine(x); // Compile time error
        // RefMethodPlusTwo(ref x);

        NumberOperations(1, 2, out int sum, out int sub, out float div, out float mul);

        int res = Sum(1, 2, 3, 4, 5, 6, 7);
        int res2 = OptionalSum(value: 2, addValue: 2);
        int res3 = OptionalSum(addValue: 3, value: 3);

        ref string xRef = ref GetXString();
        xRef = "New value";

        // 1 + Console.WriteLine("");

        int aa = 0;

        Console.WriteLine();

        int x = 1000;

        switch (x)
        {
            case int i when i > 500:
                Console.WriteLine($"Is integer greater than 500 with value of {i}");
                break;
        }

        int cardNumber = 13;
        string cardName = cardNumber switch
        {
            13 => "king",
            12 => "queen",
            11 => "jack",
            _ => "Pip card"
        };

        switch (cardNumber)
        {
            case 13:
                cardName = "King";
                break;
            case 12:
                cardName = "Queen";
                break;
        }
    }

    struct Point
    {
        float X;
        float Y;
    }

    static string x = "Old value";
    static ref string GetXString() => ref x;

    static void RefMethodPlusTwo(ref int value)
    {
        value += 2;
    }

    static int Sum(params int[] value)
    {
        int result = 0;
        foreach (int i in value)
        {
            result += i;
        }

        return result;
    }

    static int OptionalSum(int value, int addValue = 1)
    {
        return value + addValue;
    }

    static void NumberOperations(int value1, int value2, out int sum, out int sub, out float div, out float mul)
    {
        sum = value1 + value2;
        sub = value1 - value2;
        div = (float)value1 / value2;
        mul = (float)value1 * value2;
    }

    static bool Contains(string text, char[] comparer)
    {
        bool contain = false;

        foreach (char c in text.ToLower())
        {
            foreach (char v in comparer)
            {
                if (c == v)
                    contain = true;
            }
        }

        return contain;
    }


    static void TestInterface()
    {
        ChristmasDiscount cd = new ChristmasDiscount();
        EidDiscount ed = new EidDiscount();
        PriceCalculator calculator = new PriceCalculator(cd);

        // decimal basePrice = 5000M;

        // decimal christmasDiscountPrice = calculator.CalculatePrice(basePrice);
        // Console.WriteLine($"Christmas discounted price: {christmasDiscountPrice}");

        // calculator.ChangeDiscount(ed);

        // decimal eidDiscountPrice = calculator.CalculatePrice(basePrice);
        // Console.WriteLine($"Eid discounted price: {eidDiscountPrice}");
    }

    static void DemonstrateTestIsp()
    {
        Employee e = new Employee();
        e.Work();
        e.RemoteWork();
    }

    static void TestInheritance()
    {
        // Employee e = new Employee();
        // e.Name = "John smith";
        // e.Department = "Mechanical";
    }

    private static void TestStaticPolymorphism()
    {
        Calculator calc = new Calculator();

        //Add int
        int a = 10, b = 20;
        int resultInt = calc.Add(a, b);
        Console.WriteLine($"Int adder {resultInt}");

        //Add double
        double d1 = 20.4, d2 = 30.3;
        double resultDouble = calc.Add(d1, d2);
        Console.WriteLine(resultDouble);
    }

    static void TestDynamicPolymorphism()
    {
        Bird b = new Bird();
        b.Speak();
    }

    static void TestAbstraction()
    {
        Baby b = new Baby();
        Kid k = new Kid();

        b.MakeSound();
        k.MakeSound();
    }
}