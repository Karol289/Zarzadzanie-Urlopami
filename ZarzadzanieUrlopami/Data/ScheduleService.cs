using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using ZarzadzanieUrlopami.Models;

namespace ZarzadzanieUrlopami.Data
{
    public class ScheduleService
    {

        private readonly UrlopyDbContext _context;

        public ScheduleService(UrlopyDbContext context)
        {
            _context = context;
        }


        public async Task<List<TypyStatusow>> getTypyStatusow()
        {
            return await _context.TypyStatusows.ToListAsync();
        }

        public async Task<List<Zmiany>> getZmianyPracownikaByEmailAndMonth(string mail, int month)
        {
            return await _context.Zmianies.
                Include(x => x.IdPracownikaNavigation).
                Include(x => x.IdDniaNavigation).
                Where(x => x.IdPracownikaNavigation.Mail == mail).
                Where(x => x.IdDniaNavigation.DataDnia.Value.Month == month).
                ToListAsync();
        }

        public async Task<List<Urlopy>> getUrlopyPracownikaByEmailAndMonth(string mail)
        {
            return await _context.Urlopies.
                Include(x => x.IdTypuUrlopuNavigation). 
                Include(x => x.IdStatusuNavigation.IdTypuStatusuNavigation).
                Include(x => x.IdPracownikaNavigation).
                Where(x=> x.IdPracownikaNavigation.Mail == mail).
               // Where(x => x.DataPocz.Value.Month == month).
               // Where(x => x.DataKon.Value.Month == month).
                ToListAsync();
        }

        public async Task<List<Zmiany>> getZmianaByDate(DateOnly data)
        {
            return await _context.Zmianies.
                Include(x => x.IdDniaNavigation).
                Where(x => x.IdDniaNavigation.DataDnia == data).
                Include(x => x.Zastepstwas).ToListAsync();
                
        }

        public async Task<Pracownicy?> GetScheduleOfWorkerForMonth(string mail, int month, int year)
        {
            DateOnly start = new DateOnly(year, month, 1);
            DateOnly end = new DateOnly(year, month, DateTime.DaysInMonth(year, month));

            return await _context.Pracownicies
                .Where(x => x.Mail == mail)
                .Include(x => x.Zmianies
                    .Where(z =>
                        z.IdDniaNavigation.DataDnia.HasValue &&
                        z.IdDniaNavigation.DataDnia.Value.Month == month &&
                        z.IdDniaNavigation.DataDnia.Value.Year == year))
                .Include(x => x.Zmianies)
                    .ThenInclude(z => z.IdDniaNavigation)
                .Include(x => x.Zastepstwas)
                    .ThenInclude(z => z.IdZmianyNavigation)
                        .ThenInclude(c => c.IdDniaNavigation)
                .Include(x => x.Urlopies
                    .Where(u =>
                        u.DataPocz <= end && u.DataKon >= start))
                .Include(x => x.ZwolnieniaLekarskies
                    .Where(z =>
                        z.DataPocz <= end && z.DataKon >= start))
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Pracownicy?> GetZmiany(string mail, int month, int year)
        {
            DateOnly start = new DateOnly(year, month, 1);
            DateOnly end = new DateOnly(year, month, DateTime.DaysInMonth(year, month));

            return await _context.Pracownicies
                .Where(x => x.Mail == mail)
                .Include (x => x.Zmianies
                    .Where(
                        z => z.IdDniaNavigation.DataDnia.HasValue &&
                        z.IdDniaNavigation.DataDnia >= start &&
                        z.IdDniaNavigation.DataDnia <= end)
                    ).ThenInclude(z => z.IdDniaNavigation)
                    .ThenInclude(d => d.IdKierownikaNavigation)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Pracownicy?> GetUrlopy(string mail, int month, int year)
        {
            DateOnly start = new DateOnly(year, month, 1);
            DateOnly end = new DateOnly(year, month, DateTime.DaysInMonth(year, month));

            return await _context.Pracownicies
                .Where(x => x.Mail == mail)
                .Include(x => x.Urlopies.Where(
                    u => u.DataKon.HasValue &&
                         u.DataKon >= start &&
                         u.DataPocz.HasValue &&
                         u.DataPocz <= end)
                )
                .ThenInclude(u => u.IdTypuUrlopuNavigation)
                .Include(x => x.Urlopies)  
                    .ThenInclude(u => u.IdStatusuNavigation) 
                        .ThenInclude(s => s.IdTypuStatusuNavigation)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Pracownicy?> GetZwolnienia(string mail, int month, int year)
        {
            DateOnly start = new DateOnly(year, month, 1);
            DateOnly end = new DateOnly(year, month, DateTime.DaysInMonth(year, month));

            return await _context.Pracownicies
                .Where(x => x.Mail == mail)
                .Include(x => x.ZwolnieniaLekarskies.Where(
                    u => u.DataKon.HasValue &&
                         u.DataKon >= start &&
                         u.DataPocz.HasValue &&
                         u.DataPocz <= end)
                ).ThenInclude(z => z.IdWystawcyNavigation)
                .Include(x => x.ZwolnieniaLekarskies)
                    .ThenInclude(z => z.IdTypuNavigation)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<List<Zastepstwa>?> GetZastepstwa(string mail, int month, int year)
        {
            DateOnly start = new DateOnly(year, month, 1);
            DateOnly end = new DateOnly(year, month, DateTime.DaysInMonth(year, month));

            return await _context.Zastepstwas
                .Include(X => X.IdPracownikaNavigation)
                .Where(x => x.IdPracownikaNavigation.Mail == mail)
                .Include(x => x.IdZmianyNavigation)
                .ThenInclude(z => z.IdDniaNavigation)
                .Where(z => z.IdZmianyNavigation.IdDniaNavigation.DataDnia.HasValue &&
                    z.IdZmianyNavigation.IdDniaNavigation.DataDnia >= start &&
                    z.IdZmianyNavigation.IdDniaNavigation.DataDnia <= end)
                .ToListAsync();
        }


    }

    
}
