Delegate adalah ***type-safe function pointer*** (penunjuk fungsi yang aman terhadap tipe data). Delegate menyimpan referensi ke satu atau lebih *method* yang memiliki bentuk signature (parameter dan tipe `return`) yang sama.
Dengan delegate, `method`bisa dioper sebagai argumen ke *method* lain, menyimpan *method* dalam variabel, atau mengeksekusi sekumpulan *method* secara bergantian (*callback/event*)

# Konsep Dasar & Sintaks
Membuat dan menggunakan delegate membutuhkan 3 langkah dasar:

## Deklarasi 
Menentukan signature (tipe kembalian dan parameter)
```cs
delegate int Transformer(int x);
```
Pada kode di atas, delegate `Transformer` memiliki signature:
- **Parameter**: meminta 1 parameter `int`
- **Return**: mengembalikan *value* dengan nilai `int`

## Instansiasi
Menyambungkan delegate dengan *method* yang memiliki signature yang cocok. Misalkan sebuah method di bawah:
```cs
int Square(int x) {return x * x;}
```
Untuk menghubungkan delegate ke method, maka dapat *assign* langsung dengan cara:
```cs
Transformer t = Square;
```
## Pemanggilan (Invoke)
Setelah delegate memiliki referensi yang dipoint, maka *method* yang telah di-assign bisa langsung dipanggil dengan cara:
```cs
int answer = t(3);
// Output = 3 * 3 = 99
```
atau bisa juga menggunakan **cara eksplisit**:
```cs
int answer = t.Invoke(3);
// Output = 3 * 3 = 99
```

Kunci dari delegate adalah **indirection**. Pemanggil **memanggil delegate**, lalu **delegate memanggil target method** nya. Ini digunakan untuk memisahkan pemanggil dari implementasi target method nya.

# Contoh Penggunaan Delegate
Delegate bisa digunakan untuk membuat **plug-in architecture** atau **callback mechanism**, di mana karakteristik method bisa dikostumisasi pada runtime.
Contohnya pada kode di bawah:
```cs
delegate int Transformer(int x); 
int Square(int x) => x * x; 
int Cube(int x) => x * x * x; 

void Transform(int[] values, Transformer t)// 't' is a delegate parameter 
{ for (int i = 0; i < values.Length; i++) 
	values[i] = t(values[i]); // Invoke the plug-in method 
}
```
Lalu di main code bisa digunakan seperti ini:
```cs
int[] values = {1, 2, 3};

Transform(values, Square);

foreach (int i in values)
{
	Console.Write(i + " "); // 1 4 9 
}
```
Di sini, `Transform` disebut **higher-order function** karena memiliki parameter yang menerima fungsi (direpresentasikan dalam bentuk delegate) sebagai argument. 

# Target Instance dan Static Method
Delegate bisa mereferensikan baik itu method `static` maupun `instance`.
**Static Method Target**:
```cs
class test 
{
	public static int Square(int x) => x * x;
}
delegate int Transformer(int x);

//...
Transformer t = Test.Square; // Referensi ke static method
Console.WriteLine(t(10));
```
**Instance Method Target**:
```cs
class Test { public int Square(int x) => x * x; } 
delegate int Transformer(int x); 
Console.WriteLine(t(10)); // Output: 100
```
Ketika delegate menyimpan instance method, delegate object tidak hanya referensi ke method namun juga reference ke **instance** spesifik dari object yang memiliki method tersebut. Instance ini bisa diakses melalui kelas `System.Delegate` pada `Target` property (yang mana `null` pada static method).   

# Multicast Delegate
Multicast delegate adalah kemampuan variabel delegate untuk **menyimpan** dan **mengeksekusi**  **lebih dari satu method secara berurutan** (*invocation list*) hanya dengan sekali panggil.

## Cara Kerja Operator `+=` dan `-=`
C# menyediakan dua operator utama untuk mengelola daftar method di dalam Multicast Delegate, yaitu:
1. **Operator `+=` (Menambahkan Method)**: operator ini berfungsi untuk menambahkan method baru ke barisan paling belakang dari rantai eksekusi (*invocation list*).
2. **Operator `-=` (Menghapus Method)**: mencabut method tertentu dari rantai eksekusi. Jika method yang dicabut tidak ada dalam daftar, C# tidak akan melempar *error* (diabaikan).

