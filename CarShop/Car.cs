namespace CarShop;

public class Car : Vehicle
{
    public Car(double engineCapacity, string model, int year) : base(engineCapacity, model, year)
    { }

    public override void Start()
    {
        Console.WriteLine("Car started");
    }

    public override void Stop()
    {
        Console.WriteLine("Car stopped");
    }

    public override void Test()
    {
        Console.WriteLine("Car did a test of some kind");
    }
}
