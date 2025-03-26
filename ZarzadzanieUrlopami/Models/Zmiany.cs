using System;
using System.Collections.Generic;

namespace ZarzadzanieUrlopami.Models;

public partial class Zmiany
{
    public int IdZmiany { get; set; }

    public int IdDnia { get; set; }

    public int IdPracownika { get; set; }

    public TimeOnly? GodzRozp { get; set; }

    public TimeOnly? GodzZakon { get; set; }

    public virtual DniRobocze IdDniaNavigation { get; set; } = null!;

    public virtual Pracownicy IdPracownikaNavigation { get; set; } = null!;

    public virtual ICollection<Zastepstwa> Zastepstwas { get; set; } = new List<Zastepstwa>();
}
