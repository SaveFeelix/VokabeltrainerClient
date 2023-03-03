using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Client.Api.Base.ViewModel;
using Client.UI.Windows.Vocables;
using Client.Utils.Models;
using Client.Utils.Services;
using Microsoft.Xaml.Behaviors.Core;

namespace Client.UI.ViewModel.Windows.Vocables;

public class VocableCollectionWindowViewModel : BaseWindowViewModel<VocableCollectionWindow>
{
    private readonly RestService _restService;
    private readonly VocableCollection _collection;

    private string _name;

    public string Title { get; }

    public string Name
    {
        get => _name;
        set
        {
            if (value == _name) return;
            _name = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Vocable> Vocables { get; set; } = new();



    public ICommand CancelCommand { get; }


    public VocableCollectionWindowViewModel(VocableCollectionWindow currentControl, RestService restService,
        VocableCollection vocableCollection) : base(currentControl)
    {
        _restService = restService;
        _collection = vocableCollection;


        Title = $"{vocableCollection.Name} bearbeiten";

        CancelCommand = new ActionCommand(obj => CancelCommandAction(obj as RoutedEventArgs));
    }


    protected override Task WindowLoaded(object sender, RoutedEventArgs args)
    {
        Name = _collection.Name;
        foreach (var vocable in _collection.Vocables)
            Vocables.Add(vocable);

        return Task.CompletedTask;
    }

    protected override Task WindowUnloaded(object sender, RoutedEventArgs args)
    {
        return Task.CompletedTask;
    }


    private void CancelCommandAction(RoutedEventArgs? routedEventArgs)
    {
        CurrentControl.Close();
    }
}