// Olga Tomaszewska 31912

// Utwórz tablicę Zwierze[] z obiektami różnych zwierząt i wywołaj DajGlos() w
// pętli foreach.
Zwierze[] animals = [new Pies(), new Kot(), new Zwierze(), new Pies()];
foreach (Zwierze animal in animals)
{
    animal.DajGlos();
}

class Zwierze
{
    public virtual void DajGlos() => Console.WriteLine("Zwierzę wydaje dźwięk");
}
class Pies : Zwierze
{
    public override void DajGlos() => Console.WriteLine("Hau hau!");
}
class Kot : Zwierze
{
    public override void DajGlos() => Console.WriteLine("Miau!");
}
