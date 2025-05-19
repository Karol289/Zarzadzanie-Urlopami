using System;
using ZarzadzanieUrlopami.Data;
using ZarzadzanieUrlopami.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<DniRobocze?> PobierzPoDacieAsync(DateOnly data)
        {
            return await _context.DniRoboczes
                .FirstOrDefaultAsync(d => d.DataDnia == data);
        }


    }


}