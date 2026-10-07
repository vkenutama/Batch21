Variance (covariance, contravariance, dan invariance) adalah konsep dalam sistem tipe dalam *OOP* dan `generics` yang menentukan subtyping dari tipe kompleks seperti `List<T>` atau `Function<T>` berhubungan dengan subtyping dari tipe dasarnya.

![[variance.png|center]]

## Konsep Dasar dan Jenisnya
Misalkan `Dog` adalah turunan (*subclass*) dari `Animal`.

### Covariance (Searah)
#### Relasi
> `List<Dog>` dianggap sebagai turunan dari `List<Animal>`
#### Sifat
> Mempertahankan hierarki tipe asal
#### Prinsip
> Hanya untuk **Producer / Read-only** (mengeluarkan/mengembalikan data). Kita bisa membaca `Dog` sebagai `Animal`, tapi tidak aman untuk menambah data baru ke dalamnya.
#### Sintaks
```cs
IEnumerable<out Animal>
```
Covariance memungkinkan penggunaan tipe data yang lebih spesifik (*derived type*) daripada yang ditentukan secara generik. Keyword `out` menandakan tipe parameter tersebut **hanya dikeluarkan/dikembalikan** (*return type*) dan tidak pernah diterima sebagai argumen input.

**Contoh**: `IEnumerable<out T>` di C# bersifat covariant.

```cs
public class Animal 
{ 
    public string Name { get; set; } 
}

public class Dog : Animal { }

class Program
{
    static void Main()
    {
        // List<Dog> mengimplementasikan IEnumerable<Dog>
        List<Dog> dogList = new List<Dog> { new Dog { Name = "Buddy" } };

        // Covariance: IEnumerable<Dog> bisa ditugaskan ke IEnumerable<Animal>
        IEnumerable<Animal> animalList = dogList;

        foreach (Animal animal in animalList)
        {
            Console.WriteLine(animal.Name); // Aman karena kita hanya MEMBACA data
        }
    }
}
```

### Contravariance (Berlawanan arah)
#### Relasi
> `Consumer<Animal>` dianggap sebagai turunan dari `Consumer<Dog>`
#### Sifat
> Membalikkan hierarki tipe asal
#### Prinsip
>Hanya untuk **Consumer/Write-only** (menerima/memproses data). Pemproses `Animal` pasti sanggup memproses `Dog`
#### Sintaks
```cs
IComparer<in Dog>
```
Contravariance memungkinkan tipe yang lebih umum (*base type*) daripada yang ditentukan secara generik. Kata kunci `in` menandakan bahwa tipe parameter tersebut **hanya diterima sebagai input** (*method parameter*) dan tidak pernah dikembalikan.

**Contoh**: `IComparer<in T>` dan `Action<in T>` di C# bersifat contravariant.

```cs
public class Animal 
{ 
    public void MakeSound() => Console.WriteLine("Suara hewan"); 
}

public class Dog : Animal { }

class Program
{
    static void Main()
    {
        // Pemroses yang bisa menangani semua Animal
        Action<Animal> printAnimalInfo = animal => animal.MakeSound();

        // Contravariance: Action<Animal> ditugaskan ke Action<Dog>
        // Sangat aman karena Dog PASTI adalah Animal
        Action<Dog> printDogInfo = printAnimalInfo;

        printDogInfo(new Dog()); // Output: Suara hewan
    }
}
```

### Invariance (Kaku / Tidak ada relasi)
#### Relasi
>`List<Dog>` bukan turunan dari `List<Animal>`, dan sebaliknya.
#### Sifat
>Tidak memperbolehkan fleksibilitas subtyping sama sekali.
#### Prinsip 
>Digunakan saat tipe data perlu **Read & Write** (bisa dibaca sekaligus diubah).
#### Sintaks
>Secara default pada collection umum seperti 
```cs
List<T>
```
Invariance terjadi jika tipe parameter tidak memakai kata kunci `out` maupun `in`. Tipe data yang digunakan **HARUS sama persis**, tidak bisa lebih umum atau lebih spesifik.

**Contoh**: `IList<T>` di C# bersifat invariant karena memiliki method untuk membaca (`get`) dan menulis (`add`).

