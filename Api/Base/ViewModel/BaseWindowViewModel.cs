using System.Threading.Tasks;
using System.Windows;

namespace Client.Api.Base.ViewModel;

public abstract class BaseWindowViewModel<TWindow> : BaseViewModel<TWindow> where TWindow : Window
{
    public BaseWindowViewModel(TWindow currentControl) : base(currentControl)
    {
        CurrentControl.Loaded += async (sender, args) => await WindowLoaded(sender, args);
        CurrentControl.Unloaded += async (sender, args) => await WindowUnloaded(sender, args);
    }

    protected abstract Task WindowLoaded(object sender, RoutedEventArgs args);
    protected abstract Task WindowUnloaded(object sender, RoutedEventArgs args);
}