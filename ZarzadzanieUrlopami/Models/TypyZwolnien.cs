using System;
using System.Collections.Generic;

namespace ZarzadzanieUrlopami.Models;

public partial class TypyZwolnien
{
    public int IdTypuZwolnienia { get; set; }

    public string Nazwa { get; set; } = null!;

    public string? Opis { get; set; }

    public virtual ICollection<ZwolnieniaLekarskie> ZwolnieniaLekarskies { get; set; } = new List<ZwolnieniaLekarskie>();
}
