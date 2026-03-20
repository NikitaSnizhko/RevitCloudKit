// ════════════════════════════════════════════════════════════════════════
// EXAMPLE — WPF code-behind showing constructor-injected ViewModel
//           pattern. The ViewModel is resolved by the DI container and
//           passed in — the View never creates it directly.
//           RefreshCommand fires on Loaded to populate data immediately.
//           Safe to delete and replace with your own View.
// ════════════════════════════════════════════════════════════════════════

using System.Windows;
using RevitCloudKit.ViewModels.Example;

namespace RevitCloudKit.Views.Example;

public sealed partial class RevitCloudKitView
{
    public RevitCloudKitView(RevitCloudKitViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is RevitCloudKitViewModel vm)
            _ = vm.RefreshCommand.ExecuteAsync(null);
    }
}
