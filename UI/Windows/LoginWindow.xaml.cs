using Client.UI.ViewModel.Windows;
using Client.Utils.Services;

namespace Client.UI.Windows;

public partial class LoginWindow
{
    public LoginWindow(RestService restService, MainWindow mainWindow, RegisterWindow registerWindow)
    {
        ViewModel = new LoginWindowViewModel(this, restService, mainWindow, registerWindow);
        InitializeComponent();
    }
}