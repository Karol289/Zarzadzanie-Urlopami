using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using ZarzadzanieUrlopami.Data;
using ZarzadzanieUrlopami.Models;

namespace ZarzadzanieUrlopami.Service
{
    public class ZmianyService
    {
        private readonly UrlopyDbContext _context;

        public ZmianyService(UrlopyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pracownicy>> GetAllPracownicyAsync()
        {
            return await _context.Pracownicies.ToListAsync();
        }

        public async Task<List<DniRobocze>> GetDniRoboczeAsync(int year, int month)
        {
            DateOnly start = new DateOnly(year, month, 1);
            DateOnly end = new DateOnly(year, month, DateTime.DaysInMonth(year, month));

            return await _context.DniRoboczes
            .Where(d => d.DataDnia.HasValue &&
                        d.DataDnia.Value >= start &&
                        d.DataDnia.Value <= end)
            .ToListAsync();

        }

        public async Task<List<Zmiany>> GetZmianyDlaDniaAsync(DateOnly date)
        {
            var dzien = await _context.DniRoboczes
                .Include(d => d.Zmianies)
                    .ThenInclude(z => z.IdPracownikaNavigation)
                .Include(d => d.IdKierownikaNavigation)
                .FirstOrDefaultAsync(d => d.DataDnia == date);

            return dzien?.Zmianies.ToList() ?? new List<Zmiany>();
        }

        public async Task DodajZmianeAsync(Zmiany zmiana)
        {
            _context.Zmianies.Add(zmiana);
            await _context.SaveChangesAsync();
        }

        public async Task EdytujZmianeAsync(Zmiany zmiana)
        {
            _context.Zmianies.Update(zmiana);
            await _context.SaveChangesAsync();
        }

        public async Task UsunZmianeAsync(int idZmiany)
        {
            var zmiana = await _context.Zmianies.FindAsync(idZmiany);
            if (zmiana != null)
            {
                _context.Zmianies.Remove(zmiana);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DodajDzienRoboczyAsync(ClaimsPrincipal user, DateOnly dataDnia)
        {
            string? email = user.FindFirst(ClaimTypes.Name)?.Value;
            var pracownik = await _context.Pracownicies.FirstOrDefaultAsync(p => p.Mail == email);

            var nowyDzien = new DniRobocze
            {
                IdKierownika = pracownik.IdPracownika,
                DataDnia = dataDnia
            };

            _context.DniRoboczes.Add(nowyDzien);
            await _context.SaveChangesAsync();
        }

        public async Task UsunDzienRoboczyAsync(DateOnly dataDnia)
        {
            var dzien = await _context.DniRoboczes
                .FirstOrDefaultAsync(d => d.DataDnia == dataDnia);

            if (dzien != null)
            {
                _context.DniRoboczes.Remove(dzien);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<DniRobocze?> PobierzPoDacieAsync(DateOnly data)
        {
            return await _context.DniRoboczes
                .FirstOrDefaultAsync(d => d.DataDnia == data);
        }


    }


}