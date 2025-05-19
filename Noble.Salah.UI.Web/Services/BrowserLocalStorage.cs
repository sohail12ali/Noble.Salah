using Microsoft.JSInterop;
using Noble.Salah.Common.Interfaces;
using System.Text.Json;

namespace Noble.Salah.UI.Web.Services;

public class BrowserLocalStorage(IJSRuntime js) : ILocalStorage
{
    public async Task SaveAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        await js.InvokeVoidAsync("localStorage.setItem", key, json);
    }

    public async Task<T?> LoadAsync<T>(string key)
    {
        var json = await js.InvokeAsync<string>("localStorage.getItem", key);
        return string.IsNullOrWhiteSpace(json) ? default : JsonSerializer.Deserialize<T?>(json);
    }

    public async Task RemoveAsync(string key)
    {
        await js.InvokeVoidAsync("localStorage.removeItem", key);
    }

    public async Task<bool> ExistsAsync(string key)
    {
        var value = await js.InvokeAsync<string>("localStorage.getItem", key);
        return !string.IsNullOrWhiteSpace(value);
    }
}
