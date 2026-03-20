// ════════════════════════════════════════════════════════════════════════
// EXAMPLE — Revit API service implementation using RevitTask to safely
//           marshal all calls to the Revit main thread.
//           Shows: FilteredElementCollector, Transaction, Material.Create,
//           and the async Revit pattern (Task.RunAsync returning a value).
//           Safe to delete and replace with your own Revit service.
// ════════════════════════════════════════════════════════════════════════

namespace RevitCloudKit.Services.Example;

public class RevitMaterialService : IRevitMaterialService
{
    public Task<IReadOnlyList<Material>> GetModelMaterialsAsync()
    {
        return RevitTask.RunAsync(app =>
        {
            var doc = app.ActiveUIDocument.Document;
            return (IReadOnlyList<Material>)new FilteredElementCollector(doc)
                .OfClass(typeof(Material))
                .Cast<Material>()
                .OrderBy(m => m.Name)
                .ToList();
        });
    }

    public Task<Material?> ImportToModelAsync(string name)
    {
        return RevitTask.RunAsync(app =>
        {
            var doc = app.ActiveUIDocument.Document;

            // Guard: do not create a duplicate
            var existing = new FilteredElementCollector(doc)
                .OfClass(typeof(Material))
                .Cast<Material>()
                .FirstOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));

            if (existing is not null) return null;

            using var tx = new Transaction(doc, $"Import Material: {name}");
            tx.Start();
            var id = Material.Create(doc, name);
            tx.Commit();

            return doc.GetElement(id) as Material;
        });
    }
}
