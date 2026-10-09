using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace Day7;

class Program
{
    static void Main(string[] args)
    {
        // Test();    
        // Test2();    
        // Test3();    
        Test4();
    }


    public static void Test()
    {
        char c = 'A';
        char newLine = '\n';

        // Console.WriteLine(char.ToUpperInvariant('c'));
        // Console.WriteLine(char.IsWhiteSpace('\t'));
        // Console.WriteLine(char.IsPunctuation('a'));

        // Console.WriteLine(new string('*', 10));

        char[] ca = "Hello from bootcamp".ToCharArray();
        string s = new string(ca);

        string? s2 = null;

        // Console.WriteLine(string.IsNullOrEmpty(s2));

        Console.WriteLine(s.StartsWith("H"));
        Console.WriteLine(s.EndsWith("o"));
        Console.WriteLine(s.IndexOf("from"));
        Console.WriteLine(s.LastIndexOf("from"));
        Console.WriteLine(s.IndexOfAny("o".ToCharArray()));

        Console.WriteLine(s.Substring(6, 4));
        Console.WriteLine(s.Insert(0, "Inserted"));
        // Console.WriteLine(s.Remove(0, 10));
        Console.WriteLine(s.PadLeft(4, ' '));
        Console.WriteLine(s.PadRight(4, ' '));
        Console.WriteLine(s.Replace("bootcamp", "world"));

        string[] splitted = s.Split(" ");
        string join = string.Join(' ', splitted);
        string concat = string.Concat(join, " Concatenate");

        string[] userNames = new[]
        {
            "John",
            "Jane"
        };

        foreach (var name in userNames)
        {
            string composite = "Good morning, {0, -10} it is {1} degrees now, your stock drop by {2:P}";
            string greet = string.Format(composite, name, 20, 0.05);

            Console.WriteLine(greet);
        }

        string s3 = "banana";
        string s4 = "banana";

        Console.WriteLine(s3.CompareTo(s4));

        int order = string.Compare("Apple", "Apple", StringComparison.InvariantCulture); // Returns a negative number

        int order2 = string.CompareOrdinal(s3, s4);

        Console.WriteLine(order);
        Console.WriteLine(order2);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 50; i++)
        {
            sb.Append(i).Append(',');
        }

        sb.AppendLine();
        sb.AppendLine("Line Append");
        sb.AppendFormat("Hello {0}", userNames[1]);

