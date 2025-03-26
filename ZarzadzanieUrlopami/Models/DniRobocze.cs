using System;
using System.Collections.Generic;

namespace ZarzadzanieUrlopami.Models;

public partial class DniRobocze
{
    public int IdDnia { get; set; }

    public int? IdKierownika { get; set; }

    public DateOnly? DataDnia { get; set; }

    public virtual Pracownicy? IdKierownikaNavigation { get; set; }

    public virtual ICollection<Zmiany> Zmianies { get; set; } = new List<Zmiany>();
}
