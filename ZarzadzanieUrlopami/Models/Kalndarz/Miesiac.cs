
using ZarzadzanieUrlopami.Data;


namespace ZarzadzanieUrlopami.Models.Kalndarz
{
    public class Miesiac
    {

        private Dictionary<int, Dzien>? miesiac = null;
        private readonly object miesiacLock = new();

        private string mailPracownika;
        private int miesiacKalendarza;
        private int rokKalendarza;

        private readonly IServiceProvider _provider;

        //to delete
        Pracownicy? pracownik = null;
        private readonly object pracownikLock = new();


        public Miesiac(IServiceProvider provider, string mail, int miesiac, int rok)
        {

            mailPracownika = mail;
            this.miesiacKalendarza = miesiac;
            this.rokKalendarza = rok;

            _provider = provider;
        }

        public Dzien getDzien(int dzien)
        {
            return this.miesiac[dzien];
        }

        public void nastepnyMiesiac()
        {
            miesiacKalendarza += 1;
            if(miesiacKalendarza > 12)
            {
                miesiacKalendarza = 1;
                rokKalendarza += 1;
            }
            
        }

        public void poprzedniMiesiac()
        {
            miesiacKalendarza -= 1;
            if (miesiacKalendarza < 1)
            {
                miesiacKalendarza = 12;
                rokKalendarza -= 1;
            }

        }

        public async Task generujMiesiac()
        {
            this.miesiac = new Dictionary<int, Dzien>();

            if(miesiac == null || mailPracownika == null || miesiacKalendarza == null || rokKalendarza == null)
                return;


            generujDni();

            var t2 = generujZmiany();
            var t3 = generujUrlopy();
            var t4 = generujZwolnienia();
            var t5 = generujZastepstwa();
            var t6 = generujDzisiaj();

            await Task.WhenAll(t2, t3, t4, t5, t6);


        }


        private async Task generujDni()
        {
            int iloscDni = DateTime.DaysInMonth(rokKalendarza, miesiacKalendarza);

            for (int i = 1; i <= iloscDni; i++)
            {
                miesiac.Add(i, new Dzien());
            }

        }

        private async Task generujDzisiaj()
        {
            var dzisiaj = DateOnly.FromDateTime(DateTime.Now);

            lock (miesiacLock)
            {
                if(miesiacKalendarza == dzisiaj.Month && rokKalendarza == dzisiaj.Year)
                    miesiac[dzisiaj.Day].dodajRodzaj("dzisiaj");
            }


        }

        private async Task generujZmiany()
        {
            Dictionary<int, string> tempRodzaje = new Dictionary<int, string>();
            Dictionary<int, List<Zmiany>?> tempZmiany = new Dictionary<int, List<Zmiany>?>();

            Pracownicy? zmiany = null;

            using (var scope = _provider.CreateScope())
            {
                var scheduleService = scope.ServiceProvider.GetRequiredService<ScheduleService>();

                zmiany = await scheduleService.GetZmiany(mailPracownika, miesiacKalendarza, rokKalendarza);
            }

            foreach (var a in zmiany.Zmianies)
            {
                var data = a.IdDniaNavigation?.DataDnia;

                if (data.HasValue && data.Value.Month == miesiacKalendarza && data.Value.Year == rokKalendarza)
                {
                    int day = data.Value.Day;

                    if (!tempRodzaje.ContainsKey(day))
                        tempZmiany[day] = new List<Zmiany>();

                    tempZmiany[day].Add(a);

                    tempRodzaje[day] = "zmiana";
                }
            }

            lock (miesiacLock)
            {
                foreach (var item in tempRodzaje)
                {
                    miesiac[item.Key].dodajRodzaj(item.Value);
                }

                foreach (var item in tempZmiany)
                {
                    miesiac[item.Key].dodajZmiane(item.Value);
                }
            }
        }

