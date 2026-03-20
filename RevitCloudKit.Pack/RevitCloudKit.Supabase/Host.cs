using Microsoft.Extensions.DependencyInjection;
using RevitCloudKit.Configuration;
using RevitCloudKit.Services;
using RevitCloudKit.Services.Example;
using RevitCloudKit.ViewModels;
using RevitCloudKit.ViewModels.Example;
using RevitCloudKit.Views;
using RevitCloudKit.Views.Example;
using Supabase;

namespace RevitCloudKit;

/// <summary>
///     Provides a host for the application's services and manages their lifetimes.
/// </summary>
public static class Host
{
    private static IServiceProvider? _serviceProvider;

    public static void Start()
    {
        var services = new ServiceCollection();

        // --- Configuration ---
        services.AddSingleton<IAppSettings, EnvironmentAppSettings>();

        // --- Auth ---
        services.AddSingleton(sp => BuildSupabaseClient(sp.GetRequiredService<IAppSettings>()));
        services.AddSingleton<IAuthService, SupabaseAuthService>();

        // --- Domain services ---
        services.AddSingleton<ICloudMaterialService, SupabaseMaterialService>();
        services.AddSingleton<IRevitMaterialService, RevitMaterialService>();

        // --- Views / ViewModels ---
        services.AddTransient<RevitCloudKitViewModel>();
        services.AddTransient<RevitCloudKitView>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<LoginView>();

        _serviceProvider = services.BuildServiceProvider();
    }

    private static Client BuildSupabaseClient(IAppSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.SupabaseUrl))
            throw new InvalidOperationException("SupabaseUrl is not configured.");

        if (string.IsNullOrWhiteSpace(settings.SupabaseKey))
            throw new InvalidOperationException("SupabaseKey is not configured.");

        var options = new SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = false, // Set true if you need realtime features. Requires additional setup in Supabase.
            SessionHandler = new SupabaseSessionHandler()
        };

        var client = new Client(settings.SupabaseUrl, settings.SupabaseKey, options);
        Task.Run(() => client.InitializeAsync()).GetAwaiter().GetResult();
        return client;
    }

    /// <summary>
    ///     Get service of type <typeparamref name="T"/>.
    /// </summary>
    public static T GetService<T>() where T : class =>
        _serviceProvider!.GetRequiredService<T>();
}
