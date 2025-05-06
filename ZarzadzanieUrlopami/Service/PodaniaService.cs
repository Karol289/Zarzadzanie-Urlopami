
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
    }

}