## Syarat Penting Multicast Delegate
- **Tipe Kembalian Harus** `void`: sebagian besar multicast delegate menggunakan tipe kembalian `void`. Jika menggunakan delegate yang mengembalikan nilai (misalnya `int`), **hanya nilai dari method terakhir yang dieksekusi dan dikembalikan valuenya**. Nilai dari method-method sebelumnya akan hilang/tertimpa.
- **Eksekusi Berurutan (Synchronous)**: method dipanggil satu per satu sesuai urutan pendaftarannya (`+=`). Method kedua tidak akan jalan sebelum method pertama selesai.

### Multicast yang Tertimpa
Multicast delegate yang tertimpa terjadi karena function yang direference memiliki return value **YANG BUKAN** `void`. Misalkan sebuah delegate:
```cs
private delegate int Add(int x);
```
Dan beberapa fungsi berikut:
```cs
static int AddTwo(int x) => x + 2;  
static int AddThree(int x) => x + 3;  
static int AddFour(int x) => x + 4;
```
Ditambahkan ke reference dari delegate melalui operator `+=`:
```cs
Add add = AddTwo;  
add += AddThree;  
add += AddFour;
Console.WriteLine(add(2));
```
Maka pemanggilan `add(2)` akan menggunakan nilai dari method terakhir yang ditambahkan yaitu `AddFour`:
```
6
Process finished with exit code 0.
```

### Contoh Penggunaan Multicast Delegate
Sebuah delegate dideklarasikan dengan return type `void`:
```cs
public delegate void Notifier(string message);
```
Lalu akan ada beberapa method sebagai target:
```cs
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
```
Untuk menambahkan method yang akan di-invoke oleh delegate bisa menggunakan sintaks:
```cs
Notifier notifier = SendToConsole;  

notifier += SendToLog;  
notifier += SendSmsToAdmin;
```
Lalu, apabila program dieksekusi dengan:
```cs
notifier.Invoke("Warning message, temperature drop");
```
Maka outputnya:
```
[CONSOLE] Sending message: Warning message, temperature drop
[LOG] Sending message: Warning message, temperature drop
[SMS Admin] Sending message: Warning message, temperature drop

Process finished with exit code 0.
```
Pada output di atas, terlihat bahwa delegate mengeksekusi program sesuai dengan urutan penambahan reference method melalui operator `+=`.

> **Penting**: Delegate bersifat **immutable**, saat menggunakan operator `+=` atau `-=` maka delegate yang sudah ada tidak dimodifikasi, melainkan terjadi **pembuatan** delegate baru untuk membuat invocation list yang telah diperbarui ke dalam instance baru. 

# Generic Delegate Types
Sama seperti `class` dan `method`, tipe delegate juga bisa memiliki **generic type parameters**, yang bisa digunakan untuk berbagai tipe data dan *reusable*.
```cs
public delegate TResult Transformer<TArg, TResult>(TArg arg);
```
Dengan ini, `Transform` method bisa digeneralisasikan menjadi:
```cs
public class Util
{
	public static void Transform<T>(T[] values, Transformer<T, T> t)
	{
		for(int i = 0; i < values.Length; i++)
		{
			values[i] = t(values[i]);
		}
	}
}
```
Dapat dijalankan dengan:
```cs
int Square(int x) => x * x;
int[] values = {1, 2, 3};
Util.Transform(values, Square);
```

