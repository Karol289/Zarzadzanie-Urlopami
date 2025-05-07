using Microsoft.EntityFrameworkCore;
using ZarzadzanieUrlopami.Data;
using ZarzadzanieUrlopami.Models;

namespace ZarzadzanieUrlopami.Service;

public class ReportsService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ReportsService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<List<Urlopy>> GetAllUrlopiesInMonth(int month, int year)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<UrlopyDbContext>();

        return await ctx.Urlopies
            .Where(u =>
                (u.DataPocz.HasValue && u.DataPocz.Value.Month == month && u.DataPocz.Value.Year == year) ||
                (u.DataKon.HasValue && u.DataKon.Value.Month == month && u.DataKon.Value.Year == year) ||
                (u.DataPocz.HasValue && u.DataKon.HasValue &&
                 u.DataPocz.Value <= new DateOnly(year, month, DateTime.DaysInMonth(year, month)) &&
                 u.DataKon.Value >= new DateOnly(year, month, 1)))
            .ToListAsync();
    }

    public async Task<List<ZwolnieniaLekarskie>> GetAllZwolnieniaInMonth(int month, int year)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<UrlopyDbContext>();

        return await ctx.ZwolnieniaLekarskies
            .Where(z =>
                (z.DataPocz.HasValue && z.DataPocz.Value.Month == month && z.DataPocz.Value.Year == year) ||
                (z.DataKon.HasValue && z.DataKon.Value.Month == month && z.DataKon.Value.Year == year) ||
                (z.DataPocz.HasValue && z.DataKon.HasValue &&
                 z.DataPocz.Value <= new DateOnly(year, month, DateTime.DaysInMonth(year, month)) &&
                 z.DataKon.Value >= new DateOnly(year, month, 1)))
            .ToListAsync();
    }

    public async Task<List<Pracownicy>> GetPracownicyWithAbsencesInMonth(int month, int year)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var ctx = scope.ServiceProvider.GetRequiredService<UrlopyDbContext>();

        var urlopyTask = GetAllUrlopiesInMonth(month, year);
        var zwolnieniaTask = GetAllZwolnieniaInMonth(month, year);

        await Task.WhenAll(urlopyTask, zwolnieniaTask);

        var urlopy = await urlopyTask;
        var zwolnienia = await zwolnieniaTask;

        var pracownicyIds = urlopy.Select(u => u.IdPracownika)
            .Concat(zwolnienia.Select(z => z.IdPracownika))
            .Distinct().ToList();

        var urlopyIds = urlopy.Select(u => u.IdUrlopu);
        var zwolnieniaIds = zwolnienia.Select(z => z.IdZwolnienia);

        return await ctx.Pracownicies
            .Where(p => pracownicyIds.Contains(p.IdPracownika))
            .Include(p => p.Urlopies.Where(u => urlopyIds.Contains(u.IdUrlopu))).ThenInclude(u => u.IdStatusuNavigation)
            .Include(p => p.ZwolnieniaLekarskies.Where(z => zwolnieniaIds.Contains(z.IdZwolnienia))).ThenInclude(z => z.IdTypuNavigation)
            .ToListAsync();
    }
}
