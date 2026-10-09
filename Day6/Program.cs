using System.Text;

namespace Day6;

class Program
{
    static void Main(string[] args)
    {
    }

    #region Nullable Value Types

    public static void TestNullableValueTypes()
    {
        int? i = null;
        Nullable<int> i2 = new Nullable<int>();

        Console.WriteLine(i.GetValueOrDefault());

        object o = 1;
        int? x = o as int?;
        Console.WriteLine(x.HasValue);

        int? i3 = 5;
        int? i4 = 10;
        bool b = i3 < i4;
        Console.WriteLine(b);

        // Console.WriteLine((int?)null == (double?)null);
        // Console.WriteLine(i3 == i4);
        // Console.WriteLine(i3 == null);
        // Console.WriteLine(i4 == null);
        // Console.WriteLine(null != 5);

        // Console.WriteLine(null + 5);

        Console.WriteLine(null & false);

        int? x1 = null;
        int? y1 = x ?? (int?)5;

        Console.WriteLine(y1);

        StringBuilder sb = null;
        int? length = sb?.Length ?? 0;

        Console.WriteLine($"Length {length}");

        int? i5 = "Pink".IndexOf("b");
        Console.WriteLine(i5);
    }

    #endregion

    #region Operator Overloading

    class Note
    {
        private int _value;

        public Note(int semitonesFromA)
        {
            _value = semitonesFromA;
        }

        public static Note operator +(Note x, int semitones)
        {
            checked
            {
                return new Note(x._value + semitones);
            }
        }

        public static implicit operator double(Note x) => 440 * Math.Pow(2, (double)x._value / 12);

        public static explicit operator Note(double x) => new Note((int)(0.5 + 12 * (Math.Log(x / 440) / Math.Log(2))));
    }

    public class Person
    {
        public string Name { get; set; }
    }

    public class People
    {
        private List<Person> _persons = new List<Person>();

        public People()
        {
        }

        public People(List<Person> persons)
        {
            _persons = persons;
        }

        public static People operator +(People people, Person person)
        {
            people._persons.Add(person);
            return new People(people._persons);
        }

