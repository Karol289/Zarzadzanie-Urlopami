using ZarzadzanieUrlopami.Models;
using Microsoft.EntityFrameworkCore;
using ZarzadzanieUrlopami.Data;
using ZarzadzanieUrlopami.Service;

public class LoginService
{
    private readonly UrlopyDbContext _context;
    private readonly PasswordService _passwordService;

    public LoginService(UrlopyDbContext context)
    {
        _context = context;
        _passwordService = new PasswordService();
    }

    public async Task<Pracownicy?> AuthenticateAsync(string email, string password)
    {
        var user = await _context.Pracownicies.FirstOrDefaultAsync(x => x.Mail == email);
        if (user == null)
            return null;

        if (_passwordService.VerifyPassword(user.HasloHash, password))
            return user;

        return null;
    }
}
