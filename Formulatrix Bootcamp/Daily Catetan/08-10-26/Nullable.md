Nullable memungkinkan suatu tipe data nilai *Value Type* seperti `int`, `double`, `bool`, `DateTime`, atau `struct` untuk menerima nilai `null`.

*Value Type* secara default tidak boleh bernilai `null` karena harus selalu menyimpan data misal `int` defaultnya `0`, `bool` defaultnya `false`. Konsep nullable menjembatani situasi di mana suatu data memang belum ada atau tidak diketahui seperti kolom bernilai `NULL` di DB atau input opsional pada API.

# Nullable Value Types (`T?`)
Untuk membuat variabel menjadi nullable, tambahkan tanda tanya `?` sesudah `tipe_data`, atau gunakan struktur generic `Nullable<T>`.
**Sintaks**
```cs
int? angka = null; // Pendekatan singkat (Sangat Direkomendasikan)
Nullable<int> angkaLengkap = null; // Bentuk formal eksplisit
```
**Properti Utama**
Setiap variabel `Nullable<T>` memiliki dua properti penting:
- `HasValue` (`bool`): bernilai `true` jika variable memiliki nilai, dan `false` jika bernilai `null`.
- `Value` (`T`): Mengambil nilai aslinya. Jika dipanggil saat `HasValue == false`, program akan melempar exception `InvalidOperationException`.
**Contoh Penggunaan**
```cs
int? umur = null;

if (umur.HasValue)
{
    Console.WriteLine($"Umur: {umur.Value}");
}
else
{
    Console.WriteLine("Umur tidak diisi.");
}
```
# Operator Penting Terkait Nullable
## Null-Coalescing Operator (`??`)
Digunakan untuk memberikan nilai default jika variabel bernilai `null`.
```cs
int? skor = null;
int skorAkhir = skor ?? 0; // Jika 'skor' null, gunakan nilai 0
```

## Null-Coalescing Assignment Operator (`??=`)
Mengisi nilai ke variabel hanya jika variabel tersebut saat ini bernilai `null`.
```cs
int? jumlah = null;
jumlah ??= 10; // Variabel 'jumlah' sekarang bernilai 10
```
## Null-Conditional Operator (`?.`)
Mencegah error `NullReferenceException` saat **mengakses properti atau method** yang mungkin bernilai `null`.
```cs
DateTime? tanggalLahir = null;
int? tahun = tanggalLahir?.Year; // 'tahun' akan otomatis bernilai null tanpa melempar error
```

# Nullable Reference Types (Fitur C# 8.0)
Sebelum C# 8.0 semua Reference Type (`string`, `class`) secara otomatis bersifat nullable. Namun, mulai C# 8.0 diperkenalkan fitur **Nullable Reference Types** untuk mencegah *NullReferenceException* (sering dipanggil sebagai "*The Billion Dollar Mistake*").

