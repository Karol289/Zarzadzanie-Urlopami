using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZarzadzanieUrlopami.Models;

public partial class Pracownicy
{
    public int IdPracownika { get; set; }

    [Required(ErrorMessage = "Rola jest wymagana")]
    public int? IdRoli { get; set; }

    [Required(ErrorMessage = "Imię jest wymagane")]
    [StringLength(50, ErrorMessage = "Imię może mieć maksymalnie 50 znaków")]
    public string? Imie { get; set; }

    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    [StringLength(50, ErrorMessage = "Nazwisko może mieć maksymalnie 50 znaków")]
    public string? Nazwisko { get; set; }

    public string? Plec { get; set; }

    public int? Wiek { get; set; }

    [Required(ErrorMessage = "Email jest wymagany")]
    [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu email")]
    public string? Mail { get; set; }

    public int? DzieciPonizej14 { get; set; }

    [Required(ErrorMessage = "Data rozpoczęcia jest wymagana")]
    public DateOnly? DataRozpoczecia { get; set; }

    public int? StazPracyWDniach { get; set; }

    public string? Adres { get; set; }

    [Required(ErrorMessage = "Telefon jest wymagany")]
    [Phone(ErrorMessage = "Nieprawidłowy format numeru telefonu")]
    public string? Telefon { get; set; }

    [Required(ErrorMessage = "Hasło jest wymagane")]
    [MinLength(8, ErrorMessage = "Hasło musi mieć co najmniej 8 znaków")]
    public string? Haslo { get; set; }

    public virtual ICollection<DniRobocze> DniRoboczes { get; set; } = new List<DniRobocze>();

    public virtual ICollection<DostepneUrlopyRoczne> DostepneUrlopyRocznes { get; set; } = new List<DostepneUrlopyRoczne>();

    public virtual Role? IdRoliNavigation { get; set; }

    public virtual ICollection<Urlopy> Urlopies { get; set; } = new List<Urlopy>();

    public virtual ICollection<Zastepstwa> Zastepstwas { get; set; } = new List<Zastepstwa>();

    public virtual ICollection<Zmiany> Zmianies { get; set; } = new List<Zmiany>();

    public virtual ICollection<ZwolnieniaLekarskie> ZwolnieniaLekarskies { get; set; } = new List<ZwolnieniaLekarskie>();
}
