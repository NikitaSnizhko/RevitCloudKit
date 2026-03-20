using Supabase;
using Supabase.Gotrue.Exceptions;

namespace RevitCloudKit.Services;

public class SupabaseAuthService(Client supabaseClient) : IAuthService
{
    private readonly Client _supabaseClient = supabaseClient ?? throw new ArgumentNullException(nameof(supabaseClient));

    public bool IsAuthenticated => _supabaseClient.Auth.CurrentUser != null;

    public bool TryRestoreSession()
    {
        try
        {
            _supabaseClient.Auth.LoadSession();
            var session = Task.Run(() => _supabaseClient.Auth.RetrieveSessionAsync())
                              .ConfigureAwait(false)
                              .GetAwaiter()
                              .GetResult();
            return session != null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> SignInAsync(string email, string password, bool rememberMe = false)
    {
        try
        {
            // Implement {rememberMe} logic as needed, e.g., by saving securely.
            
            var session = await _supabaseClient.Auth.SignIn(email, password);
            return !string.IsNullOrWhiteSpace(session?.AccessToken) && IsAuthenticated;
        }
        catch (GotrueException)
        {
            // Invalid credentials to implement yourself.
            return false;
        }
    }

    public async Task SignOutAsync()
    {
        await _supabaseClient.Auth.SignOut();
    }
}