namespace Day3;

public class CustomAdder
{
    public static int Add(int a, int b) => a + b;
    public static float Add(float a, float b) => a + b;
    public static double Add(double a, double b) => a + b;
    public static decimal Add(decimal a, decimal b) => a + b;
    
    public static void Add(int a, int b, out int result) => result = a + b;
    public static void Add(float a, float b, out float result) => result = a + b;
}