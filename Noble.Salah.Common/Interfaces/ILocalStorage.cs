namespace Noble.Salah.Common.Interfaces;
public interface ILocalStorage
{
    Task SaveAsync<T>(string key, T value);
    Task<T?> LoadAsync<T>(string key);
    Task RemoveAsync(string key);
    Task<bool> ExistsAsync(string key);
}
