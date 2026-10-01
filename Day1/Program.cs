using System;
using Day1;

#region Numeric Types

// // Integral Types
// // Byte data unsigned vs signed
// sbyte sb = sbyte.MaxValue;
// byte b = byte.MaxValue;
// Console.WriteLine($"Max value of sbyte: {sb}, byte: {b}");

// int a = int.MaxValue;
// nint nInt = nint.MaxValue;
// Console.WriteLine($"Max value of int: {a}, nint: {nInt}");

// // Unsigned
// ushort uShort = ushort.MaxValue;
// Console.WriteLine($"Max value of ushort: {uShort}");

// // Real type
// float g = 9.81f;
// double pi = 3.14159;
// decimal paymentsTotal = 1_900_000M;
// Console.WriteLine($"Real type: {g} {pi} {paymentsTotal}");

// //Big Int
// Int128 int128 = Int128.MaxValue;
// Console.WriteLine($"Int128 max: {int128}");

// /*
//     Numeric literals
// */
// int x = 127;
// long y = 0x7F;
// Console.WriteLine($"Value x:{x} is equal with y:{y}");

// // enchanching readability
// int million = 1_000_000;
// var bin = 0b0010_0110_0011;

// /*
//     Numeric conversions
// */
// // 1. Implicit conversion
// int iA = 100;
// long lA = iA;

// //2. Explicit conversion
// short sA = (short)iA;

// // 3. Float to integral type
// float fConv = 3.14f;
// int i2 = (int)fConv; //explicit
// Console.WriteLine("Float to int conversion: " + i2); //3

// // 4. Integral to float type
// int i3 = 10;
// float f1 = i3; // implicit ok

// /*
//     Arithmetic operators
// */
// int add = 1 + 2;
// int sub = 1 - 2;
// int mul = 2 * 2;
// float div = 1 / 2;
// int remainder = 5 % 2;
// Console.WriteLine($"% operator {remainder}");

// /*
//     Increment and decrement
// */
// int i4 = 0, i5 = 0;

// System.Console.WriteLine(i4++); //post-increment
// System.Console.WriteLine(++i5); //pre-increment


// // Overflow checking
// // checked
// // int i6 = 1000000;
// // int i7 = 1000000;
// // int c = checked(a * b);
// // checked
// // {
// //     c = a * b;
// // }

// // Scientific constants
// double scientificConstant = 10e3;

// System.Console.WriteLine($"Scientific: {scientificConstant}");

#endregion

#region Boolean Type and Operators
// Conversions
// int x = 1;
// int y = 2;
// int z = 1;

// Console.WriteLine(x == y); // Output: False
// Console.WriteLine(x == z); // Output: True

// Reference type
// Dude d1 = new Dude("John");
// Dude d2 = new Dude("John");
// Console.WriteLine(d1 == d2);

// Dude d3 = d1;
// System.Console.WriteLine(d3 == d1);

// static bool UseUmbrella(bool rainy, bool sunny, bool windy)
// {
//     return !windy && (rainy || sunny);
// }

// System.Console.WriteLine(UseUmbrella(true, false, false));

// Null prevent with short circuit
// string? sb = null;

// if(sb != null && sb.Length > 0)
// {
//     Console.WriteLine(sb);
// }

#endregion

#region String and Characters
// Basic string escape sequence
string s = "I think it's somehow \"True\"";
System.Console.WriteLine(s);

string p = "C:\\";
System.Console.WriteLine("Path " + p);

string vTab = "This is horizontal \ttab, and this is vertical \vtab";
System.Console.WriteLine(vTab);

// Char conversion
char c = 'c';
ushort cInt = c;

System.Console.WriteLine("Char conversion: " + c + ' ' + cInt);

// Verbatim
string a1 = "\\\\server\\fileshare\\helloworld.cs";
string a2 = @"\\server\fileshare\helloworld.cs";
System.Console.WriteLine("Verbatim \n" + a1 + '\n' + a2);

// Raw string literal
string raw = """This is raw string "this is quoted" """;
System.Console.WriteLine(raw);

// Format specifier 
float pi = 3.14159f;
decimal price = 12.50M;
int number = 1000000;
float percentage = 0.756f;
int hex = 255;
System.Console.WriteLine($"{pi:F2}, {price:C}, {number:N},{percentage:P1}, {hex:X2}");

#endregion