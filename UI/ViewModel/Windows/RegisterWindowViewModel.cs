using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Client.Api.Base.Commands;
using Client.Api.Base.ViewModel;
using Client.UI.Windows;
using Client.Utils.Dto.User;
using Client.Utils.Services;
using RestSharp;

namespace Client.UI.ViewModel.Windows;

public class RegisterWindowViewModel : BaseWindowViewModel<RegisterWindow>
{
    private readonly RestService _restService;

    private string _email = string.Empty;
    private string _userName = string.Empty;

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            OnPropertyChanged();
        }
    }


    public ICommand RegisterCommand { get; set; }

    public RegisterWindowViewModel(RegisterWindow currentControl, RestService restService) : base(currentControl)
    {
        _restService = restService;
        RegisterCommand = new AsyncCommand<RoutedEventArgs>(RegisterCommandAction);
    }

    protected override Task WindowLoaded(object sender, RoutedEventArgs args)
    {
        return Task.CompletedTask;
    }

    protected override Task WindowUnloaded(object sender, RoutedEventArgs args)
    {
        return Task.CompletedTask;
    }

    private async Task RegisterCommandAction(RoutedEventArgs? arg)
    {
        try
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(UserName) ||
                string.IsNullOrEmpty(CurrentControl.PasswordBox.Password) ||
                string.IsNullOrEmpty(CurrentControl.PasswordBoxConfirm.Password))
                throw new Exception("Nicht alle Felder sind ausgefüllt!");

            try
            {
                _ = new MailAddress(Email);
            }
            catch
            {
                throw new Exception($"Fehlerhafte E-Mail Adresse: {Email}");
            }

            if (!CurrentControl.PasswordBoxConfirm.Password.Equals(CurrentControl.PasswordBox.Password))
                throw new Exception("Die Passwörter müssen identisch sein!");


            var result = await _restService.Request<object>("Auth/Register", Method.Post,
                new UserRegisterDto(Email, UserName, CurrentControl.PasswordBox.Password));
            if (result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Registrierung fehlgeschlagen! (Request ist fehlgeschlagen)", "Fehler",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Registrierung erfolgreich!", "Information", MessageBoxButton.OK,
                MessageBoxImage.Information);
            CurrentControl.Close();
        }
        catch (Exception e)
        {
            MessageBox.Show($"Registrierung fehlgeschlagen! ({e.Message})", "Fehler", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}