using System;
using System.Collections.Generic;

namespace ZarzadzanieUrlopami.Models;

public partial class Zastepstwa
{
    public int IdZastepstwa { get; set; }

    public int IdZmiany { get; set; }

    public int? IdPracownika { get; set; }

    public virtual Pracownicy? IdPracownikaNavigation { get; set; }

    public virtual Zmiany IdZmianyNavigation { get; set; } = null!;
}
