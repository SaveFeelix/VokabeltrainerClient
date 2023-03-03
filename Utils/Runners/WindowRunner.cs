using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Client.UI.Windows;
using Microsoft.Extensions.Hosting;

namespace Client.Utils.Runners;

public class WindowRunner : IHostedService
{
    private readonly LoginWindow _loginWindow;

    public WindowRunner(LoginWindow loginWindow)
    {
        _loginWindow = loginWindow;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _loginWindow.ShowDialog();
        Application.Current.Shutdown();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}