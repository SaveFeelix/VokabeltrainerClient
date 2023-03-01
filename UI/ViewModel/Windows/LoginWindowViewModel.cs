using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Client.Api.Base.Commands;
using Client.Api.Base.ViewModel;
using Client.UI.Windows;
using Client.Utils.Dto.User;
using Client.Utils.Services;
using Microsoft.Xaml.Behaviors.Core;
using RestSharp;

namespace Client.UI.ViewModel.Windows;

public class LoginWindowViewModel : BaseWindowViewModel<LoginWindow>
{
    private readonly RestService _restService;
    private readonly MainWindow _mainWindow;
    private readonly RegisterWindow _registerWindow;

    private string _userName = string.Empty;

    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            OnPropertyChanged();
        }
    }


    public ICommand LoginCommand { get; }
    public ICommand RegisterCommand { get; }

    public LoginWindowViewModel(LoginWindow currentControl, RestService restService, MainWindow mainWindow,
        RegisterWindow registerWindow) : base(
        currentControl)
    {
        LoginCommand = new AsyncCommand<RoutedEventArgs>(LoginCommandAction);
        RegisterCommand = new ActionCommand(o => RegisterCommandAction(o as RoutedEventArgs));

        _restService = restService;
        _mainWindow = mainWindow;
        _registerWindow = registerWindow;
    }


    protected override Task WindowLoaded(object sender, RoutedEventArgs args)
    {
        return Task.CompletedTask;
    }

    protected override Task WindowUnloaded(object sender, RoutedEventArgs args)
    {
        return Task.CompletedTask;
    }


    private async Task LoginCommandAction(RoutedEventArgs? arg)
    {
        var response = await _restService.Request<string>("Auth/Login", Method.Post,
            new UserLoginDto(UserName, CurrentControl.PasswordBox.Password));

        if (response.StatusCode != HttpStatusCode.OK)
        {
            MessageBox.Show("Login fehlgeschlagen! (Request ist fehlgeschlagen)", "Fehler", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        var token = response.Data;
        if (string.IsNullOrEmpty(token))
        {
            MessageBox.Show("Login fehlgeschlagen! (Token konnte nicht ausgelesen werden)", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        _restService.Token = token;

        CurrentControl.Close();
        _mainWindow.ShowDialog();
    }

    private void RegisterCommandAction(RoutedEventArgs? arg)
    {
        _registerWindow.ShowDialog();
    }
}