# `Func` dan `Action` Delegate
C# memberikan pre-defined generic delegate dalam namespace `System` yang meng-cover hampir seluruh method signature umum. Ini adalah `Func` dan `Action`:
- `Func<TResult>` : merepresentasikan method yang **tidak mengambil argumen apapun** dan mengembalikan nilai dengan tipe `TResult`.
- `Func<TArg, TResult>`: merepresentasikan method yang **mengambil sebuah argumen** dengan tipe `TArg` dan mengembalikan nilai dengan tipe `TResult`.
- `Func<T1, T2, TResult>`: ... dan seterusnya, hingga maksimal 16 input parameter.
- `Action`: merupakan representasi method yang tidak mengambil argumen dan mengembalikan `void`
- `Action<TArg>`: mengambil satu buah argument dengan tipe `TArg` dan mengembalikan `void`.
- `Action<T1, T2>`: .... dan seterusnya, hingga maksimal 16 input parameter.
Kemudian delegate `Transformer<T, T>` bisa digantikan menggunakan `Func<T,T>` menjadi:
```cs
public static void Transform<T>(T[] values, Func<T, T> transformer) { 
	for (int i = 0; i < values.Length; i++) 
		values[i] = transformer(values[i]); 
}
```
Skenario penggunaan `Func`/`Action` biasanya tidak meng-cover methods dengan `ref` atau `out` parameter. Kebanyakan code di C# sekarang menggunakan `Func` dan `Action` dibandingkan dengan custom delegate karena lebih general.

# Perbedaan `Func` dan `Action`
Perbedaan utama antara `Func` dan `Action` adalah pada **ada atau tidaknya nilai kembalian (return value)**.
Keduanya digunakan untuk menyimpan referensi ke sebuah method tanpa perlu mendeklarasikan `delegate` secara manual.
## Tabel Perbandingan
| Fitur | `Func` | `Action` |
| --- | --- | --- |
|**Return value** | **Wajib ada** (mengembalikan nilai) | **Tidak ada** (`void`)|
|**Posisi Tipe Return** | 0 hingga 16 parameter input + 1 parameter output | 0 hingga 16 parameter input|
|**Posisi tipe return**| Parameter generic **paling akhir** (`Func<..., TResult>`) | Tidak ada (semua parameter generic adalah input)
|**Tujuan Penggunaan** | Kalkulasi, transformasi data, pemetaan (*mapping*) | Menjalankan aksi, logging, mengubah status, cetak output |

### `Func` (Mengembalikan Nilai)
`Func` digunakan ketika method yang dipanggil **memproses sesuatu dan mengembalikan hasil.** 
- `Func<TResult>`: Method tanpa parameter input, mengembalikan `TResult`.
- `Func<T, TResult>`: Method dengan 1 parameter input (`T`), mengembalikan tipe `TResult`
-  `Func<T1, T2, TResult>`: Method dengan 2 parameter input (`T1`, `T2`), mengembalikan tipe `TResult`.

**Contoh kode** `Func`:
```cs
using System;

class Program
{
    static void Main()
    {
        // 1. Func tanpa parameter input, mengembalikan string
        Func<string> getGreeting = () => "Selamat Pagi!";
        string pesan = getGreeting(); // Hasil: "Selamat Pagi!"

        // 2. Func dengan 2 parameter int input, mengembalikan int (TResult paling belakang)
        Func<int, int, int> tambah = (a, b) => a + b;
        int hasil = tambah(10, 20); // Hasil: 30

        // 3. Func dengan input string, mengembalikan bool
        Func<string, bool> isLongString = text => text.Length > 5;
        bool cek = isLongString("Halo"); // Hasil: false
    }
}
```

### `Action` (Tidak Mengembalikan Nilai) / `void`
`Action` digunakan ketika method yang dipanggil **hanya melakukan serangkaian perintah tanpa memberikan nilai balik**.
- `Action`: Method tanpa parameter input dan return `void`
- `Action<T>`: Method dengan 1 parameter input (`T`) dan return `void`.
- `Action<T1, T2>`: method dengan 2 parameter input (`T1`, `T2`) dan return `void`.

**Contoh kode** `Action`:
```cs
using System;

class Program
{
    static void Main()
    {
        // 1. Action tanpa parameter input
        Action sayHello = () => Console.WriteLine("Halo Dunia!");
        sayHello(); // Output: Halo Dunia!

        // 2. Action dengan 1 parameter string
        Action<string> logMessage = msg => Console.WriteLine($"[LOG]: {msg}");
        logMessage("Aplikasi dimulai"); // Output: [LOG]: Aplikasi dimulai

        // 3. Action dengan 2 parameter (int, string)
        Action<int, string> printStatus = (code, text) => 
            Console.WriteLine($"Status {code}: {text}");
        printStatus(200, "OK"); // Output: Status 200: OK
    }
}
```
