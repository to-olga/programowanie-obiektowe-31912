namespace CarShop;

public abstract class Vehicle
{
    public double EngineCapacity
    {
        get;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            field = value;
        }
    }
    public string Model
    {
        get;
        set
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(value);
            field = value;
        }
    }
    public int Year
    {
        get;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
            field = value;
        }
    }

    public Vehicle(double engineCapacity, string model, int year)
    {
        EngineCapacity = engineCapacity;
        Model = model;
        Year = year;
    }

    public virtual void Start()
    {
        Console.WriteLine("Vehicle Started");
    }

    public virtual void Stop()
    {
        Console.WriteLine("Vehicle Stopped");
    }

    public void PrintMe()
    {
        Console.Write($"Model: {Model}, Year: {Year}, Engine Capacity: {EngineCapacity}");
    }

    abstract public void Test();
}
