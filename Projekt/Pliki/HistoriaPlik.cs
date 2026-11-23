using System.IO;
using System.Text.Json;
namespace WirtualnyPoligonBalistyczny;

// Ta klasa odpowiada tylko za zapis i odczyt historii.
public class HistoriaPlik
{
    public void Zapisz(string sciezka, List<WynikProby> historia)
    {
        var opcje = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(historia, opcje);
        File.WriteAllText(sciezka, json);
    }
    public List<WynikProby> Wczytaj(string sciezka)
    {
        string json = File.ReadAllText(sciezka);
        var historia = JsonSerializer.Deserialize<List<WynikProby>>(json);
        if (historia == null)
            throw new InvalidDataException("Plik nie zawiera listy prób.");
        foreach (var wynik in historia)
        {
            if (wynik == null || wynik.Pocisk == null || wynik.Plyta == null || wynik.Data == default)
                throw new InvalidDataException("Historia zawiera niepełną próbę.");
            wynik.Plyta.ObliczDrogeWPlycie();
        }
        return historia;
    }
}
