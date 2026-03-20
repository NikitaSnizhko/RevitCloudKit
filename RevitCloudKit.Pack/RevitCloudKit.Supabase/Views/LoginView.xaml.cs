using System.Reflection;
using RevitCloudKit.ViewModels;

namespace RevitCloudKit.Views;

public partial class LoginView
{
    private readonly LoginViewModel _loginViewModel;
    
    public LoginView(LoginViewModel viewModel)
    {
        _loginViewModel = viewModel;
        InitializeComponent();
        DataContext = viewModel;
        viewModel.LoginSucceeded += OnLoginSucceeded;
        VersionText.Text = $"RevitCloudKit v{Assembly.GetExecutingAssembly().GetName().Version?.ToString(3)}";
    }

    private void OnLoginSucceeded()
    {
        Close();
        _loginViewModel.LoginSucceeded -= OnLoginSucceeded;
    }
}