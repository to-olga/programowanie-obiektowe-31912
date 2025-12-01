// Olga Tomaszewska 31912

// Dodaj metodę Wyplata(double kwota), która wypłaca pieniądze tylko
// jeśli saldo jest wystarczające.

KontoBankowe konto = new KontoBankowe();
Console.WriteLine($"Stan konta: {konto.PobierzSaldo()}");
konto.Wplata(100);
Console.WriteLine($"Stan konta: {konto.PobierzSaldo()}");
konto.Wyplata(50);
Console.WriteLine($"Stan konta: {konto.PobierzSaldo()}");
konto.Wyplata(60);
Console.WriteLine($"Stan konta: {konto.PobierzSaldo()}");

class KontoBankowe
{
    private double saldo;
    public void Wplata(double kwota) { saldo += kwota; }
    public double PobierzSaldo() { return saldo; }
    public void Wyplata(double kwota)
    {
        if (kwota <= saldo)
        {
            saldo -= kwota;
        }
        else
        {
            Console.WriteLine("Nie masz tyle pieniędzy :(");
        }
    }
}
