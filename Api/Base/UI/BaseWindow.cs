using System.Windows;
using Client.Api.Base.ViewModel;

namespace Client.Api.Base.UI;

public class BaseWindow<TViewModel, TWindow> : Window
    where TViewModel : BaseWindowViewModel<TWindow> where TWindow : Window
{
    public TViewModel ViewModel { get; protected set; }

    public BaseWindow()
    {
    }
}