using System;
using System.Collections.Generic;

namespace ZarzadzanieUrlopami.Models;

public partial class Role
{
    public int IdRoli { get; set; }

    public string? Nazwa { get; set; }

    public virtual ICollection<Pracownicy> Pracownicies { get; set; } = new List<Pracownicy>();
}
