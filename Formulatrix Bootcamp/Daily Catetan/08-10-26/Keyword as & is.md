Keyword `as` dan `is` digunakan untuk memeriksa dan mengonversi tipe data pada *reference type* atau *nullable type*, namun keduanya memiliki fungsi utama dan hasil akhir yang berbeda.
# Kata Kunci `is` (Pengecekan Tipe Data)
`is` digunakan untuk **memeriksa** apakah suatu objek kompatibel dengan tipe data tertentu. Operator ini **mengemballikan nilai `bool`** (true/false) dan tidak akan pernah melempar exception.

## Fitur Utama
- Memeriksa tipe tanpa mengubah objek aslinya
- Bisa juga digunakan untuk melakukan pengecekan **sekaligus konversi (casting)** dalam satu baris melalui *pattern matching*.
## Contoh Kode
```cs
object data = "Halo C#";

// Pengecekan standar
if (data is string)
{
    Console.WriteLine("Data adalah sebuah string.");
}

// Pattern Matching (Pengecekan + Konversi otomatis ke variabel 'teks')
if (data is string teks)
{
    Console.WriteLine(teks.ToUpper()); // 'teks' langsung bisa digunakan
}
```

# Keyword `as` (Konversi Tipe Data)
`as` digunakan untuk **mengonversi (casting)** suatu objek ke tipe data yang dituju secara aman. Jika konvesrsi **gagall**, hasilnya adalah `null` (bukan exception).

## Fitur Utama:
- Hanya digunakan pada *reference type* seperti `class`, `string`, `interface` atau *nullable value type*  seperti `int?`, `double?`, dst.
- Tidak melempar exception apabila tipe data tidak cocok.
## Contoh
```cs
object data = 123; // Nilai asli adalah int

// Mencoba konversi ke string menggunakan 'as'
string? teks = data as string;

if (teks is not null)
{
    Console.WriteLine($"Berhasil konversi: {teks}");
}
else
{
    Console.WriteLine("Konversi gagal, variabel 'teks' bernilai null.");
}
```
