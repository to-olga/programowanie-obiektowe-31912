// Olga Tomaszewska 31912

// Utwórz klasę Osoba z polami Imie, Wiek i metodą PrzedstawSie().
// Utwórz kilka obiektów i wywołaj tę metodę.

Osoba olga = new Osoba
{
    Imie = "Olga",
    Wiek = 30
};

Osoba dexter = new Osoba
{
    Imie = "Dexter",
    Wiek = 5
};

Osoba maciej = new Osoba
{
    Imie = "Maciej",
    Wiek = 30
};

olga.PrzedstawSie();
dexter.PrzedstawSie();
maciej.PrzedstawSie();

class Osoba
{
    public string Imie;
    public int Wiek;
    public void PrzedstawSie()
    {
        Console.WriteLine($"Cześć, jestem {Imie} i mam {Wiek} lat.");
    }
}