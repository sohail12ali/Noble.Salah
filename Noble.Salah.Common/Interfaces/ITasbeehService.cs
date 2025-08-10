using Noble.Salah.Common.Models;
using Noble.Salah.Common.Enums;

namespace Noble.Salah.Common.Interfaces;

/// <summary>
/// Service for managing Tasbeeh (Dhikr) counting
/// </summary>
public interface ITasbeehService
{
    /// <summary>
    /// Creates a new Tasbeeh session
    /// </summary>
    Task<TasbeehModel> CreateTasbeehAsync(TasbeehType type, int target, string? customText = null);
    
    /// <summary>
    /// Increments the Tasbeeh count
    /// </summary>
    Task<TasbeehModel> IncrementTasbeehAsync(Guid tasbeehId);
    
    /// <summary>
    /// Decrements the Tasbeeh count
    /// </summary>
    Task<TasbeehModel> DecrementTasbeehAsync(Guid tasbeehId);
    
    /// <summary>
    /// Resets the Tasbeeh count
    /// </summary>
    Task<TasbeehModel> ResetTasbeehAsync(Guid tasbeehId);
    
    /// <summary>
    /// Gets the current Tasbeeh session
    /// </summary>
    Task<TasbeehModel?> GetCurrentTasbeehAsync();
    
    /// <summary>
    /// Gets all Tasbeeh sessions for today
    /// </summary>
    Task<IList<TasbeehModel>> GetTodayTasbeehAsync();
    
    /// <summary>
    /// Gets Tasbeeh sessions for a date range
    /// </summary>
    Task<IList<TasbeehModel>> GetTasbeehRangeAsync(DateTime startDate, DateTime endDate);
    
    /// <summary>
    /// Gets Tasbeeh statistics
    /// </summary>
    Task<TasbeehStatistics> GetTasbeehStatisticsAsync();
    
    /// <summary>
    /// Gets predefined Tasbeeh presets
    /// </summary>
    Task<IList<TasbeehPreset>> GetTasbeehPresetsAsync();
    
    /// <summary>
    /// Saves a custom Tasbeeh preset
    /// </summary>
    Task SaveTasbeehPresetAsync(TasbeehPreset preset);
    
    /// <summary>
    /// Deletes a Tasbeeh session
    /// </summary>
    Task DeleteTasbeehAsync(Guid tasbeehId);
    
    /// <summary>
    /// Exports Tasbeeh data
    /// </summary>
    Task<byte[]> ExportTasbeehAsync(DateTime startDate, DateTime endDate, ExportFormat format = ExportFormat.CSV);
}
