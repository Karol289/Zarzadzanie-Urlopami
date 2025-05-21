
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

    }

}