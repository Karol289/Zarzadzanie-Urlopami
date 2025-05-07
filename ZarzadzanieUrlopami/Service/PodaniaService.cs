
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

        public async Task<List<DostepneUrlopyRoczne>> GetDostepneUrlopy(string mail)
        {
            return await _context.DostepneUrlopyRocznes
                .Include(x => x.IdPracownikaNavigation)
                .Where(x => x.IdPracownikaNavigation.Mail == mail)
                .Include(x => x.IdTypuUrlopuNavigation)
                .ToListAsync();
        }

        public async Task DodajUrlop(Urlopy urlop)
        {
            _context.Urlopies.Add(urlop);
            _context.SaveChanges();
        }

    }

}