using System.Text.Json;
using Noble.Salah.Common.Interfaces;

namespace Noble.Salah.UI.Services;

public class MauiLocalStorage : ILocalStorage
{
    public Task SaveAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        Preferences.Set(key, json);
        return Task.CompletedTask;
    }

    public Task<T?> LoadAsync<T>(string key)
    {
        if (!Preferences.ContainsKey(key))
            return Task.FromResult<T?>(default);

        var json = Preferences.Get(key, string.Empty);
        return Task.FromResult(JsonSerializer.Deserialize<T?>(json));
    }

    public Task RemoveAsync(string key)
    {
        Preferences.Remove(key);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(string key)
    {
        return Task.FromResult(Preferences.ContainsKey(key));
    }
}
