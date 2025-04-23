
using ZarzadzanieUrlopami.Models;

namespace ZarzadzanieUrlopami.Models.Kalndarz
{
    public class DaneDnia
    {
        public string rodzaje { get; set; } = "";
        public List<Zmiany>? zmiany { get; set; } = null;
        public List<Urlopy>? urlopy { get; set; } = null;
        public List<ZwolnieniaLekarskie>? zwolnienia { get; set; } = null;
        public List<Zastepstwa>? zastepstwa { get; set; } = null;

        public DaneDnia()
        {
            zmiany = new List<Zmiany>();
            urlopy = new List<Urlopy>();
            zwolnienia = new List<ZwolnieniaLekarskie>();
            zastepstwa = new List<Zastepstwa>();
        }

    }
}
