using Client.UI.ViewModel.Windows.Vocables;
using Client.Utils.Models;
using Client.Utils.Services;

namespace Client.UI.Windows.Vocables;

public partial class VocableCollectionWindow
{
    public VocableCollectionWindow(RestService restService, VocableCollection? collection = default)
    {
        ViewModel = new VocableCollectionWindowViewModel(this, restService, collection);
        InitializeComponent();
    }
}