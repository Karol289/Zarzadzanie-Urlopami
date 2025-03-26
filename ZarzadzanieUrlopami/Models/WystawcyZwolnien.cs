using System;
using System.Collections.Generic;

namespace ZarzadzanieUrlopami.Models;

public partial class WystawcyZwolnien
{
    public int IdWystawcy { get; set; }

    public string? Nazwa { get; set; }

    public string? Adres { get; set; }

    public virtual ICollection<ZwolnieniaLekarskie> ZwolnieniaLekarskies { get; set; } = new List<ZwolnieniaLekarskie>();
}
