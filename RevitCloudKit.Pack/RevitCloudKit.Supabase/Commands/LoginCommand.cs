using Autodesk.Revit.Attributes;
using RevitCloudKit.Views;

namespace RevitCloudKit.Commands;

[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public class LoginCommand : IExternalCommand
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
            
            var view = Host.GetService<LoginView>();
            view.ShowDialog();
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