        private async Task generujUrlopy()
        {
            Dictionary<int, string> tempRodzaje = new Dictionary<int, string>();
            Dictionary<int, List<Urlopy>?> tempUrlopy = new Dictionary<int, List<Urlopy>?>();

            Pracownicy? urlopy;
            using (var scope = _provider.CreateScope())
            {
                var scheduleService = scope.ServiceProvider.GetRequiredService<ScheduleService>();

                urlopy = await scheduleService.GetUrlopy(mailPracownika, miesiacKalendarza, rokKalendarza);
            }



            foreach (var a in urlopy.Urlopies)
            {
                DateOnly start = a.DataPocz.GetValueOrDefault();

                if (start < new DateOnly(rokKalendarza, miesiacKalendarza, 1))
                    start = new DateOnly(rokKalendarza, miesiacKalendarza, 1);

                DateOnly end  = a.DataKon.GetValueOrDefault();

                if (end > new DateOnly(rokKalendarza, miesiacKalendarza, DateTime.DaysInMonth(rokKalendarza, miesiacKalendarza)))
                    end = new DateOnly(rokKalendarza, miesiacKalendarza, DateTime.DaysInMonth(rokKalendarza, miesiacKalendarza));


                for(var i = start; i <= end; i = i.AddDays(1))
                {
                    int day = i.Day;

                    if (!tempRodzaje.ContainsKey(day))
                        tempUrlopy[day] = new List<Urlopy>();

                    tempRodzaje[day] = "urlop";



                    tempUrlopy[day].Add(a);

                }
            }

            lock (miesiacLock)
            {
                foreach (var item in tempRodzaje)
                {
                    miesiac[item.Key].dodajRodzaj(item.Value);
                }
                foreach (var item in tempUrlopy)
                {
                    miesiac[item.Key].dodajUrlop(item.Value);
                }
            }

        }

        private async Task generujZwolnienia()
        {
            Dictionary<int, string> tempRodzaje = new Dictionary<int, string>();
            Dictionary<int, List<ZwolnieniaLekarskie>?> tempZwolnienia = new Dictionary<int, List<ZwolnieniaLekarskie>?>();

            Pracownicy? zwolnienia;
            using (var scope = _provider.CreateScope())
            {
                var scheduleService = scope.ServiceProvider.GetRequiredService<ScheduleService>();

                zwolnienia = await scheduleService.GetZwolnienia(mailPracownika, miesiacKalendarza, rokKalendarza);
            }

            foreach (var a in zwolnienia.ZwolnieniaLekarskies)
            {
                DateOnly start = a.DataPocz.GetValueOrDefault();

                if (start < new DateOnly(rokKalendarza, miesiacKalendarza, 1))
                    start = new DateOnly(rokKalendarza, miesiacKalendarza, 1);

                DateOnly end = a.DataKon.GetValueOrDefault();

                if (end > new DateOnly(rokKalendarza, miesiacKalendarza, DateTime.DaysInMonth(rokKalendarza, miesiacKalendarza)))
                    start = new DateOnly(rokKalendarza, miesiacKalendarza, 1);


                for (var i = start; i <= end; i = i.AddDays(1))
                {
                    int day = i.Day;


                    if (!tempRodzaje.ContainsKey(day))
                        tempZwolnienia[day] = new List<ZwolnieniaLekarskie>();

                    tempRodzaje[day] = "zwolnienie";


                    tempZwolnienia[day].Add(a);

                }
            }

            lock (miesiacLock)
            {
                foreach (var item in tempRodzaje)
                {
                    miesiac[item.Key].dodajRodzaj(item.Value);
                }
                foreach (var item in tempZwolnienia)
                {
                    miesiac[item.Key].dodajZwolnienie(item.Value);
                }
            }

        }


        private async Task generujZastepstwa()
        {
            Dictionary<int, string> tempRodzaje = new Dictionary<int, string>();
            Dictionary<int, List<Zastepstwa>?> tempZastepstwa = new Dictionary<int, List<Zastepstwa>?>();

            List<Zastepstwa>? zmiany = null;

            using (var scope = _provider.CreateScope())
            {
                var scheduleService = scope.ServiceProvider.GetRequiredService<ScheduleService>();

                zmiany = await scheduleService.GetZastepstwa(mailPracownika, miesiacKalendarza, rokKalendarza);
            }

            foreach (var a in zmiany)
            {
                var data = a.IdZmianyNavigation.IdDniaNavigation.DataDnia;

                if (data.HasValue && data.Value.Month == miesiacKalendarza && data.Value.Year == rokKalendarza)
                {
                    int day = data.Value.Day;

                    if (!tempRodzaje.ContainsKey(day))
                        tempZastepstwa[day] = new List<Zastepstwa>();

                    tempZastepstwa[day].Add(a);

                    tempRodzaje[day] = "zastepstwo";
                }
            }

            lock (miesiacLock)
            {
                foreach (var item in tempRodzaje)
                {
                    miesiac[item.Key].dodajRodzaj(item.Value);
                }

                foreach (var item in tempZastepstwa)
                {
                    miesiac[item.Key].dodajZastepstwo(item.Value);
                }
            }
        }
    }

    
}
