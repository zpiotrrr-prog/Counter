using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Counter.Models;
using Counter.Services;

namespace Counter.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string newCounterName = string.Empty;

    [ObservableProperty]
    private string newCounterInitialValue = string.Empty;

    public ObservableCollection<CounterViewModel> Counters { get; } = new();

    public MainViewModel()
    {
        LoadCounters();
    }

    [RelayCommand]
    private void AddCounter()
    {
        if (string.IsNullOrWhiteSpace(NewCounterName))
            return;

        int.TryParse(NewCounterInitialValue, out int initialValue);

        var vm = new CounterViewModel(
            new CounterModel { Name = NewCounterName, Value = initialValue },
            SaveCounters
        );

        Counters.Add(vm);
        SaveCounters();

        NewCounterName = string.Empty;
        NewCounterInitialValue = string.Empty;
    }

    private void LoadCounters()
    {
        var list = CounterStorageService.LoadCounters();
        foreach (var item in list)
        {
            Counters.Add(new CounterViewModel(item, SaveCounters));
        }
    }

    private void SaveCounters()
    {
        var list = Counters.Select(c => c.ToModel()).ToList();
        CounterStorageService.SaveCounters(list);
    }
}
