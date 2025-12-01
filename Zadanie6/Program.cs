// Olga Tomaszewska 31912

// Dodaj klasę Kot, która również dziedziczy po Zwierze i ma metodę Miaucz().

Zwierze zwierzak = new Zwierze();
Pies dexter = new Pies();
Kot kotek = new Kot();

zwierzak.Jedz();
dexter.Jedz();
dexter.Szczekaj();
kotek.Jedz();
kotek.Miaucz();

class Zwierze
{
    public void Jedz() => Console.WriteLine("Zwierzę je");
}
class Pies : Zwierze
{
    public void Szczekaj() => Console.WriteLine("Hau hau!");
}

class Kot : Zwierze
{
    public void Miaucz() => Console.WriteLine("Miau Miau!");
}