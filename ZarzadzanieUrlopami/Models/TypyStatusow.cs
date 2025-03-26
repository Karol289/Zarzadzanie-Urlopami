using System;
using System.Collections.Generic;

namespace ZarzadzanieUrlopami.Models;

public partial class TypyStatusow
{
    public int IdTypuStat { get; set; }

    public string Nazwa { get; set; } = null!;

    public string? Opis { get; set; }

    public virtual ICollection<StatusUrlopu> StatusUrlopus { get; set; } = new List<StatusUrlopu>();
}
