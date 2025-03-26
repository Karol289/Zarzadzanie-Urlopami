using System;
using System.Collections.Generic;

namespace ZarzadzanieUrlopami.Models;

public partial class StatusUrlopu
{
    public int IdStatusu { get; set; }

    public int? IdTypuStatusu { get; set; }

    public string? Wyjaśnienie { get; set; }

    public virtual TypyStatusow? IdTypuStatusuNavigation { get; set; }

    public virtual ICollection<Urlopy> Urlopies { get; set; } = new List<Urlopy>();
}
