using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using ZarzadzanieUrlopami.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AuthState _authState;

    public CustomAuthenticationStateProvider(AuthState authState)
    {
        _authState = authState;
    }

    public void MarkUserAsAuthenticated(string email, string rola, string imie)
    {
        _authState.SetUser(email, rola, imie);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void MarkUserAsLoggedOut()
    {
        _authState.ClearUser();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = _authState.User ?? new ClaimsPrincipal(new ClaimsIdentity());
        return Task.FromResult(new AuthenticationState(user));
    }
}