Jika fitur ini diaktifkan pada proyek C# (`<Nullable>enable</Nullable>`):
```cs
string namaA = "Budi";  // Non-nullable reference type (tidak boleh null)
string? namaB = null;   // Nullable reference type (boleh null)

// Kompilator akan memberikan peringatan (warning) jika Anda mencoba memasukkan null ke namaA:
namaA = null; // Warning: Converting null literal or possible null value to non-nullable type.
```
# Ringkasan
| Kategori | Tanpa Nullable | Dengan Nullable(`?`) |
|---|---|---|
|**Value Type** (`int`, `bool`, `struct`) | Tidak bisa bernilai `null` (memiliki nilai default seperti `0` atau `false`)| Bisa bernilai `null` untuk menandakan data tidak ada.|
|**Reference Type** (`string`, `class`) | (Sebelum C# 8) selalu bisa dianggap `null`. (C# 8+) dianggap **non-null** secara default jika fitur dieksekusi | Boleh bernilai `null` secara eksplisit dan diperiksa oleh kompilator|

# Pattern Matching untuk Null Checking
Pattern matching merupakan teknik modern berfokus pada **keterbacaan kode (readability)**, **keamanan compile-time**, dan pembatasan error *NullReferenceException* seawal mungkin. 

Dua teknik yang paling penting dan sering digunakan adalah **Pattern Matching** dan **Guard Clause**.

## Pattern Matching untuk Null Checking
Pattern matching menggantikan cara lama seperti `if (obj == null)` atau `if (obj != null)`.

### Mengapa Menggunakan Pattern Matching 
Operator seperti `==` dan `!=` bisa di-overload secara kustom oleh sebuah kelas, sehingga pemeriksaan `if (obj == null)` berpotensi mengeksekusi logika kustom yang lambat atau bahkan melempar exception.
Pattern matching seperti `is null` dijamin melakukan pemeriksaan **referensi murni (bypass operator overloading)**

#### Pattern `is null` dan `is not null`
Sintaks ini sangat mudah, karena sangat mirip dengan bahasa sehari-hari.
```cs
string? nama = GetNamaFromDatabase();

// Memeriksa apakah null
if (nama is null)
{
    Console.WriteLine("Nama tidak ditemukan.");
}

// Memeriksa apakah TIDAK null (C# 9+)
if (nama is not null)
{
    Console.WriteLine($"Panjang nama: {nama.Length}");
}
```
#### Type Pattern dengan Deklarasi Variabel (`is Type var`)
Memeriksa apakah objek tidak `null` **sekaligus melakukan casting otomatis** ke variabel baru dalam satu baris.
```cs
object data = "Halo C#";

// Memeriksa apakah 'data' tidak null DAN bertipe string
if (data is string teks)
{
    // 'teks' langsung bisa digunakan sebagai string di dalam blok ini
    Console.WriteLine(teks.ToUpper()); 
}
```
#### Property Pattern (`{}`)
Kurung kurawal `{}` mencocokkan objek apa pun yang **tidak null**, sekaligus bisa memeriksa properti di dalamnya.
```cs
Pelanggan? pelanggan = GetPelanggan();

// Memeriksa objek tidak null sekaligus memeriksa properti di dalamnya
if (pelanggan is { IsActive: true, Alamat: not null })
{
    Console.WriteLine($"Mengirim email ke {pelanggan.Alamat}");
}
```

## Guard Clauses (Proteksi Input Method)
Ini adalah pola pemrograman di mana pemeriksaan validitas argumen **terjadi di awal fungsi/method**. Jika argumen nilainya `null` method akan langsung melempar exception sebelum logika utama berjalan.
### Cara Modern: `ArgumentNullException.ThrowIfNull`
Ini adalah cara yang paling direkomendasikan. Kompilator secara otomatis mengambil nama variabel menggunakan atribut `[CallerArgumentExpression]`.
```cs
public void ProsesPesanan(Pesanan pesanan, User pengirim)
{
    // Satu baris terbersih di C# 10+ / .NET 6+
    ArgumentNullException.ThrowIfNull(pesanan);
    ArgumentNullException.ThrowIfNull(pengirim);

    // Logika utama aman dijalankan di sini
    pesanan.Proses();
}
```
### Throw Expression dengan Operator `??`
Gunakan operator `??` jika ingin melakukan null checking saat melakukan inisialiasi properti atau konstruktor (dependency injection).
```cs
public class LayananPelanggan
{
    private readonly IRepository _repository;

    public LayananPelanggan(IRepository repository)
    {
        // Jika 'repository' null, langsung lempar ArgumentNullException
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
}
```
### Cara Tradisional
Cara lama membutuhkan lebih banyak baris kode yang sebaiknya diganti saja menggunakan `ThrowIfNull` di proyek.
```cs
public class LayananPelanggan
{
    private readonly IRepository _repository;

    public LayananPelanggan(IRepository repository)
    {
        // Jika 'repository' null, langsung lempar ArgumentNullException
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
}
```