using System;
using System.Collections.Generic;

namespace ProjSem;

public partial class Pojazdy
{
    public string NrRejestracyjny { get; set; } = null!;

    public virtual ICollection<Bilety> Bileties { get; set; } = new List<Bilety>();

    public virtual ICollection<Historium> Historia { get; set; } = new List<Historium>();
}
