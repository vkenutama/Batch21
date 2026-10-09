Evaluasi ini disebut juga *strict evaluation* atau evaluasi langsung, merupakan strategi evaluasi yang mana ekspresi atau perintah **langsung dihitung dan dieksekusi saat dideklarasikan atau ditetapkan ke variabel**, tanpa menunggi diminta atau dibutuhkan oleh kode lain.
# Karakteristik Eager Evaluation
- **Eksekusi Instant**: Nilai langsung dihitung di tempat saat baris kode tersebut berjalan.
- **Alokasi Memori Langsung**: Seluruh hasil perhitungan langsung disimpan di dalam memori.
- **Prediktabilitas Tinggi**: *Side effect* (seperti query DB, penulisan file, atau print log) terjadi secara berurutan persis sesuai urutan baris kode.

# Contoh Eager vs Lazy Evaluation
## Perbandingan Sederhana
```cs
// EAGER EVALUATION
// Perhitungan langsung terjadi di baris ini. Nilai 15 langsung disimpan di memori.
int hasilEager = 5 + 10; 


// LAZY EVALUATION (menggunakan Func)
// Perhitungan BELUM terjadi. Yang disimpan baru instruksi/rumusnya.
Func<int> hasilLazy = () => 5 + 10; 

int nilaiAplikasi = hasilLazy(); // Perhitungan baru dieksekusi di baris ini
```

## Pada LINQ & Koleksi Data
Pemanggilan `.ToList()` atau `.ToArray()` atau operasi agregat seperti `.Count()` dan `.Sum()` akan **memaksa** proses evaluasi berubah dari *lazy* menjadi *eager*.
```cs
List<int> angka = new List<int> { 1, 2, 3, 4, 5 };

// EAGER EVALUATION (karena menggunakan .ToList())
// Seluruh proses filtering (Where) dan transformasi (Select) 
// LANGSUNG dijalankan saat baris ini dieksekusi.
List<int> hasilEager = angka
    .Where(n => n % 2 == 0)
    .Select(n => n * 10)
    .ToList(); // Mengubah query menjadi eager

// Data sudah matang dan tersimpan penuh di dalam RAM
Console.WriteLine(hasilEager[0]);
```

# Kelebihan dan Kekurangan
| Kelebihan| Kekurangan|
|---|---|
|**Performa Akses Cepat:** Data sudah siap di memori, sehingga pembacaan berulang kali sangat cepat.|**Konsumsi Memori Tinggi:** Menyimpan seluruh dataset ke memori sekaligus (bisa menyebabkan _Out of Memory_ pada data besar).|
| **Menghindari Multiple Execution:** Aman dari masalah eksekusi ulang (_multiple enumeration_) yang biasa terjadi pada lazy query. | **Waktu Tunggu Awal:** Membutuhkan waktu pemrosesan di awal sebelum program dapat melanjutkan ke baris berikutnya.|
| **Alur Program Jelas:** Mudah di-_debug_ karena _error_ atau _exception_ langsung muncul di lokasi baris kodenya. | **Pemborosan Komputasi:** Menghitung data yang mungkin pada akhirnya tidak pernah dipakai oleh aplikasi.|

# Kapan Harus Menggunakan
- **Data Perlu Diakses Berulang Kali:** Jika Anda memproses suatu query LINQ dan ingin menggunakan hasilnya di beberapa tempat berbeda tanpa memicu query ulang ke database/memori.
- **Mengisolasi Data dari Perubahan:** Saat Anda ingin mengambil _snapshot_ data pada momen tertentu sebelum data sumbernya berubah.
- **Ukuran Data Terbatas & Pasti:** Ketika dataset berukuran relatif kecil sehingga muat di dalam RAM tanpa mengganggu performa aplikasi.