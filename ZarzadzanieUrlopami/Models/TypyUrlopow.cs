using System;
using System.Collections.Generic;

namespace ZarzadzanieUrlopami.Models;

public partial class TypyUrlopow
{
    public int IdTypuUrlopu { get; set; }

    public string? Nazwa { get; set; }

    public virtual ICollection<DostepneUrlopyRoczne> DostepneUrlopyRocznes { get; set; } = new List<DostepneUrlopyRoczne>();

    public virtual ICollection<Urlopy> Urlopies { get; set; } = new List<Urlopy>();
}
