namespace Day3;

public class Wine(decimal price)
{
    private decimal _price = price;
    private int _year;

    public Wine(decimal price, int year) : this(price) => this._year = year;

    public void PrintDetail()
    {
        Console.WriteLine($"Wine price:{_price:C} year:{_year}");
    }

}