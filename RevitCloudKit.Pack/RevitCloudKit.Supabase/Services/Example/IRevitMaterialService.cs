// ════════════════════════════════════════════════════════════════════════
// EXAMPLE — Service interface for Revit-side operations that must
//           run on the Revit main thread via RevitTask.
//           Shows the recommended contract pattern for any service
//           that wraps Revit API calls.
//           Safe to delete and replace with your own service interface.
// ════════════════════════════════════════════════════════════════════════

namespace RevitCloudKit.Services.Example;

/// <summary>
///     Revit-side material operations that run on the Revit main thread via RevitTask.
/// </summary>
public interface IRevitMaterialService
{
    Task<IReadOnlyList<Material>> GetModelMaterialsAsync();

    Task<Material?> ImportToModelAsync(string name);
}
