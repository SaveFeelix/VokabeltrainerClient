using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Client.Api.Base.Commands;
using Client.Api.Base.ViewModel;
using Client.UI.Windows;
using Client.UI.Windows.Vocables;
using Client.Utils.Dto.Vocables;
using Client.Utils.Models;
using Client.Utils.Services;
using Microsoft.Xaml.Behaviors.Core;
using RestSharp;

namespace Client.UI.ViewModel.Windows;

public class MainWindowViewModel : BaseWindowViewModel<MainWindow>
{
    private readonly RestService _restService;
    private bool _addCollectionVisible;
    private string _addCollectionName;


    public int SelectedIndex { get; set; } = -1;
    public VocableCollection? SelectedItem { get; set; }
    public ObservableCollection<VocableCollection> VocableCollections { get; set; } = new();

    public bool AddCollectionVisible
    {
        get => _addCollectionVisible;
        set
        {
            if (value == _addCollectionVisible) return;
            _addCollectionVisible = value;
            OnPropertyChanged();
        }
    }

    public string AddCollectionName
    {
        get => _addCollectionName;
        set
        {
            if (value == _addCollectionName) return;
            _addCollectionName = value;
            OnPropertyChanged();
        }
    }

    public ICommand UpdateCommand { get; }
    public ICommand RemoveCommand { get; }
    public ICommand AddCollectionCommand { get; }
    public ICommand SaveAddCollectionCommand { get; }


    public MainWindowViewModel(MainWindow currentControl, RestService restService) : base(currentControl)
    {
        _restService = restService;

        UpdateCommand = new AsyncCommand<RoutedEventArgs>(UpdateCommandAction);
        RemoveCommand = new AsyncCommand<RoutedEventArgs>(RemoveCommandAction);
        AddCollectionCommand = new ActionCommand(obj => AddCollectionCommandAction(obj as RoutedEventArgs));
        SaveAddCollectionCommand = new AsyncCommand<RoutedEventArgs>(SaveAddCollectionCommandAction);
    }

    private async Task SaveAddCollectionCommandAction(RoutedEventArgs? arg)
    {
        if (string.IsNullOrEmpty(AddCollectionName))
        {
            MessageBox.Show("Bitte gib einen gültigen Namen ein!", "Warnung", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        var result = await _restService.Request<VocableCollection>("VocableCollection/Create", Method.Post,
            new VocableCollectionsCreateDto(AddCollectionName));
        if (result.StatusCode != HttpStatusCode.OK)
        {
            MessageBox.Show("Collection konnte nicht erstellt werden (Request ist fehlgeschlagen)", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        VocableCollections.Add(result.Data);

        AddCollectionName = string.Empty;
        AddCollectionVisible = false;
    }

    private void AddCollectionCommandAction(RoutedEventArgs? routedEventArgs)
    {
        AddCollectionVisible = true;
    }

    protected override async Task WindowLoaded(object sender, RoutedEventArgs args)
    {
        var response = await _restService.Request<IList<VocableCollection>>("VocableCollection/All", Method.Get);

        if (response.Data != null)
            foreach (var data in response.Data)
                VocableCollections.Add(data);
    }

    protected override Task WindowUnloaded(object sender, RoutedEventArgs args)
    {
        return Task.CompletedTask;
    }


    private async Task UpdateCommandAction(RoutedEventArgs? routedEventArgs)
    {
        if (SelectedIndex == -1)
        {
            MessageBox.Show("Bitte wähle zuerst ein Item aus!", "Warnung", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        var window = new VocableCollectionWindow(_restService, SelectedItem);
        window.ShowDialog();
        var response = await _restService.Request<IList<VocableCollection>>("VocableCollection/All", Method.Get);
        VocableCollections.Clear();
        if (response.Data != null)
            foreach (var data in response.Data)
                VocableCollections.Add(data);
    }

    private async Task RemoveCommandAction(RoutedEventArgs? arg)
    {
        if (SelectedIndex == -1)
        {
            MessageBox.Show("Bitte wähle zuerst ein Item aus!", "Warnung", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        var tmpCollection = SelectedItem;
        var response =
            await _restService.Request<object>($"VocableCollection/Delete?id={tmpCollection!.Id}", Method.Delete);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            MessageBox.Show($"Löschen von {tmpCollection.Name} ist fehlgeschlagen! (Request ist fehlgeschlagen)",
                "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        SelectedIndex = -1;
        VocableCollections.Remove(tmpCollection);
    }
}