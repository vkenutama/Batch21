Letak tanda tanya `(?)` menentukan **bagian mana yang boleh bernilai** `null`, apakah elemen di dalam array-nya, objek array-nya sendiri, atau dua-duanya.
Konsep ini erat kaitannya dengan fitur **Nullable Reference Types** di C#.

# Ringkasan Cepat
| Sintaks | Array-nya Ada? | Elemen di Dalamnya Boleh `null`? |
| ------- | -------------- | -------------------------------- |
| `User?[]` users |**Wajib ada**(Array nya tidak boleh `null`|**Boleh** `null`|
|`User[]? users`| **Boleh** `null` (Array bisa bernilai `null`)| **Tidak boleh** `null`|
|`User?[]? users| **Boleh** `null` (Array bisa bernilai `null`) | **Boleh** `null|`

# Penjelasan Rinci Setiap Sintaks
