using Microsoft.EntityFrameworkCore;
using ZarzadzanieUrlopami.Models;

namespace ZarzadzanieUrlopami.Data
{
    public class TypyUrlopowService
    {
        private readonly UrlopyDbContext _context;

        public TypyUrlopowService(UrlopyDbContext context)
        {
            _context = context;
        }

        public async Task<List<TypyUrlopow>> GetTypyUrlopowAsync()
        {
            return await _context.TypyUrlopows.ToListAsync();
        }

        public async Task DodajTypUrlopu(TypyUrlopow typUrlopu)
        {
            _context.TypyUrlopows.Add(typUrlopu);
            await _context.SaveChangesAsync();
        }

        public async Task AktualizujTypUrlopu(TypyUrlopow typUrlopu)
        {
            _context.TypyUrlopows.Update(typUrlopu);
            await _context.SaveChangesAsync();
        }

        public async Task UsunTypUrlopu(int id)
        {
            var typUrlopu = await _context.TypyUrlopows.FindAsync(id);
            if (typUrlopu != null)
            {
                _context.TypyUrlopows.Remove(typUrlopu);
                await _context.SaveChangesAsync();
            }
        }
    }
}
