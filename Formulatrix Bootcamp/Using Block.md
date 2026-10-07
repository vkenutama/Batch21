`using` (atau blok `using(){}`) di C# digunakan untuk **mengelola dan membersihkan sumber daya tak terkelola (*unmanaged resources*)** secara otomatis, segera setelah sumber daya tersebut selesai digunakan. 
Di balik layar, `using` memanggil metode `.Dispose()` dari interface `IDisposable` yang diimplementasikan oleh suatu objek.

# Mengapa `using` Sangat Penting?
GC pada .NET secara otomatis membersihkan memori terkelola (*managed memory*). Namun, GC **tidak tahu kapan dan bagaimana** cara membersihkan sumber daya tak terkelola milik sistem operasi, seperti:
- Handle file di disk (`FileStream`, `StreamReader`, `StreamWriter`)
- Koneksi basis data (`SqlConnection`, `DbContext`)
- Socket jaringan (`HttpClient`, `TcpClient`)
- Sumber daya grafik/gambar (`Bitmap`, `Graphics`)
Jika objek-objek ini tidak segera ditutup/dibersihkan, aplikasi dapat mengalami **kebocoran sumber daya (resource leak)**, file menjadi terkunci (*locked*), atau koneksi database menjadi habis (*connection pool exhaustion*).

# Cara Kerja `using` di Balik Layar
Ketika menulis blok `using` seperti ini:
```cs
using (StreamReader reader = new StreamReader("data.txt"))
{
    string isi = reader.ReadToEnd();
    Console.WriteLine(isi);
} // Di titik ini, reader.Dispose() otomatis dipanggil!
```
Kompiler C# sebenarnya mengubah kode di atas menjadi struktur `try-finally`:
```cs
StreamReader reader = new StreamReader("data.txt");
try
{
    string isi = reader.ReadToEnd();
    Console.WriteLine(isi);
}
finally
{
    if (reader != null)
    {
        ((IDisposable)reader).Dispose(); // Dijamin TETAP dipanggil meskipun ada error/exception!
    }
}
```
Keuntungannya adalah method `.Dispose()` **dijamin akan selalu dipanggil**, bahkan jika terjadi *error (exception)* di dalam blok kode tersebut.

# Sintaks `using` Modern (C# 8.0)
Mulai C# 8.0 penggunaan tanda kurung kurawal `{}` secara eksplisit tidak lagi diperlukan, anda dapat menggantinya menjadi:
```cs
using var reader = new StreamReader("data.txt"); 
string isi = reader.ReadToEnd(); Console.WriteLine(isi);
```
# Syarat Objek yang Bisa Menggunakan `using`
> Objek yang dimasukkan ke dalam using **wajib implementasi interface** `IDisposable` (atau `IAsyncDisposable` untuk `await using`). Jika mencoba memasukkan tipe data biasa yang tidak memiliki data tersebut (misalnya `string`), kompilasi akan gagal dengan error.

```cs
// ERROR KOMPILASI! string tidak mengimplementasikan IDisposable
using (string teks = "Halo") 
{
}
```