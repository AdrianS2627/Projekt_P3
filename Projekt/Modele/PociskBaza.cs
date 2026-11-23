using System.Text.Json.Serialization;
namespace WirtualnyPoligonBalistyczny;

// Znacznik w JSON pozwala odtworzyć konkretną klasę pocisku.
[JsonPolymorphic(TypeDiscriminatorPropertyName = "TypPocisku")]
[JsonDerivedType(typeof(PociskAP), "AP")]
[JsonDerivedType(typeof(PociskHEAT), "HEAT")]
[JsonDerivedType(typeof(PociskAPFSDS), "APFSDS")]
public abstract class PociskBaza
{
    public abstract string Nazwa { get; }
    public abstract double Penetracja { get; }

    // Parametr typu interfejsowego – dowolny cel spełniający ICel.
    public bool CzyPrzebija(ICel cel)
    {
        return Penetracja >= cel.ObliczDrogeWPlycie();
    }
}
