using System.Collections;

namespace Day5;

class Test
{
    public static int Cube(int x) => x * x * x;
}

class Util
{
    public delegate TResult Transformer<TArg, TResult>(TArg x);

    public static void Transform<T>(T[] values, Transformer<T, T> t)
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = t(values[i]);
        }
    }

    public static void FuncTransform<T>(T[] values, Func<T, T> t)
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = t(values[i]);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
    }

    #region CovarianceContravariance

    public static void TestVariance()
    {
        /*
         * Covariance (out) (sub -> base)
         */
        List<Dog> dogs = new List<Dog>()
        {
            new()
            {
                Name = "Buddy"
            }
        };

        // Can do
        IEnumerable<Animal> animalList = dogs;

        // Cannot do as List is invariance
        // List<Animal> animals = dogs;

        foreach (Animal animal in animalList)
        {
            Console.WriteLine(animal.Name);
        }


        /*
         * Contravariance (in) (base -> sub)
         */
        // Handle all animal to make sound
        Action<Animal> printAnimalInfo = animal => animal.MakeSound();

        // Contravariance because Action<Animal> -> Action<Dog>
        // Safe because Dog IS an animal
        Action<Dog> printDogInfo = printAnimalInfo;

        printDogInfo(new Dog());

        // Invariance (in, out) (RW)
        List<Dog> dogList = new List<Dog>();

        /*
         * Error:
         * Cannot convert source type 'System.Collections.Generic.List<Day5.Program.Dog>' to target type 'System.Collections.Generic.IList<Day5.Program.Animal>'
         */
        // IList<Animal> animalList2 = dogList;

        // Ok, because data type is the same
        IList<Dog> dogList2 = dogs;

        /*
         * Covariance array crash in runtime
         */
        Dog[] dogs3 = new Dog[2];

        // Covariance
        Animal[] animals2 = dogs3;

        /*
         * Compiler : no error because Cat is subclass of Animal
         * Runtime : Error because trying to assign Cat to a Dog data
         */
        try
        {
            /*
             * Runtime error message
             * Attempted to access an element as a type incompatible with the array.
             * --- EXCEPTION #1/1 [ArrayTypeMismatchException]
             */
            animals2[0] = new Cat();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        /*
         * Boxing to object error
         */
        List<Dog> dogs4 = new List<Dog>();

        // Force casting to object then to List<Animal> 
        object rawList = dogs4;
        List<Animal>
            animals3 =
                (List<Animal>)rawList; // Error: JetBrains Launcher could not run. Unable to cast object of type 'System.Collections.Generic.List`1[Day5.Program+Dog]' to type 'System.Collections.Generic.List`1[Day5.Program+Animal]'.
    }

    public interface IReadonlyContainer<out T>
    {
        T GetItem();

        //Invalid variance: covariant type parameter 'T' is used in contravariant position. Parameter must be input-safe
        // void AddItem(T item);
    }

    class Animal
    {
        public string Name { get; set; }

        public void MakeSound() => Console.WriteLine("Animal sound");
    }

    class Dog : Animal
    {
    }

    class Cat : Animal
    {
    }

    #endregion

    #region Delegate

    public static void TestDelegate()
    {
        // Delegate basic
        Transformer t = Square;
        int result = t(3);
        int result2 = t.Invoke(4);

        Console.WriteLine($"{result} {result2}");

        int[] values = { 1, 2, 3, 4, 5 };
        Transform(values, Square);

        foreach (var v in values)
        {
            Console.Write($"{v} ");
        }

        Console.WriteLine();

        // Static delegate
        int[] values2 = { 1, 2, 3, 4, 5 };
        Transformer t2 = Test.Cube;

        Transform(values2, t2);
        foreach (var v in values2)
        {
            Console.Write($"{v} ");
        }

        Console.WriteLine();


        // Multicast delegate
        Notifier notifier = SendToConsole;
        notifier += SendToLog;
        notifier += SendSmsToAdmin;

        notifier.Invoke("Warning message, temperature drop");

        // Generic delegate
        Util.Transformer<int, int> tGeneric = Square;
        int[] values3 = { 1, 2, 3, 4 };

        Util.Transform(values3, tGeneric);
        foreach (var v in values3)
        {
            Console.Write($"{v} ");
        }

        Console.WriteLine();

        // Func 
        Func<int, int> f = Square;
        int[] values4 = { 1, 2, 3, 4 };

        Util.FuncTransform(values4, f);
        foreach (var v in values3)
        {
            Console.Write($"{v} ");
        }

        Console.WriteLine();

        // Func vs Action
        Func<int, int> addTwo = (int a) => a + 2;
        Func<string, bool> isLongString = (str) => str.Length > 5;
        Action sayHello = () => Console.WriteLine("Hello");

        Console.WriteLine(addTwo(4));
        Console.WriteLine(isLongString("Is this considered long?"));
        sayHello();

        // Variance in delegate
        StringAction s1 = ActOnObject;
        StringAction2 s2 = new StringAction2(s1);

        s1("Hello contravariance");
        s2("Hello contravariance from s2");

        // Covariance
        ObjectRetriever objectRetriever = RetrieveString;
        Console.WriteLine(objectRetriever);
    }

    // Contravariance on delegate
    static void ActOnObject(object o) => Console.WriteLine(o);

    delegate void StringAction(string s);

    delegate void StringAction2(string s);

    // Covariance in delegate
    static string RetrieveString() => "Retrieving string";

    delegate object ObjectRetriever();


    static void Transform(int[] values, Transformer t)
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = t.Invoke(values[i]);
        }
    }

    public delegate void Notifier(string message);

    static void SendToConsole(string msg)
    {
        Console.WriteLine($"[CONSOLE] Sending message: {msg}");
    }

    static void SendToLog(string msg)
    {
        Console.WriteLine($"[LOG] Sending message: {msg}");
    }

    static void SendSmsToAdmin(string msg)
    {
        Console.WriteLine($"[SMS Admin] Sending message: {msg}");
    }


    delegate int Transformer(int i);

    static int Square(int x) => x * x;

    #endregion


    #region Event Handler

    public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);

    public class Broadcaster
    {
        public event PriceChangedHandler PriceChanged
        {
            add => PriceChanged += value;
            remove => PriceChanged -= value;
        }
    }

    public class Stock
    {
        private string _symbol;
        private decimal _price;

        public Stock(string symbol) => this._symbol = symbol;

        // public event PriceChangedHandler PriceChanged;
        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        public decimal Price
        {
            get => _price;
            set
            {
                if (value == _price) return;

                decimal oldPrice = _price;
                _price = value;

                OnPriceChanged(new(oldPrice, _price));
            }
        }

        protected virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            PriceChanged?.Invoke(this, e);
        }
    }

    public class PriceChangedEventArgs : EventArgs
    {
        public readonly decimal LastPrice;
        public readonly decimal NewPrice;

        public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
        {
            LastPrice = lastPrice;
            NewPrice = newPrice;
        }
    }

    public static void TestEventHandler()
    {
        Stock stock = new Stock("BUMI");
        stock.Price = 2700M;

        stock.PriceChanged += (sender, args) =>
        {
            Console.WriteLine($"[{sender.GetType()}]Price changed: {args.LastPrice} -> {args.NewPrice} ");
        };

        stock.Price = 2800M;
    }

    #endregion

    #region Try Catch Statements

    public static void TestTryCatch()
    {
        Func<int, int> calc = (num) => 10 / num;

        try
        {
            int y = calc(0);
            Console.WriteLine(y);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Error: x cannot be zero");
        }

        Console.WriteLine("Program completed");

        StreamReader reader = null;
        try
        {
            reader = File.OpenText("file.txt");

            if (reader.EndOfStream) return;
            Console.WriteLine(reader.ReadToEnd());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message} {ex.StackTrace}");
        }
        finally
        {
            if (reader != null)
            {
                reader.Dispose();
            }
        }

        Func<int, int> addTwo = (num) => num + 2;
        Func<int, int> addThree = (num) => num + 3;
        Func<int, int> addFour = (num) => num + 4;

        Add add = AddTwo;
        add += AddThree;
        add += AddFour;

        Console.WriteLine(add(2));
    }

    static int AddTwo(int x) => x + 2;
    static int AddThree(int x) => x + 3;
    static int AddFour(int x) => x + 4;

    private delegate int Add(int x);

    #endregion

    #region Enumerator and Iterators

    public static void TestIterator()
    {
        // MyEnumerator<int> enums = new([1, 2, 3, 4]);

        foreach (char c in "beer")
        {
            Console.Write($"{c} ");
        }

        Console.WriteLine();

        // Explicit of foreach
        using (var enumerator = "beer".GetEnumerator())
        {
            while (enumerator.MoveNext())
            {
                var element = enumerator.Current;
                Console.Write($"{element} ");
            }
        }

        using var enumerator2 = "enum".GetEnumerator();
        while (enumerator2.MoveNext())
        {
            var element = enumerator2.Current;
            Console.Write($"{element} ");
        }

        Console.WriteLine();

        /*
         * Iterators
         */
        User?[] users = new[]
        {
            new User
            {
                Name = "John Doe",
                Email = "johndoe@gmail.com",
            },
            new User
            {
                Name = "Steven Johnson",
                Email = "stevenjohnson@gmail.com",
            },
            new User
            {
                Name = "Emily Walker",
                Email = "emily@microsoft.com",
            },
            null,
            null
        };

        // Send to log
        foreach (var user in SendEmail(users)!)
        {
            if (user != null)
            {
                Console.WriteLine($"[EMAIL Service]: Successfully send email to {user.Email}");
            }
            else
            {
                Console.WriteLine($"[EMAIL Service]: Failed to send email");
            }
        }

        foreach (var e in EvenNumbersOnly(Fibs(10)))
        {
            Console.Write($"{e} ");
        }
    }

    static IEnumerable<int> Fibs(int fibCount)
    {
        for (int i = 0, prevFib = 1, curFib = 1; i < fibCount; i++)
        {
            yield return prevFib;
            int newFib = prevFib + curFib;
            prevFib = curFib;
            curFib = newFib;
        }
    }

    static IEnumerable<int> EvenNumbersOnly(IEnumerable<int> sequence)
    {
        foreach (var x in sequence)
        {
            if ((x % 2) == 0)
            {
                yield return x;
            }
        }
    }

    static IEnumerable<User?> SendEmail(User?[] users)
    {
        foreach (var user in users)
        {
            if (user != null)
            {
                Console.WriteLine($"Sending email to name: {user.Name} email: {user.Email}");
            }

            yield return user;
        }
    }

    class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }


    class MyEnumerator<T> : IEnumerator<T>
    {
        private object[]? _datas = null;
        private int _index;

        public MyEnumerator(T[]? datas)
        {
            _datas = datas as object[];
        }

        public T Current
        {
            get => (T)_datas[_index];
        }

        public bool MoveNext()
        {
            _index++;
            if (_datas[_index] == null)
            {
                return false;
            }

            return true;
        }

        public void Reset()
        {
        }

        object? IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }

    #endregion
}