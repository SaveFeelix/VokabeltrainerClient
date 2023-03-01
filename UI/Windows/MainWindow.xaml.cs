using Client.UI.ViewModel.Windows;
using Client.Utils.Services;

namespace Client.UI.Windows;

public partial class MainWindow
{
    public MainWindow(RestService restService)
    {
        ViewModel = new MainWindowViewModel(this, restService);
        InitializeComponent();
    }
}