        Console.WriteLine(sb.ToString());
        Console.WriteLine(sb.Length);
    }

    public static void Test2()
    {
        TimeSpan tSpan = new TimeSpan(20, 0, 0, 0);
        Console.WriteLine(new TimeSpan(2, 30, 0));
        Console.WriteLine(TimeSpan.FromHours(2.5));
        Console.WriteLine(TimeSpan.FromHours(-2.5));

        Console.WriteLine(TimeSpan.FromHours(2) + TimeSpan.FromMinutes(30));
        Console.WriteLine(TimeSpan.FromDays(10) - TimeSpan.FromSeconds(1));

        TimeSpan nearlyTenDays = TimeSpan.FromDays(10) - TimeSpan.FromSeconds(1);

        Console.WriteLine(nearlyTenDays.Days);

        Console.WriteLine(TimeSpan.Parse("9.11:00:00"));


        DateTime dt = DateTime.Parse("2026/10/09 11:35:00");
        DateTime dt2 = DateTime.Parse("2026/10/09 11:35:00");

        DateTimeOffset dto = DateTimeOffset.Parse("2026/10/09 11:35:00 +02:00") + TimeSpan.FromHours(2);
        DateTimeOffset dto2 = DateTimeOffset.Parse("2026/10/09 11:35:00 +07:00");

        Console.WriteLine(dto);
        Console.WriteLine(dto.Offset);

        Console.WriteLine(dto2);
        Console.WriteLine(dto2.Offset);

        Console.WriteLine(dt == dt2);
        Console.WriteLine(dto == dto2);
        Console.WriteLine(dt.ToString("HH:mm:ss dd/MM/yy"));

        TimeOnly timeOnly = new TimeOnly(20, 4, 5);
        Console.WriteLine(timeOnly);
    }

    static void Test3()
    {
        string s = true.ToString();
        bool b = bool.Parse(s);

        Console.WriteLine(b);

        double pi = double.Parse("3,14");
        double pi2 = double.Parse("3.14", CultureInfo.InvariantCulture);
        Console.WriteLine(double.Parse("1.234"));
        Console.WriteLine(double.Parse("1,234"));

        Console.WriteLine(pi);
        Console.WriteLine(pi2);

        NumberFormatInfo f = new NumberFormatInfo();
        f.CurrencySymbol = "Rp. ";
        Console.WriteLine(3000.ToString("C", f));

        CultureInfo c = new CultureInfo("id-ID");
        Console.WriteLine(3.ToString("C", c));

        NumberFormatInfo customFormat = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
        customFormat.NumberGroupSeparator = ".";

        Console.WriteLine(1000.ToString("N3", customFormat));

        IFormatProvider wordy = new WordyFormat();
        Console.WriteLine(string.Format(wordy, "{0}", 123));
    }

    class WordyFormat : IFormatProvider, ICustomFormatter
    {
        public object? GetFormat(Type? formatType)
        {
            return "bbb";
        }

        public string Format(string? format, object? arg, IFormatProvider? formatProvider)
        {
            return "aaa";
        }
    }

    static void Test4()
    {
        int thirty = Convert.ToInt32("1E", 16);
        Console.WriteLine(thirty);

        uint five = Convert.ToUInt32("101", 2);
        Console.WriteLine(five);

        Random rng = new Random();
        int v = rng.Next(10);
        Console.WriteLine(v);

        int[] arr = [1, 2, 3, 4, 5];

        Console.WriteLine(rng.GetItems(arr, 1).First());
        Console.WriteLine(BitOperations.TrailingZeroCount(04));

        Func<Enum, object> f = (Enum value) =>
        {
            Type integralType = Enum.GetUnderlyingType(value.GetType());
            return Convert.ChangeType(value, integralType);
        };

        Func<Enum, int> f2 = (Enum anyEnum) => Convert.ToInt16(anyEnum.ToString("D"));

        object result = f(Order.Beverage);
        Console.WriteLine(result);
        Console.WriteLine(result.GetType());

        Console.WriteLine(f2(Order.Beverage));

        object bs = Enum.ToObject(typeof(Order), 2);
        Enum orderType = (Order)bs;
        Console.WriteLine(orderType);

        Enum orderType2 = Order.Food | Order.Beverage;
        Console.WriteLine(Enum.Format(typeof(Order), orderType2, "G"));
        Console.WriteLine(orderType2.ToString("G"));
        Console.WriteLine();

        foreach (var value in Enum.GetValues(typeof(Order)))
        {
            Console.WriteLine(value);
        }
        
        Area a1 = new Area(20, 10);
        Area a2 = new Area(10, 20);

        Console.WriteLine(a1.Equals(a2));
    }

    [Flags]
    enum Order
    {
        Food = 1 << 0,
        Beverage = 1 << 1,
    }

    struct Area : IEquatable<Area>
    {
        public readonly int Measure1;
        public readonly int Measure2;

        public Area(int m1, int m2)
        {
            Measure1 = Math.Min(m1, m2);
            Measure2 = Math.Max(m1, m2);
        }

        public bool Equals(Area other)
            => other.Measure1 == Measure1 && other.Measure2 == Measure2;

        public override bool Equals(object? other) => other is Area a && Equals(a);

        public override int GetHashCode() => HashCode.Combine(Measure1, Measure2);

        public static bool operator ==(Area a1, Area a2) => Equals(a1, a2);

        public static bool operator !=(Area a1, Area a2) => !(a1 == a2);

    }

    public static void Test5()
    {
        // ProcessStartInfo psi = new ProcessStartInfo()
        // {
        //     FileName = "cmd.exe",
        //     Arguments = "/c ipconfig /all",
        //     RedirectStandardOutput = true,
        //     UseShellExecute = false
        // };
        //
        // Process p = Process.Start(psi)!;
        // string result = p.StandardOutput.ReadToEnd();
        // Console.WriteLine(result);
        //

        NumberFormatInfo format = new NumberFormatInfo();
        format.PositiveSign = "-";
        Console.WriteLine(3.ToString("", format));
        

    }
}