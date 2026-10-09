Lazy evaluation (evaluasi tunda) pada iterator berarti kode di dalam metode iterator **tidak langsung dieksekusi** saat method tersebut dipanggil. Kode baru **dijalankan secara bertahap pada saat item benar-benar diminta** oleh konsumen (misalnya melalui perulangan `foreach` atau pemanggilan `.MoveNext()`.

Iterator di C# dibuat menggunakan kata kunci `yield return` atau ekstensi LINQ seperti `Where` dan `Select`.

# Bagaimana `yield return` Bekerja
Saat mencapai `yield return`, kompilator secara otomatis mengubah fungsi tersebut menjadi sebuah **state machine** (mesin status) berbentuk kelas tersembunyi yang mengimplementasikan `IEnumerable<T>` dan `IEnumerator<T>`.
```cs
public static IEnumerable<int> AmbilAngka()
{
    Console.WriteLine("Langkah 1");
    yield return 10;

    Console.WriteLine("Langkah 2");
    yield return 20;

    Console.WriteLine("Langkah 3");
    yield return 30;
}
```
**Eksekusi Tahap demi Tahap**
```cs
// 1. Pemanggilan metode TIDAK MENJALANKAN kode sama sekali!
IEnumerable<int> hasil = AmbilAngka(); 
Console.WriteLine("Metode baru saja dipanggil.");

// 2. Kode di dalam AmbilAngka() mulai dieksekusi di sini
foreach (int angka in hasil)
{
    Console.WriteLine($"Menerima: {angka}");
}
```
**Output**
```
Metode baru saja dipanggil.
Langkah 1
Menerima: 10
Langkah 2
Menerima: 20
Langkah 3
Menerima: 30
```

# Alur Kerja Eksekusi 
Ketika perulangan `foreach` dijalankan terhadap objek iterator:
1. `foreach` memanggil metode `GetEnumerator()`, lalu memanggil `.MoveNext()` pertama kali.
2. Eksekusi kode berjalan dari awal metode hingga bertemu kata kunci `yield return` pertama.
3. Nilai dikembalikan ke pembaca (`Current`), dan state machine **membekukan (pause) eksekusi** tepat di posisi tersebut.
4. Ketika `foreach` meminta item berikutnya (memanggil `.MoveNext()` lagi), eksekusi **dilanjutkan persis dari posisi pause terakhir**, bukan dari awal metode.
5. Proses ini berulang hingga **metode selesai** atau bertemu `yield break`.

# Lazy Evaluation pada LINQ
Sebagian besar metode ekstensi LINQ (`Where`, `Select`, `Take`, dll) menerapkan lazy evaluation secara default.
```cs
List<int> angka = new List<int> { 1, 2, 3, 4, 5 };

// Query belum dieksekusi sama sekali!
var query = angka
    .Where(n => {
        Console.WriteLine($"Filtering {n}");
        return n % 2 == 0;
    })
    .Select(n => {
        Console.WriteLine($"Mapping {n}");
        return n * 10;
    });

Console.WriteLine("Query telah didefinisikan.");

// Eksekusi baru terjadi di sini item per item
foreach (var item in query)
{
    Console.WriteLine($"Hasil: {item}");
}
```
**Output**
```
Query telah didefinisikan.
Filtering 1
Filtering 2
Mapping 2
Hasil: 20
Filtering 3
Filtering 4
Mapping 4
Hasil: 40
Filtering 5
```
> **Perhatikan**: LINQ memproses item satu per satu secara streaming. Item `2` diproses melalui `Where` lalu langsung dikirm ke `Select` dan dibaca `foreach` sebelum ke item `3` untuk mulai diproses.

# Keuntungan & Jebakan Lazy Evaluation
## Keuntungan
- **Efisien Memory**: Tidak perlu memuat/menampung seluruh dataset besar ke dalam array/list di memory.
- **Mendukung Data Tak Terhingga**: Karena data *on-demand* maka kita bisa membuat urutan tanpa batas (contoh: infinite stream angka acak).
- **Penghematan Komputasi**: Jika hanya butuh misalkan 5 item pertama dari 1.000.000 data menggunakan `.Take(5)`, proses pemrosesan akan langsung berhenti setelah item ke-5 didapat.
## Jebakan
1. **Multiple Enumeration**: Membaca variable query berulang kali akan menyebabkan logika iterator **dieksekusi ulang dari awal** setiap kali dipanggil.
```cs
var query = AmbilDataDariDatabase(); // Belum dieksekusi
int jumlah = query.Count();          // Menjalankan query (Iterasi 1)
var list = query.ToList();           // Menjalankan query lagi! (Iterasi 2)
```
2. **Side Effect/Deffered Bug**: Jika data sumber berubah sebelum query diiterasi, hasil query bisa berubah secara tidak terduga.

# Eager Evaluation
Jika ingin langsung mengeksekusi iterator di memory (menghentikan perilaku *lazy*), konversikan hasilnya menggunakan `.ToList()` atau `.ToArray()`:
```cs
// Langsung dieksekusi saat baris ini berjalan
List<int> hasilDisimpan = AmbilAngka().ToList();
```
Eager evaluation bisa dibaca lebih lengkap [[Eager Evaluation | di sini]]