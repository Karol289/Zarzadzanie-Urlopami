
namespace ZarzadzanieUrlopami.Models.Kalndarz
{
    public class Dzien
    {
        private DaneDnia? dane = null;


        public Dzien() 
        {
            dane = new DaneDnia();

        }  


        public void dodajRodzaj(string rodzaj)
        {
            dane.rodzaje += " " + rodzaj;
        }

        public void dodajZmiane(List<Zmiany> zmiany)
        {
            foreach(var z in zmiany)
            {
                dane.zmiany.Add(z);
            }

        }

        public void DodajZmiane(Zmiany nowaZmiana)
        {
            if (dane.zmiany == null)
                dane.zmiany = new List<Zmiany>();

            dane.zmiany.Add(nowaZmiana);
        }

        public void EdytujZmiane(int zmianaId, Zmiany noweDane)
        {
            var zmiana = dane.zmiany?.FirstOrDefault(z => z.IdZmiany == zmianaId);
            if (zmiana != null)
            {
                zmiana.GodzRozp = noweDane.GodzRozp;
                zmiana.GodzZakon = noweDane.GodzZakon;
                zmiana.IdDnia = noweDane.IdDnia;
                zmiana.IdPracownika = noweDane.IdPracownika;
            }
        }


        public void dodajUrlop(List<Urlopy> urlopy)
        {
            foreach (var z in urlopy)
            {
                dane.urlopy.Add(z);
            }
        }

        public void dodajZwolnienie(List<ZwolnieniaLekarskie> zwolnienia)
        {
            foreach (var z in zwolnienia)
            {
                dane.zwolnienia.Add(z);
            }
        }

        public void dodajZastepstwo(List<Zastepstwa> zastepstwa)
        {
            foreach (var z in zastepstwa)
            {
                dane.zastepstwa.Add(z);
            }
        }

        public string getRodzaje()
        {
            return dane.rodzaje;
        }

        public List<Zmiany> getZmiany()
        {
            return dane.zmiany;
        }
        public List<Urlopy> getUrlopy()
        {
            return dane.urlopy;
        }
        public List<ZwolnieniaLekarskie> getZwolnienia()
        {
            return dane.zwolnienia;
        }
        public List<Zastepstwa> getZastepstwa()
        {
            return dane.zastepstwa;
        }
    }
}
