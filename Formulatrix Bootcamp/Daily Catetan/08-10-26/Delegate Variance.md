Variance adalah fitur yang memberikan fleksibilitas pada delegate saat mencocokkan **tipe kembalian (return type)** dan **parameter fungsi** dengan bentuk signature delegate.

Fitur ini memungkinkan penetapan method ke delegate meskipun tipe data parameter atau return type method tersebut **tidak persis sama**, **selama memenuhi hubungan turunan/warisan (inheritance)**.

# Aturan Dasar
- **Covariance**: Berlaku untuk **Return Type**, hal ini memungkinkan method **mengembalikan** tipe yang **lebih spesifik (child class)** daripada yang didefinisikan di delegate.
- **Contravariance**: Berlaku untuk **Parameter**, hal ini memungkinkan method menerima parameter tipe yang **lebih umum (parent class)** daripada yang didefinisikan di delegate.
## Contoh Kelas Dasar (Inheritance)
```cs
public class Animal { }
public class Dog : Animal { } // Dog adalah turunan dari Animal
```
# Covariance (Tipe Kembalian)
Covariance memperbolehkan **method yang diikat ke delegate mengembalikan class turunan** (child class). Jika sebuah delegate mengharapkan objek `Animal`, memberikan objek `Dog` tidak akan menimbulkan error karena setiap `Dog` adalah pasti sebuah `Animal`.

**Contoh Kode Covariance**
```cs
// Delegate mengharapkan return type 'Animal' (Parent)
public delegate Animal AnimalFactory();

public class Program
{
    // Method ini mengembalikan 'Dog' (Child)
    public static Dog GetDog()
    {
        return new Dog();
    }

    public static void Main()
    {
        // COVARIANCE: GetDog mengembalikan Dog, tetapi bisa dimasukkan ke delegate AnimalFactory
        AnimalFactory factory = GetDog;

        Animal myAnimal = factory(); // Mengembalikan Dog, yang dapat diperlakukan sebagai Animal
    }
}
```
> `Animal` lebih umum daripada `Dog`, namun karena hubungan covariance, delegate mampu mengembalikan nilai yang lebih **spesifik** ke nilai yang lebih **umum** ketika direferensikan ke method `GetDog()`

# Contravariance (Parameter)
Contravariance memperbolehkan **method yang diikat ke delegate menerima parameter dengan tipe kelas induk (*parent class*).** Jika delegate memiliki return type objek `Dog` saat dipanggil, method yang menerima `Animal` pasti bisa memprosesnya karena objek `Dog` yang dikirim tetaplah sebuah `Animal`.

**Contoh Kode Contravariance**
```cs
// Delegate mengharapkan method yang menerima parameter 'Dog' (Child)
public delegate void DogHandler(Dog dog);

public class Program
{
    // Method ini menerima parameter 'Animal' (Parent)
    public static void ProcessAnimal(Animal animal)
    {
        Console.WriteLine("Memproses animal...");
    }

    public static void Main()
    {
        // CONTRAVARIANCE: ProcessAnimal menerima Animal, tetapi diikat ke delegate DogHandler
        DogHandler handler = ProcessAnimal;

        // Saat dipanggil, dipasangkan objek Dog
        handler(new Dog()); // Aman! ProcessAnimal sanggup menerima objek Dog karena Dog adalah Animal
    }
}
```

# Variance pada Generics Delegate (`out` & `in`)
Mulai C# 4.0, ketersediaan covariance dan contravariance pada *generic delegate* menggunakan kata kunci `out` dan `in`:
- `out` **(Covariance)**: Digunakan untuk tipe kembalian (output)
- `in` **(Contravariance)**: Digunakan untuk parameter input.

**Contoh dengan Generics Delegate**
```cs
// 'out T' berarti T bersifat Covariant (Return Type)
public delegate T ResultSupplier<out T>();

// 'in T' berarti T bersifat Contravariant (Parameter)
public delegate void DataConsumer<in T>(T data);
```
Delegasi bawaan C# seperti `Func<TResult>` dan `Action<T>` sudah memanfaatkan fitur ini secara otomatis:
- `Func<out TResult>` mendukung **covariance**.
- `Action<in T>` mendukung **contravariance**.
```cs
// Contoh bawaan C#
Func<Dog> dogFactory = () => new Dog();
Func<Animal> animalFactory = dogFactory; // Covariance (Func<out TResult>)

Action<Animal> animalAction = (a) => Console.WriteLine(a);
Action<Dog> dogAction = animalAction;     // Contravariance (Action<in T>)
```