        public static bool operator ==(People people1, People people2)
        {
            foreach (var p1 in people1._persons)
            {
                if (!people2._persons.Contains(p1))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(People people1, People people2)
        {
            foreach (var p1 in people1._persons)
            {
                if (people2._persons.Contains(p1))
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            string msg = "Person List: ";
            foreach (var person in _persons)
            {
                msg += person.Name += " ";
            }

            return msg;
        }
    }

    public static void TestOperatorOverloading()
    {
        Note B = new Note(2);
        Note CSharp = B + 2;

        Console.WriteLine((Note)CSharp);

        Note n = (Note)554.38;
        double x = n;

        Console.WriteLine(x);

        Person john = new()
        {
            Name = "John"
        };

        Person laurent = new()
        {
            Name = "Laurent"
        };

        Person windy = new()
        {
            Name = "Windy"
        };

        People people = new People();
        people += john;
        people += laurent;
        people += windy;

        People people2 = new People();
        people2 += john;
        people2 += laurent;
        people2 += windy;

        Console.WriteLine(people == people2);

        Console.WriteLine(people.GetHashCode());
    }

    #endregion

    #region Delegate Variance

    
    
    // Covariance
    public static string GetGreeting(string name) => "Hello " + name;

    public delegate object GetGreetingDelegate(string name);
    
    // Contravariance

    public static void ProcessAnimal(Animal animal)
    {
        Console.WriteLine($"Processing animal....");    
    }
    
    public delegate void GetDogDelegate(Dog dog);

    static void ProcessAnimal2(Animal animal)
    {
        Console.WriteLine($"Processing animal....");
    }

    public static void TestDelegateVariance()
    {
        GetGreetingDelegate greet = GetGreeting;
        Console.WriteLine(greet("John"));

        GetDogDelegate getDog = ProcessAnimal;
        getDog(new Dog
        {
            Name = "Dog"
        });
        
        

    }

    #endregion

    #region Event Handlers

    public class NotificationEventArgs
    {
        public enum MessageType
        {
            Information,
            Warning,
            Error
        }
        
        public string Message { get; set; }
        public MessageType Type { get; set; }
    }

    public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);
    
    public class Broadcaster
    {
        private decimal _price;
        public event PriceChangedHandler PriceChanged;

        public decimal Price
        {
            get => _price;
            set
            {
                if(value == _price) return;
                
                decimal oldPrice = _price;
                _price = value;
                
                PriceChanged?.Invoke(oldPrice, _price);
            }
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

    public class Stock
    {
        public readonly string Symbol;
        private decimal _price;

        public Stock(string symbol)
        {
            this.Symbol = symbol;
        }
        
        public event EventHandler<PriceChangedEventArgs>? PriceChanged;

        protected virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            PriceChanged?.Invoke(this, e);
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if(value == _price) return;
                
                decimal oldPrice = _price;
                _price = value;
                
                OnPriceChanged(new PriceChangedEventArgs(oldPrice, value));
            }
        }
    }


    public static void TestEvent()
    {
        // Broadcaster broadcaster = new Broadcaster();
        // broadcaster.PriceChanged += (oldPrice, newPrice) =>
        // {
        //     Console.WriteLine($"[A] {oldPrice} -> {newPrice}");
        // };
        //
        // broadcaster.Price = 1000;
        // broadcaster.Price = 2000;
        
        Stock stock = new Stock("AAPL");
        stock.PriceChanged += (_, e) =>
        {
            Console.WriteLine($"[Broker A] {stock.Symbol}: {e.LastPrice} -> {e.NewPrice}");
        };
        
        stock.PriceChanged += (_, e) =>
        {
            Console.WriteLine($"[Broker B] {stock.Symbol}: {e.LastPrice} to {e.NewPrice}");
        };

        
        stock.Price = 1000M;
        stock.Price = 1002M;
        
    }

    #endregion

    public static void Test()
    {
        string beer = "beer";
        using (var enumerator = beer.GetEnumerator())
        {
            
        }
        
        List<int> ints = new List<int>(){1, 2, 3};
        List<int> ints2 = [1, 2, 3];
     
        // Lazy evaluation in LINQ
        int[] numbers = [1, 2, 3, 4, 5, 6, 7];

        var filtered = numbers
            .Where(num =>
            {
                Console.WriteLine($"[Filtering]: {num}");
                return num % 2 == 0;
            })
            .Select(num => num * 2); // Here the data still not processed because lazy evaluation

        using (var enumerator = filtered.GetEnumerator())
        {
            while (enumerator.MoveNext())
            {
                var num = enumerator.Current;
                Console.WriteLine(num);
            }
        }
        
        // Eager evaluation
        var filtered2 = numbers
            .Where(num =>
            {
                Console.WriteLine($"[Filtering]: {num}");
                return num % 2 == 0;
            })
            .Select(num => num * 2);
        
        var result = filtered2.ToList(); // Force the lazy evaluation of filtered data to immediate 

        try
        {
            Func<int, int, int> calc = (int a, int b) =>
            {
                return a / b;
            };
            
            // int a = calc(2, 0);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        object b = 0;

        if (b is int c)
        {
            Console.WriteLine(c);
        }

        // Null conditional
        Command command = null;
        command?.Process();
        
        // Null coalescing assignment
        command ??= new Command();
        
        // Null coalescing
        Command command2 = null;
        command2 = command ?? new Command();

        string a = null;
    }

    class Command
    {
        public void Process()
        {
            
        }
    }
    
    public class Animal
    {
        public string Name { get; set; }
    }

    public class Dog : Animal
    {
        
    }

    public class Animals
    {
        private List<Animal> _animals = new List<Animal>();

        public Animals()
        {
            
        }
        
        public Animals(List<Animal> animals)
        {
            this._animals = animals;
        }
        
        public static Animals operator +(Animals animals, Animal animal)
        {
            animals._animals.Add(animal);
            return new Animals(animals._animals);
        }
    }
    

    public static void Overloading()
    {
        Dog dog = new Dog
        {
            Name = "Dog"
        };
        Dog dog2 = new Dog()
        {
            Name = "Dog 2"
        };

        Animals animals = new Animals();
        animals += dog;
        animals += dog2;

        int? x = 5;
        int? y = null;
        int? z = x + y;

        Console.WriteLine(z);

    }
}