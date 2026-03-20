namespace RevitCloudKit.Services;

public interface IAuthService
{
    /// <summary>
    /// Signs in a user with email and password
    /// </summary>
    Task<bool> SignInAsync(string email, string password, bool rememberMe = false);
    
    /// <summary>
    /// Signs out the current user
    /// </summary>
    Task SignOutAsync();

    /// <summary>
    ///     Validates the current session and refreshes the token if expired.
    ///     Returns <c>true</c> if a valid session exists after the attempt.
    /// </summary>
    bool TryRestoreSession();

    /// <summary>
    ///     Indicates whether a user is currently authenticated in memory.
    /// </summary>
    bool IsAuthenticated { get; }
}