// Olga Tomaszewska 31912

double number = 0;
string numer = "";
do
{
    Console.Write("Podaj liczbę większą od zera: ");
    numer = Console.ReadLine();
    double.TryParse(numer, out number);
}
while (number <= 0);
Console.WriteLine("Dobra robota!");
