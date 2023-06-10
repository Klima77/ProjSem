using System;
using System.Collections.Generic;

namespace ProjSem;

public partial class RodzajeBiletow
{
    public string Nazwa { get; set; } = null!;

    public int CzasParkowania { get; set; }

    public virtual ICollection<Bilety> Bileties { get; set; } = new List<Bilety>();
}
