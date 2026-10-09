Boxing dan unboxing adalah proses konversi tipe data yang terjadi karena adanya dua kategori tipe data utama di C# yaitu: **Value Type** (tipe nilai) dan **Reference Type** (tipe acuan).

## Tipe Data Dasar 
- **Value Type** : Menyimpan data langsung di dalam memory **Stack**. Ringan dan cepat.
	- Contoh: `int`, `double`, `bool`, `char`, `struct`
- **Reference Type**: Menyimpan alamat/referensi ke objek yang berada di memory **Heap**.
	- Contoh: `object`, `string`, `class`, `interface`
## Boxing
**Boxing** adalah proses mengubah *Value Type* menjadi *Reference Type* (biasanya ke tipe `object` atau `interface`).

Proses ini membungkus nilai dari Stack ke dalam sebuah objek baru yang dialokasikan di memory Heap.

### Cara Kerja Boxing
1. Alokasi memory baru dilakukan di **Heap**.
2. Nilai dari variabel Stack **disalin** ke memory **Heap** tersebut
3. Alamat memory Heap tersebut disimpan sebagai referensi.
```cs
int angka = 100;     // Value Type (tersimpan di Stack)
object obj = angka;  // Boxing: angka dibungkus ke dalam tipe object (tersimpan di Heap)
```

## Unboxing
**Unboxing** adalah proses kebalikan dari boxing, yaitu mengambil nilai *Value Type* yang ada di dalam objek *Reference Type* (Heap) dan mengembalikannya ke variable *Value Type* (Stack).
> Unboxing memerlukan **casting secara eksplisit** `(tipe_data)`.

### Cara Kerja Unboxing
1. Program memeriksa apakah objek tersebut valid dan berisi tipe data yang sesuai.
2. Nilai yang ada di dalam Heap disalin kembali ke variabel di Stack.
```cs
object obj = 100;    // Objek di Heap
int angka = (int)obj; // Unboxing: nilai dari Heap diekstrak kembali menjadi int (Stack)
```
> **Catatan Penting**: Tipe data saat unboxing **harus sama persis** dengan tipe data asal saat dilakukan boxing. Jika tidak cocok, C# akan melempar error `InvalidCastException` **saat runtime**.

## Perbandingan
| Fitur | Boxing | Unboxing|
| ---| --- | --- |
|**Arah Konversi**| Value Type -> Reference Type | Reference Type -> Value Type|
|**Lokasi Memori** | Stack -> Heap | Heap -> Stack |
|**Casting** | Implisit (otomatis) | Eksplisit (manual dengan `(tipe)`)|
|**Performa** | Alokasi memori Heap baru | Pengecekan tipe data & konversi |

## Dampak Performa
Boxing dan unboxing membutuhkan biaya komputasi (*overhead*) yang cukup besar:
1. **Garbage Collector**: terlalu banyak boxing menciptakan banyak objek kecil di Heap yang memperberat kerja GC.
2. **Kecepatan**: Alokasi dan penyalinan memori memakan waktu lebih lambat dibandingkan operasi nilai biasa.
## Cara Menghindari Boxing/Unboxing
Gunakan **Generics** (`List<T>`, `Dictionary<TKey, TValue>`) daripada koleksi non-generic lama `ArrayList`
```cs
// TIDAK DIREKOMENDASIKAN (Terjadi Boxing & Unboxing):
ArrayList list = new ArrayList();
list.Add(10);          // Boxing! (int -> object)
int x = (int)list[0];  // Unboxing! (object -> int)

// DIREKOMENDASIKAN (Bebas Boxing/Unboxing dengan Generics):
List<int> listBagus = new List<int>();
listBagus.Add(10);     // Tidak ada boxing
int y = listBagus[0];  // Tidak ada unboxing
```