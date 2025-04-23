
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
            dane.rodzaje += rodzaj;
        }

        public void dodajZmiane(List<Zmiany> zmiany)
        {
            foreach(var z in zmiany)
            {
                dane.zmiany.Add(z);
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
    }
}
