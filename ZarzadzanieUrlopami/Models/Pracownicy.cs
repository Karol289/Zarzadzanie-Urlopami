using System;
using System.Collections.Generic;

namespace ZarzadzanieUrlopami.Models;

public partial class Pracownicy
{
    public int IdPracownika { get; set; }

    public int? IdRoli { get; set; }

    public string? Imie { get; set; }

    public string? Nazwisko { get; set; }

    public string? Plec { get; set; }

    public int? Wiek { get; set; }

    public string? Mail { get; set; }

    public int? DzieciPonizej14 { get; set; }

    public DateOnly? DataRozpoczecia { get; set; }

    public int? StazPracyWDniach { get; set; }

    public string? Adres { get; set; }

    public string? Telefon { get; set; }

    public string? Haslo { get; set; }

    public virtual ICollection<DniRobocze> DniRoboczes { get; set; } = new List<DniRobocze>();

    public virtual ICollection<DostepneUrlopyRoczne> DostepneUrlopyRocznes { get; set; } = new List<DostepneUrlopyRoczne>();

    public virtual Role? IdRoliNavigation { get; set; }

    public virtual ICollection<Urlopy> Urlopies { get; set; } = new List<Urlopy>();

    public virtual ICollection<Zastepstwa> Zastepstwas { get; set; } = new List<Zastepstwa>();

    public virtual ICollection<Zmiany> Zmianies { get; set; } = new List<Zmiany>();

    public virtual ICollection<ZwolnieniaLekarskie> ZwolnieniaLekarskies { get; set; } = new List<ZwolnieniaLekarskie>();
}
