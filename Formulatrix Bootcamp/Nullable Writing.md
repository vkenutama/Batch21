Letak tanda tanya `(?)` menentukan **bagian mana yang boleh bernilai** `null`, apakah elemen di dalam array-nya, objek array-nya sendiri, atau dua-duanya.
Konsep ini erat kaitannya dengan fitur **Nullable Reference Types** di C#.

# Ringkasan Cepat
| Sintaks | Array-nya Ada? | Elemen di Dalamnya Boleh `null`? |
| ------- | -------------- | -------------------------------- |
| `User?[] users`|**Wajib ada** (Array nya tidak boleh `null`|**Boleh** `null`|
|`User[]? users`| **Boleh** `null` (Array bisa bernilai `null`)| **Tidak boleh** `null`|
|`User?[]? users`| **Boleh** `null` (Array bisa bernilai `null`) | **Boleh** `null|`

# Penjelasan Rinci Setiap Sintaks
1.  ### `User?[] users`
	- **Cara Baca**: "Ini adalah sebuah array `User` yang **objek array-nya harus ada** (tidak boleh `null`), tetapi **elemen-elemen di dalamnya boleh bernilai** `null`"
	- **Penempatan** `?`: Tanda `?` menempel pada `User` (`User?`), sehingga yang dipengaruhi adalah tipe elemennya.
	
	```cs
	User?[] users = new User?[] 
	{ 
	    new User("Budi"), 
	    null,               // Boleh null!
	    new User("Siti") 
	};
	
	// users = null; // ERROR! Array-nya sendiri tidak boleh null.
	```
1. ### `User[]? users`
	- **Cara Baca:** "Ini adalah sebuah variabel array `User` yang **objek array-nya sendiri boleh** `null`, tetapi jika array-nya ada, semua elemen di dalamnya tidak boleh `null`"
	- **Penempatan** `?`: Tanda `?` menempel pada kurung siku `[]?`, sehingga yang dipengaruhi adalah wadah/objek array-nya.
	```cs
	User[]? users = null; // Boleh null!

	// Jika diisi array, tidak boleh ada elemen null di dalamnya:
	users = new User[] 
	{ 
	    new User("Budi"), 
	    // null // ERROR! Elemen di dalam array tidak boleh null.
	};
	```
2. ### `User?[]? users` 
	-**Cara Baca**: "Ini adalah sebuah variabel array `User` yang **paling fleksibel**: objek array-nya **boleh** `null`, DAN elemen-elemen di dalam array-nya juga **boleh bernilai** `null`".
	```cs
	User?[]? users = null; // Boleh null!

	// Dan jika diisi array, elemennya juga boleh null:
	users = new User?[] 
	{ 
	    new User("Budi"), 
	    null,               // Boleh null!
	    new User("Siti") 
	};
	```
# Ilustrasi Analogi
Bayangkan sebuah **Rak Sepatu** (`[]`) dan **Sepatu** (`User`):
- `User?[]` : Rak sepatunya **pasti ada**, tetapi beberapa kotak di dalam rak bisa **kosong (tanpa sepatu)**.
- `User[]?` : Rak sepatunya **bisa jadi tidak ada sama sekali** (diisi `null`), tetapi jika raknya ada, seluruh kotaknya **harus terisi sepatu**
- `User?[]?`: Rak sepatunya bisa jadi ttidak ada, dan **kalaupun ada** kotaknya **boleh ada yang kosong**