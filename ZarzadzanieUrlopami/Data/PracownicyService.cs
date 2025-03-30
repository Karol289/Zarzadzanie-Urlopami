using Microsoft.EntityFrameworkCore;
using ZarzadzanieUrlopami.Models;

namespace ZarzadzanieUrlopami.Data
{
    public class PracownicyService
    {
        private readonly UrlopyDbContext _context;

        public PracownicyService(UrlopyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pracownicy>> GetPracownicyAsync()
        {
            return await _context.Pracownicies.Include(p => p.IdRoliNavigation).ToListAsync();
        }

        public async Task<List<Role>> GetRoleAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task DodajPracownika(Pracownicy pracownik)
        {
            _context.Pracownicies.Add(pracownik);
            await _context.SaveChangesAsync();
        }

        public async Task UsunPracownika(int id)
        {
            var pracownik = await _context.Pracownicies.FindAsync(id);
            if (pracownik != null)
            {
                _context.Pracownicies.Remove(pracownik);
                await _context.SaveChangesAsync();
            }
        }
    }
}
