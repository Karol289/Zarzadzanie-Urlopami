using System;
using System.Collections.Generic;

namespace ZarzadzanieUrlopami.Models;

public partial class Urlopy
{
    public int IdUrlopu { get; set; }

    public int IdPracownika { get; set; }

    public int IdTypuUrlopu { get; set; }

    public int IdStatusu { get; set; }

    public DateOnly? DataPocz { get; set; }

    public DateOnly? DataKon { get; set; }

    public virtual Pracownicy IdPracownikaNavigation { get; set; } = null!;

    public virtual StatusUrlopu IdStatusuNavigation { get; set; } = null!;

    public virtual TypyUrlopow IdTypuUrlopuNavigation { get; set; } = null!;
}
