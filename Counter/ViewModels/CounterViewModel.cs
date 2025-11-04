using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Counter.Models;

namespace Counter.ViewModels;

public partial class CounterViewModel : ObservableObject
{
    private readonly Action _onChanged;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private int value;

    public CounterViewModel(CounterModel model, Action onChanged)
    {
        name = model.Name;
        value = model.Value;
        _onChanged = onChanged;
    }

    [RelayCommand]
    private void Increment()
    {
        Value++;
        _onChanged?.Invoke();
    }

    [RelayCommand]
    private void Decrement()
    {
        Value--;
        _onChanged?.Invoke();
    }

    public CounterModel ToModel() => new CounterModel { Name = this.Name, Value = this.Value };
}
