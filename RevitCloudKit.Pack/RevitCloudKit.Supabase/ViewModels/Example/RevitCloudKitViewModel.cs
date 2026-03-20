// ════════════════════════════════════════════════════════════════════════
// EXAMPLE — ViewModel demonstrating the full MVVM + CommunityToolkit.Mvvm
//           pattern for a Revit plugin window:
//           ObservableCollection, RelayCommand, CanExecute guards,
//           async commands, and local variable capture before await.
//           Safe to delete and replace with your own ViewModel.
// ════════════════════════════════════════════════════════════════════════

using System.Collections.ObjectModel;
using RevitCloudKit.Models.Example;
using RevitCloudKit.Services.Example;

namespace RevitCloudKit.ViewModels.Example;

public partial class RevitCloudKitViewModel(
    ICloudMaterialService cloudService,
    IRevitMaterialService revitService) : ObservableObject
{
    public ObservableCollection<SupabaseMaterialModel> CloudMaterials { get; } = [];
    public ObservableCollection<Material> RevitMaterials { get; } = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ImportCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
    private SupabaseMaterialModel? _selectedCloudMaterial;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PublishCommand))]
    private Material? _selectedRevitMaterial;

    [RelayCommand]
    private async Task Refresh()
    {
        try
        {
            var cloudMaterials = await cloudService.GetMaterialsAsync();
            CloudMaterials.Clear();
            foreach (var m in cloudMaterials) CloudMaterials.Add(m);

            var revitMaterials = await revitService.GetModelMaterialsAsync();
            RevitMaterials.Clear();
            foreach (var m in revitMaterials) RevitMaterials.Add(m);
        }
        catch (Exception ex)
        {
            TaskDialog.Show("Error", ex.Message);
        }
    }

    private bool CanImport() => SelectedCloudMaterial is not null;

    [RelayCommand(CanExecute = nameof(CanImport))]
    private async Task Import()
    {
        try
        {
            var material = await revitService.ImportToModelAsync(SelectedCloudMaterial!.Name!);
            if (material is not null)
                RevitMaterials.Add(material);
        }
        catch (Exception ex)
        {
            TaskDialog.Show("Import Failed", ex.Message);
        }
    }

    private bool CanPublish() => SelectedRevitMaterial is not null;

    [RelayCommand(CanExecute = nameof(CanPublish))]
    private async Task Publish()
    {
        try
        {
            var record = await cloudService.PublishMaterialAsync(SelectedRevitMaterial!.ToSupabaseRecord());
            CloudMaterials.Add(record);
        }
        catch (Exception ex)
        {
            TaskDialog.Show("Publish Failed", ex.Message);
        }
    }

    private bool CanDelete() => SelectedCloudMaterial is not null;

    [RelayCommand(CanExecute = nameof(CanDelete))]
    private async Task Delete()
    {
        var material = SelectedCloudMaterial!;
        try
        {
            await cloudService.DeleteMaterialAsync(material.Id!);
            CloudMaterials.Remove(material);
        }
        catch (Exception ex)
        {
            TaskDialog.Show("Delete Failed", ex.Message);
        }
    }
}
