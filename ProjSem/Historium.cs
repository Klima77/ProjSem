using System;
using System.Collections.Generic;

namespace ProjSem;

public partial class Historium
{
    public int IdHistorii { get; set; }

    public string NrRejestracyjny { get; set; } = null!;

    public DateTime DataRejestracji { get; set; }

    public virtual Pojazdy NrRejestracyjnyNavigation { get; set; } = null!;
}