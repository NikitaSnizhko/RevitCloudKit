using RevitCloudKit.Services;

namespace RevitCloudKit.ViewModels;

public partial class LoginViewModel(IAuthService authService) : ObservableObject
{
    public event Action? LoginSucceeded;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;
    
    [ObservableProperty]
    private bool _rememberMe;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [RelayCommand]
    private async Task Login()
    {
        ErrorMessage = string.Empty;
        try
        {
            var success = await authService.SignInAsync(Email, Password);
            //  Check <SupabaseAuthService> logic to implement RememberMe functionality. 
            //  RememberMe auth variant:     await authService.SignInAsync(Email, Password, RememberMe);
            
            if (success)
                LoginSucceeded?.Invoke();
            else
                ErrorMessage = "Invalid email or password.";
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Login failed: {ex.Message}";
        }
    }
}
