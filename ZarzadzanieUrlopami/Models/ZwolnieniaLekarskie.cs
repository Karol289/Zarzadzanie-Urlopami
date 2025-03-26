using System;
using System.Collections.Generic;

namespace ZarzadzanieUrlopami.Models;

public partial class ZwolnieniaLekarskie
{
    public int IdZwolnienia { get; set; }

    public int IdPracownika { get; set; }

    public int IdWystawcy { get; set; }

    public int IdTypu { get; set; }

    public DateOnly? DataPocz { get; set; }

    public DateOnly? DataKon { get; set; }

    public string? Opis { get; set; }

    public virtual Pracownicy IdPracownikaNavigation { get; set; } = null!;

    public virtual TypyZwolnien IdTypuNavigation { get; set; } = null!;

    public virtual WystawcyZwolnien IdWystawcyNavigation { get; set; } = null!;
}
