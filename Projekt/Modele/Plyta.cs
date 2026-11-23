namespace WirtualnyPoligonBalistyczny;

public class Plyta : ICel
{
    public double Grubosc { get; set; }
    public double Kat { get; set; }

    // Konstruktor bez parametrów jest używany podczas odczytu JSON.
    public Plyta() { }

    public Plyta(double grubosc, double kat)
    {
        Grubosc = grubosc;
        Kat = kat;
    }
    public double ObliczDrogeWPlycie()
    {
        if (!double.IsFinite(Grubosc) || Grubosc <= 0)
            throw new ArgumentException("Grubość musi być dodatnią, skończoną liczbą.");
        if (!double.IsFinite(Kat) || Kat < 0 || Kat > 75)
            throw new ArgumentException("Kąt musi należeć do zakresu 0–75°.");
        double katWRadianach = Kat * Math.PI / 180.0;
        double droga = Grubosc / Math.Cos(katWRadianach);
        if (!double.IsFinite(droga))
            throw new ArgumentException("Podana grubość jest zbyt duża.");
        return droga;
    }
}
