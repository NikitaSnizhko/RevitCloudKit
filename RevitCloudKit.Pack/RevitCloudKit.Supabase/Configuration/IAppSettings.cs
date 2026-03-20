namespace RevitCloudKit.Configuration;

/// <summary>
///     Provides configuration values required by the application.
///     Replace <see cref="EnvironmentAppSettings"/> with your own implementation
///     (e.g. appsettings.json, Windows Registry, Azure Key Vault, encrypted file).
/// </summary>
public interface IAppSettings
{
    string SupabaseUrl { get; }
    string SupabaseKey { get; }
}
