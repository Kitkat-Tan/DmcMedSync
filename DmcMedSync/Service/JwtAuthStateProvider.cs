using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace DmcMedSync.Service
{
    public class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public JwtAuthStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var jwtToken = await _sessionStorage.GetAsync<string>("authToken");
                if (!jwtToken.Success || string.IsNullOrWhiteSpace(jwtToken.Value))
                {
                    return new AuthenticationState(_anonymous);
                }

                var claims = ParseClaimsFromJwt(jwtToken.Value).ToList();
                var expiry = claims.FirstOrDefault(c => c.Type == "exp")?.Value;

                if (expiry != null &&
                    DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiry)) < DateTimeOffset.UtcNow)
                {
                    await _sessionStorage.DeleteAsync("authToken");
                    return new AuthenticationState(_anonymous);
                }

                var identity = new ClaimsIdentity(claims, "jwt");
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            catch
            {
                // Also hit during prerendering, when JS interop/session storage isn't available yet
                return new AuthenticationState(_anonymous);
            }
        }

        public async Task LoginAsync(string token)
        {
            await _sessionStorage.SetAsync("authToken", token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task LogoutAsync()
        {
            await _sessionStorage.DeleteAsync("authToken");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonBytes)!;

            foreach (var kvp in keyValuePairs)
            {
                if (kvp.Value.ValueKind == JsonValueKind.Array)
                {
                    // e.g. "role": ["Admin","Doctor"] -> one claim per item
                    foreach (var item in kvp.Value.EnumerateArray())
                        yield return new Claim(kvp.Key, item.ToString());
                }
                else
                {
                    yield return new Claim(kvp.Key, kvp.Value.ToString());
                }
            }
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            base64 = base64.Replace('-', '+').Replace('_', '/'); // JWTs use base64url
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}