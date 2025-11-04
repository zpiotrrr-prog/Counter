using Microsoft.Maui.Storage;
using Counter.Models;

namespace Counter.Services;

public static class CounterStorageService
{
    private const string Key = "counters_data";

    public static List<CounterModel> LoadCounters()
    {
        var data = Preferences.Get(Key, string.Empty);
        if (string.IsNullOrEmpty(data))
            return new List<CounterModel>();

        var counters = new List<CounterModel>();
        var items = data.Split('|', StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in items)
        {
            var parts = item.Split(':', 2);
            if (parts.Length == 2 && int.TryParse(parts[1], out int value))
            {
                counters.Add(new CounterModel { Name = parts[0], Value = value });
            }
        }
        return counters;
    }

    public static void SaveCounters(List<CounterModel> counters)
    {
        if (counters == null || counters.Count == 0)
        {
            Preferences.Remove(Key);
            return;
        }

        var serialized = string.Join("|", counters.Select(c => $"{c.Name}:{c.Value}"));
        Preferences.Set(Key, serialized);
    }
}
