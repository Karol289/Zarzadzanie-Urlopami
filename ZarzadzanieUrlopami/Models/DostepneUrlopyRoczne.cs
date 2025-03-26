using System;
using System.Collections.Generic;

namespace ZarzadzanieUrlopami.Models;

public partial class DostepneUrlopyRoczne
{
    public int IdDostepnegoUrlopu { get; set; }

    public int? IdPracownika { get; set; }

    public int? IdTypuUrlopu { get; set; }

    public int? Ilosc { get; set; }

    public int? Rok { get; set; }

    public virtual Pracownicy? IdPracownikaNavigation { get; set; }

    public virtual TypyUrlopow? IdTypuUrlopuNavigation { get; set; }
}
