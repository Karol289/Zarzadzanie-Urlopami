namespace ZarzadzanieUrlopami.Models;

public partial class StatusUrlopu
{
    public const int PENDING = 3;
    public const int ACCEPTED = 1;

    public int IdStatusu { get; set; }

    public int? IdTypuStatusu { get; set; }

    public string? Wyjaśnienie { get; set; }

    public virtual TypyStatusow? IdTypuStatusuNavigation { get; set; }

    public virtual ICollection<Urlopy> Urlopies { get; set; } = new List<Urlopy>();

}