```cs
class Program
{
    static void Main()
    {
        List<Dog> dogList = new List<Dog>();

        // ERROR KOMPILASI! List<Dog> TIDAK BISA ditugaskan ke IList<Animal>
        // IList<Animal> animalList = dogList; 

        // Alasan kenapa dilarang (Type Safety):
        // Jika diizinkan, kita bisa melakukan ini:
        // animalList.Add(new Cat()); // Ini akan merusak dogList yang seharusnya hanya berisi Dog!
    }
}
```


## Skenario tidak aman
Contoh tidak aman (*type safety violation*) terjadi jika bahasa pemrograman membiarkan **Invariance** dilanggar, atau saat tipe yang bisa **membaca sekaligus menulis** (Read & Write) dipaksa menajadi Covariant. 

### 1. Kasus Array di C# (Covariance Array yang Rentan Crash)
Di C# **Array bersifat covariant** pada kasus `Dog[]` yang dianggap sebagai `Animal[]`. Hal ini terjadi karena array bisa dibaca dan ditulis, perilaku ini tidak sesuai dengan *type safety* dan menyebabkan *runtime error*
```cs
public class Animal { }
public class Dog : Animal { }
public class Cat : Animal { }

class Program
{
    static void Main()
    {
        Dog[] dogs = new Dog[2];

        // Covariance pada Array: Diizinkan oleh kompiler C#
        Animal[] animals = dogs; 

        // BAHAYA! Kompiler membiarkan baris ini karena Cat adalah Animal
        // Tetapi secara runtime, variabel ini sebenarnya adalah array Dog!
        animals[0] = new Cat(); 

        // Akibat: Program CRASH saat dijalankan dengan error:
        // System.ArrayTypeMismatchException: 'Attempted to access an element as a type incompatible with the array.'
    }
}
```

### 2. Seandainya `List<T>` di C# bersifat Covariant (Mengapa `IList<T>` Harus Invariant)
`List<T>` dibuat **Invariant** untuk mencegah masalah yang terjadi pada Array di atas. seandainya `List<T>` dibuat **covariant** tanpa batasan, kode berikut akan merusak koleksi data:
```cs
List<Dog> dogList = new List<Dog>();

// Seandainya C# mengizinkan ini (Covariance tanpa batasan):
List<Animal> animalList = dogList; // (Hipotesis: Diizinkan)

// Kita menambah Cat ke dalam daftar Animal
animalList.Add(new Cat()); 

// BAHAYA KETIKA MEMBACA DATA DARI dogList:
Dog firstDog = dogList[0]; 
// Program akan crash dengan InvalidCastException karena dogList[0] ternyata berisi Cat!
```
### 3. Mengakali Generics dengan Cast Manual (Tidak Aman)
Jika memaksa `casting` tipe generic secara eksplisit untuk menembus aturan Invariance, **program akan gagal saat mencoba mengakses method khusus *subclass*.**
```cs
List<Dog> dogs = new List<Dog> { new Dog() };

// Memaksa cast ke object lalu ke List<Animal> (sangat tidak disarankan)
object rawList = dogs;
List<Animal> animals = (List<Animal>)rawList; // Throws System.InvalidCastException at runtime!
```
### 4. Mengapa Covariance `out` di C# Selalu Aman?
Sistem tipe C#, memastikan keamanan dengan aturan ketat: **Jika kamu menggunakan keyword `out T`, method di dalam interface tersebut TIDAK BOLEH menerima `T` sebagai parameter**
```cs
public interface IReadOnlyContainer<out T>
{
    T GetItem(); // AMAN: Mengembalikan data
    
    // ERROR KOMPILASI! C# melarang ini:
    // void AddItem(T item); 
    // Alasan: Memasukkan data melalui 'out' akan merusak Type Safety seperti pada contoh Cat & Dog di atas.
}
```

## Ringkasan Keyword C#
| Jenis Covariance | Keyword C# | Batasan Penggunaan | Contoh Bawaan .Net|
| --- | --- | --- | --- |
| **Covariance** | `out T` | Hanya sebagai *return* value | `IEnumerable<out T>`, `IReadOnlyList<out T>, Func<out TResult>`|
| **Contravariance** | `in T` | Hanya sebagai *parameter input* | `IComparer<in T>`, `IEqualityComparer<in T>`, `Action<in T>`
| **Invariance** | `T` (tanpa `in`/`out`) | Digunakan untuk input sekaligus output | `List<T>`, `IList<T>`, `Dictionary<TKey, TValue>`