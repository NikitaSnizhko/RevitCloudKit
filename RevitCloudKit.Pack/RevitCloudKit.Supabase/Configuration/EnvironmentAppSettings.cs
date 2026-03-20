namespace RevitCloudKit.Configuration;

/// <summary>
///     Default <see cref="IAppSettings"/> implementation that reads from environment variables.
///     Environment variables must be set before Revit.exe starts.
///     Replace this class with your own implementation — see <see cref="IAppSettings"/>.
/// </summary>
public class EnvironmentAppSettings : IAppSettings
{
    public string SupabaseUrl { get; } =
        Environment.GetEnvironmentVariable("SUPABASE_URL")
        ?? throw new InvalidOperationException("SUPABASE_URL environment variable is not set.");

    public string SupabaseKey { get; } =
        Environment.GetEnvironmentVariable("SUPABASE_KEY")
        ?? throw new InvalidOperationException("SUPABASE_KEY environment variable is not set.");
}
