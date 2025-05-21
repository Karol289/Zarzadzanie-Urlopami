using Microsoft.EntityFrameworkCore;
using ZarzadzanieUrlopami.Data;
using ZarzadzanieUrlopami.Models;

namespace ZarzadzanieUrlopami.Service
{
    public class PodaniaService
    {
        private readonly UrlopyDbContext _context;

        public PodaniaService(UrlopyDbContext context)
        {
            _context = context;
        }

        public async Task<List<TypyUrlopow>> GetTypyUrlopowAsync()
        {
            return await _context.TypyUrlopows.ToListAsync();
        }

        public async Task<Pracownicy?> GetDostepneUrlopy(string mail)
        {
            return await _context.Pracownicies
                .Where(x => x.Mail == mail)
                .Include(p => p.DostepneUrlopyRocznes)
                    .ThenInclude(dup => dup.IdTypuUrlopuNavigation)
                .Include(p => p.Urlopies)
                .Include(p => p.ZwolnieniaLekarskies)
                    .ThenInclude(zl => zl.IdTypuNavigation)
                .FirstOrDefaultAsync();
        }

        public async Task DodajUrlop(Urlopy urlop)
        {
            _context.Urlopies.Add(urlop);
            _context.SaveChanges();
        }

        public async Task<List<Urlopy>> GetPendingLeavesAsync()
        {
            return await _context.Urlopies
                .Include(u => u.IdPracownikaNavigation)
                .Include(u => u.IdTypuUrlopuNavigation)
                .Include(u => u.IdStatusuNavigation)
                    .ThenInclude(s => s.IdTypuStatusuNavigation)
                .Where(u => u.IdStatusuNavigation.IdTypuStatusu == StatusUrlopu.PENDING) // 3 - Oczekujący
                .ToListAsync();
        }

        public async Task UpdateLeaveStatusAsync(int leaveId, int statusTypeId, string explanation)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var urlop = await _context.Urlopies
                    .Include(u => u.IdStatusuNavigation)
                    .Include(u => u.IdPracownikaNavigation)
                        .ThenInclude(p => p.DostepneUrlopyRocznes)
                    .Include(u => u.IdTypuUrlopuNavigation)
                    .FirstOrDefaultAsync(u => u.IdUrlopu == leaveId);

                if (urlop == null)
                    throw new Exception("Nie znaleziono urlopu o podanym identyfikatorze.");

                urlop.IdStatusuNavigation.IdTypuStatusu = statusTypeId;
                if (!string.IsNullOrEmpty(explanation))
                {
                    urlop.IdStatusuNavigation.Wyjaśnienie += " | " + explanation;
                }

                if (statusTypeId == StatusUrlopu.ACCEPTED) // 1 - Zatwierdzony
                {
                    int liczbaDni = CalculateLeaveDays(urlop.DataPocz!.Value, urlop.DataKon!.Value);

                    var dostepneUrlopy = urlop.IdPracownikaNavigation?.DostepneUrlopyRocznes
                        .FirstOrDefault(d => d.IdTypuUrlopu == urlop.IdTypuUrlopu &&
                                            d.Rok == urlop.DataPocz?.Year);

                    if (dostepneUrlopy != null)
                    {
                        if (dostepneUrlopy.Ilosc < liczbaDni)
                            throw new Exception($"Nie wystarczająca liczba dni urlopowych. Dostępne: {dostepneUrlopy.Ilosc}, Wymagane: {liczbaDni}");

                        dostepneUrlopy.Ilosc -= liczbaDni;
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private int CalculateLeaveDays(DateOnly startDate, DateOnly endDate)
        {
            return (endDate.DayNumber - startDate.DayNumber) + 1;
        }
    }
}