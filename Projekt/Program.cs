using System.Globalization;
using System.Text;
namespace WirtualnyPoligonBalistyczny;

internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("pl-PL");
        List<WynikProby> historia = new List<WynikProby>();
        KalkulatorProby kalkulator = new KalkulatorProby();
        HistoriaPlik historiaPlik = new HistoriaPlik();
        const string nazwaPliku = "historia.json";
        while (true)
        {
            Console.WriteLine("\n=== WIRTUALNY POLIGON BALISTYCZNY ===");
            Console.WriteLine("1. Nowa próba");
            Console.WriteLine("2. Pokaż historię");
            Console.WriteLine("3. Zapisz historię do JSON");
            Console.WriteLine("4. Wczytaj historię z JSON");
            Console.WriteLine("0. Zakończ");
            Console.Write("Wybór: ");
            string? wybor = Console.ReadLine();
            if (wybor == "0")
                break;
            else if (wybor == "1")
                NowaProba(historia, kalkulator);
            else if (wybor == "2")
                PokazHistorie(historia);
            else if (wybor == "3")
            {
                try
                {
                    historiaPlik.Zapisz(nazwaPliku, historia);
                    Console.WriteLine("Zapisano historię do pliku " + nazwaPliku);
                }
                catch (Exception blad)
                {
                    Console.WriteLine("Nie udało się zapisać historii: " + blad.Message);
                }
            }
            else if (wybor == "4")
            {
                try
                {
                    // W razie błędu wczytywania dotychczasowa historia zostaje.
                    List<WynikProby> wczytana = historiaPlik.Wczytaj(nazwaPliku);
                    historia = wczytana;
                    Console.WriteLine("Wczytano prób: " + historia.Count);
                }
                catch (Exception blad)
                {
                    Console.WriteLine("Nie udało się wczytać historii: " + blad.Message);
                }
            }
            else
                Console.WriteLine("Nie ma takiej opcji.");
        }
    }
    static void NowaProba(List<WynikProby> historia, KalkulatorProby kalkulator)
    {
        Console.WriteLine("\nWybierz pocisk:");
        Console.WriteLine("1. AP (120 mm)");
        Console.WriteLine("2. HEAT (320 mm)");
        Console.WriteLine("3. APFSDS (480 mm)");
        Console.Write("Wybór: ");
        string? wybor = Console.ReadLine();
        PociskBaza pocisk;
        if (wybor == "1")
            pocisk = new PociskAP();
        else if (wybor == "2")
            pocisk = new PociskHEAT();
        else if (wybor == "3")
            pocisk = new PociskAPFSDS();
        else
        {
            Console.WriteLine("Nieprawidłowy rodzaj pocisku.");
            return;
        }
        Console.Write("Grubość płyty w mm (np. 100 lub 100,5): ");
        string tekstGrubosci = Console.ReadLine() ?? "";
        if (!double.TryParse(tekstGrubosci, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("pl-PL"), out double grubosc)
            || !double.IsFinite(grubosc) || grubosc <= 0)
        {
            Console.WriteLine("Grubość musi być dodatnią liczbą (użyj przecinka).");
            return;
        }
        Console.Write("Kąt trafienia w stopniach (0–75): ");
        string tekstKata = Console.ReadLine() ?? "";
        if (!double.TryParse(tekstKata, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("pl-PL"), out double kat)
            || !double.IsFinite(kat) || kat < 0 || kat > 75)
        {
            Console.WriteLine("Kąt musi należeć do zakresu 0–75 stopni.");
            return;
        }
        try
        {
            WynikProby wynik = kalkulator.ObliczWynik(pocisk, grubosc, kat);
            historia.Add(wynik);
            Console.WriteLine("Wynik: " + wynik.OpisWyniku);
            Console.WriteLine($"Penetracja: {pocisk.Penetracja:F2} mm");
            Console.WriteLine($"Droga w płycie: {wynik.DrogaWPlycie:F2} mm");
        }
        catch (ArgumentException blad)
        {
            Console.WriteLine("Błąd parametrów: " + blad.Message);
        }
    }
    static void PokazHistorie(List<WynikProby> historia)
    {
        if (historia.Count == 0)
        {
            Console.WriteLine("Historia jest pusta.");
            return;
        }
        Console.WriteLine("\n=== HISTORIA PRÓB ===");
        for (int i = 0; i < historia.Count; i++)
        {
            WynikProby wynik = historia[i];
            Console.WriteLine($"{i + 1}. {wynik.Data:dd.MM.yyyy HH:mm} | " +
                $"{wynik.Pocisk.Nazwa} | {wynik.Plyta.Grubosc:F2} mm | " +
                $"{wynik.Plyta.Kat:F0}° | {wynik.OpisWyniku}");
        }
    }
}
