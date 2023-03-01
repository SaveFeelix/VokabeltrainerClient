using System.Threading.Tasks;
using System.Windows;
using Client.Api.Base.ViewModel;
using Client.UI.Windows;
using Client.Utils.Services;

namespace Client.UI.ViewModel.Windows;

public class MainWindowViewModel : BaseWindowViewModel<MainWindow>
{
    private readonly RestService _restService;

    public MainWindowViewModel(MainWindow currentControl, RestService restService) : base(currentControl)
    {
        _restService = restService;
    }

    protected override Task WindowLoaded(object sender, RoutedEventArgs args)
    {
        return Task.CompletedTask;
    }

    protected override Task WindowUnloaded(object sender, RoutedEventArgs args)
    {
        return Task.CompletedTask;
    }
}