using System.Reflection;
using System.Windows.Media.Imaging;
using RevitCloudKit.Commands;

namespace RevitCloudKit;

/// <summary>
///     Application entry point
/// </summary>
[UsedImplicitly]
public class Application : IExternalApplication
{
    private const string AppName = "RevitCloudKit";

    public Result OnStartup(UIControlledApplication application)
    {
        try
        {
            RevitTask.Initialize(application);
            Host.Start();

            var panel = application.CreateRibbonPanel(AppName);

            var mainButton = new PushButtonData(
                nameof(StartupCommand),
                "Main Command",
                Assembly.GetExecutingAssembly().Location,
                typeof(StartupCommand).FullName)
            {
                LargeImage = new BitmapImage(new Uri("pack://application:,,,/RevitCloudKit;component/Resources/Icons/RCK32.png")),
                Image = new BitmapImage(new Uri("pack://application:,,,/RevitCloudKit;component/Resources/Icons/RCK16.png")),
                LongDescription = "Main command for RevitCloudKit.",
                ToolTip = "Main command for RevitCloudKit."
            };

            var loginButton = new PushButtonData(
                nameof(LoginCommand),
                "Login",
                Assembly.GetExecutingAssembly().Location,
                typeof(LoginCommand).FullName)
            {
                LargeImage = new BitmapImage(new Uri("pack://application:,,,/RevitCloudKit;component/Resources/Icons/RCK32.png")),
                Image = new BitmapImage(new Uri("pack://application:,,,/RevitCloudKit;component/Resources/Icons/RCK16.png")),
                LongDescription = "Login to RevitCloudKit.",
                ToolTip = "Login to RevitCloudKit."
            };

            panel.AddItem(mainButton);
            panel.AddItem(loginButton);
        }
        catch (OperationCanceledException)
        {
            return Result.Cancelled;
        }
        catch (Exception ex)
        {
            TaskDialog.Show("Error", ex.Message);
            return Result.Failed;
        }
        return Result.Succeeded;
    }

    public Result OnShutdown(UIControlledApplication application)
    {
        return Result.Succeeded;
    }
}
