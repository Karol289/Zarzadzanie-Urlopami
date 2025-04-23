using System.Security.Claims;

namespace ZarzadzanieUrlopami.Services
{
    public class AuthState
    {
        public ClaimsPrincipal? User { get; private set; }

        public void SetUser(string email, string rola, string imie)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, rola),
                new Claim("Imie", imie)
            }, "manualAuth");

            User = new ClaimsPrincipal(identity);
        }

        public void ClearUser()
        {
            User = new ClaimsPrincipal(new ClaimsIdentity());
        }

        public bool IsAuthenticated => User?.Identity?.IsAuthenticated == true;
    }
}
