using System.Text.Json.Serialization;
namespace WirtualnyPoligonBalistyczny;

public class WynikProby
{
    public DateTime Data { get; set; }

    // Kompozycja: wynik zawiera obiekty pocisku oraz płyty.
    public PociskBaza Pocisk { get; set; } = null!;
    public Plyta Plyta { get; set; } = null!;

    // Wyniki wyliczamy z danych, aby nie zapisywać sprzecznych wartości.
    [JsonIgnore]
    public double DrogaWPlycie => Plyta.ObliczDrogeWPlycie();
    [JsonIgnore]
    public bool CzyPrzebicie => Pocisk.CzyPrzebija(Plyta);
    [JsonIgnore]
    public string OpisWyniku => CzyPrzebicie ? "PRZEBICIE" : "ZATRZYMANIE";
}
