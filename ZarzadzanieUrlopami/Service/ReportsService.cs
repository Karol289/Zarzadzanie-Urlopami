using Microsoft.EntityFrameworkCore;
using ZarzadzanieUrlopami.Data;
using ZarzadzanieUrlopami.Models;

namespace ZarzadzanieUrlopami.Service;

public class ReportsService
{
    private readonly UrlopyDbContext _context;

    public ReportsService(UrlopyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Urlopy>> GetAllUrlopiesInMonth(int month, int year)
    {
        return await _context.Urlopies
            .Include(u => u.IdPracownikaNavigation)
            .Include(u => u.IdTypuUrlopuNavigation)
            .Include(u => u.IdStatusuNavigation)
            .Where(u => (u.DataPocz.HasValue && u.DataPocz.Value.Month == month && u.DataPocz.Value.Year == year) ||
                        (u.DataKon.HasValue && u.DataKon.Value.Month == month && u.DataKon.Value.Year == year) ||
                        (u.DataPocz.HasValue && u.DataKon.HasValue &&
                         u.DataPocz.Value < new DateOnly(year, month, 1) &&
                         u.DataKon.Value >= new DateOnly(year, month, 1)))
            .ToListAsync();
    }

    public async Task<List<ZwolnieniaLekarskie>> GetAllZwolnieniaInMonth(int month, int year)
    {
        return await _context.ZwolnieniaLekarskies
            .Include(z => z.IdPracownikaNavigation)
            .Include(z => z.IdTypuNavigation)
            .Include(z => z.IdWystawcyNavigation)
            .Where(z => (z.DataPocz.HasValue && z.DataPocz.Value.Month == month && z.DataPocz.Value.Year == year) ||
                        (z.DataKon.HasValue && z.DataKon.Value.Month == month && z.DataKon.Value.Year == year) ||
                        (z.DataPocz.HasValue && z.DataKon.HasValue &&
                         z.DataPocz.Value < new DateOnly(year, month, 1) &&
                         z.DataKon.Value >= new DateOnly(year, month, 1)))
            .ToListAsync();
    }

    public async Task<List<Pracownicy>> GetPracownicyWithAbsencesInMonth(int month, int year)
    {
        var urlopyInMonth = await GetAllUrlopiesInMonth(month, year);
        var zwolnieniaInMonth = await GetAllZwolnieniaInMonth(month, year);

        // Get unique employee IDs who have absences in this month
        var pracownicyIds = urlopyInMonth.Select(u => u.IdPracownika)
            .Concat(zwolnieniaInMonth.Select(z => z.IdPracownika))
            .Distinct();

        // Get all those employees with their related data
        return await _context.Pracownicies
            .Where(p => pracownicyIds.Contains(p.IdPracownika))
            .Include(p => p.Urlopies.Where(u =>
                (u.DataPocz.HasValue && u.DataPocz.Value.Month == month && u.DataPocz.Value.Year == year) ||
                (u.DataKon.HasValue && u.DataKon.Value.Month == month && u.DataKon.Value.Year == year) ||
                (u.DataPocz.HasValue && u.DataKon.HasValue &&
                 u.DataPocz.Value < new DateOnly(year, month, 1) &&
                 u.DataKon.Value >= new DateOnly(year, month, 1))))
            .Include(p => p.ZwolnieniaLekarskies.Where(z =>
                (z.DataPocz.HasValue && z.DataPocz.Value.Month == month && z.DataPocz.Value.Year == year) ||
                (z.DataKon.HasValue && z.DataKon.Value.Month == month && z.DataKon.Value.Year == year) ||
                (z.DataPocz.HasValue && z.DataKon.HasValue &&
                 z.DataPocz.Value < new DateOnly(year, month, 1) &&
                 z.DataKon.Value >= new DateOnly(year, month, 1))))
            .ToListAsync();
    }
}
