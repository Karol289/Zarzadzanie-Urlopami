using Microsoft.EntityFrameworkCore;
using ZarzadzanieUrlopami.Data;
using ZarzadzanieUrlopami.Models;

namespace ZarzadzanieUrlopami.Service
{
    public class PracownicyService
    {
        private readonly UrlopyDbContext _context;
        private readonly PasswordService _passwordService;

        public PracownicyService(UrlopyDbContext context)
        {
            _context = context;
            _passwordService = new PasswordService();
        }

        public async Task<List<Pracownicy>> GetPracownicyAsync()
        {
            return await _context.Pracownicies.Include(p => p.IdRoliNavigation).ToListAsync();
        }

        public async Task<List<Role>> GetRoleAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task DodajPracownika(Pracownicy pracownik, string plainPassword)
        {
            pracownik.HasloHash = _passwordService.HashPassword(plainPassword);
            _context.Pracownicies.Add(pracownik);
            await _context.SaveChangesAsync();
        }

        public async Task AktualizujHasloPracownika(int id, string newPassword)
        {
            var pracownik = await _context.Pracownicies.FindAsync(id);
            if (pracownik != null)
            {
                pracownik.HasloHash = _passwordService.HashPassword(newPassword);
                await _context.SaveChangesAsync();
            }
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

        public async Task AktualizujPracownika(Pracownicy pracownik)
        {
            _context.Pracownicies.Update(pracownik);
            await _context.SaveChangesAsync();
        }

        public async Task<Pracownicy?> GetPracownikByEmailAsync(string email)
        {
            return await _context.Pracownicies
                .Include(p => p.IdRoliNavigation)
                .FirstOrDefaultAsync(p => p.Mail == email);
        }
    }
}
