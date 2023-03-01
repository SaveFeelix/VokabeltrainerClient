using Client.UI.ViewModel.Windows;
using Client.Utils.Services;

namespace Client.UI.Windows;

public partial class RegisterWindow
{
    public RegisterWindow(RestService restService)
    {
        ViewModel = new RegisterWindowViewModel(this, restService);
        InitializeComponent();
    }
}