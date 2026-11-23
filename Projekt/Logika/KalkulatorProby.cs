namespace WirtualnyPoligonBalistyczny;

public class KalkulatorProby
{
    // Przeciążenie nr 1: przyjmuje gotową płytę.
    public WynikProby ObliczWynik(PociskBaza pocisk, Plyta plyta)
    {
        ArgumentNullException.ThrowIfNull(pocisk);
        ArgumentNullException.ThrowIfNull(plyta);
        plyta.ObliczDrogeWPlycie(); // Walidacja parametrów.
        return new WynikProby
        {
            Data = DateTime.Now,
            Pocisk = pocisk,
            Plyta = new Plyta(plyta.Grubosc, plyta.Kat)
        };
    }

    // Przeciążenie nr 2: przyjmuje liczby i korzysta z pierwszej wersji.
    public WynikProby ObliczWynik(PociskBaza pocisk, double grubosc, double kat)
    {
        return ObliczWynik(pocisk, new Plyta(grubosc, kat));
    }
}
