using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Api.Base.Commands;

public class AsyncCommand<TParamType> : ICommand where TParamType : class
{
    private readonly Func<TParamType?, Task> _executeAsync;
    private readonly Func<object?, bool>? _canExecute;

    public AsyncCommand(Func<TParamType?, Task> executeAsync, Func<object?, bool>? canExecute = default)
    {
        _executeAsync = executeAsync;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke(parameter) ?? true;
    }

    public async void Execute(object? parameter)
    {
        await _executeAsync(parameter as TParamType);
    }

    public event EventHandler? CanExecuteChanged;
}