using System;
using System.Collections.Generic;

namespace ProjSem;

public partial class Bilety
{
    public int IdBiletu { get; set; }

    public string NrRejestracyjny { get; set; } = null!;

    public DateTime DataZakupu { get; set; }

    public DateTime DataWaznosci { get; set; }

    public string RodzajBiletu { get; set; } = null!;

    public virtual Pojazdy NrRejestracyjnyNavigation { get; set; } = null!;

    public virtual RodzajeBiletow RodzajBiletuNavigation { get; set; } = null!;
}
