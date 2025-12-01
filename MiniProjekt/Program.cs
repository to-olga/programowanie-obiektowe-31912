// Olga Tomaszewska 31912

Pojazd pojazd = new Pojazd();
Console.WriteLine("Pojazd");
pojazd.Start();
Console.WriteLine();

Samochod samochod = new Samochod();
Console.WriteLine("Samochod");
samochod.Start();
samochod.Jedz();
Console.WriteLine();

ElektrycznySamochod elektrycznySamochod = new ElektrycznySamochod();
Console.WriteLine("Elektryczny samochód");
elektrycznySamochod.Start();
elektrycznySamochod.Jedz();
elektrycznySamochod.Laduj();

class Pojazd
{
    public virtual void Start() => Console.WriteLine("Pojazd uruchomiony");
}
class Samochod : Pojazd
{
    public void Jedz() => Console.WriteLine("Samochód jedzie");
}
class ElektrycznySamochod : Samochod
{
    public void Laduj() => Console.WriteLine("Ładowanie baterii...");
}
