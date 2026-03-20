using Autodesk.Revit.Attributes;
using RevitCloudKit.Services;
using RevitCloudKit.Views;
using RevitCloudKit.Views.Example;

namespace RevitCloudKit.Commands;

/// <summary>
///     External command entry point.
/// </summary>
[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public class StartupCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        try
        {
#if DEBUG_R25 || DEBUG_R26
            // Only use for DEBUG in Revit Add-in Manager. Cause in Release production Application.cs is used!
            RevitTask.Initialize(commandData.Application);
            Host.Start();
#endif

            var authService = Host.GetService<IAuthService>();
            if (!authService.TryRestoreSession())
            {
                var loginView = Host.GetService<LoginView>();
                loginView.ShowDialog();

                if (!authService.IsAuthenticated)
                    return Result.Cancelled;
            }

            var view = Host.GetService<RevitCloudKitView>();
            view.Show();
        }
        catch (OperationCanceledException)
        {
            return Result.Cancelled;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return Result.Failed;
        }

        return Result.Succeeded;
    }
}