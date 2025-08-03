# Noble.Salah AI Development Guide

This guide helps AI agents understand the Noble.Salah codebase architecture, patterns, and workflows.

## Core Architecture

### Project Structure
- **Noble.Salah.UI**: MAUI Blazor application (desktop/mobile)
- **Noble.Salah.UI.Web**: Blazor WebAssembly (web version)
- **Noble.Salah.UI.Shared**: Common UI components
- **Noble.Salah.Common**: Core interfaces and models
- **Noble.Salah.Integration**: Core services and external integrations
- **Noble.Salah.AppHost**: .NET Aspire host
- **Noble.Salah.ServiceDefaults**: Shared configurations

### Key Patterns

1. **Service Architecture**
   - Define interfaces in `Noble.Salah.Common/Interfaces`
   - Implement core logic in `Noble.Salah.Integration/Services`
   - Platform-specific implementations in respective UI projects
   - Example:
     ```csharp
     // Common interface
     public interface IPrayerService {
         void UpdateConfigs(double latitude, double longitude, string timeZoneId...);
         IList<PrayerModel> GetTodayPrayers();
     }
     
     // Platform registration (MAUI)
     services.AddSingleton<IPrayerService, PrayerService>();
     services.AddSingleton<ILocationService, MauiLocationService>();
     ```

2. **UI Component Structure**
   - Shared components in `Noble.Salah.UI.Shared/Components`
   - Use MudBlazor Material Design components
   - Handle state changes via parameters and events
   - Example: `PrayerCarousel.razor` component for prayer time display

3. **Data Flow**
   - Prayer calculations: Batoulapps.Adhan library → PrayerService → UI
   - Location: Platform GPS/Browser → LocationService → PrayerService
   - Settings: LocalStorage → Service configuration

## Development Workflows

### Prayer Time Implementation
1. Update prayer calculation in `PrayerService.cs`
2. Implement platform-specific location services
3. Update UI components to display prayer times
4. Test across all platforms (Web, Windows, Android)

### Adding New Features
1. Define interfaces in `Noble.Salah.Common`
2. Implement core logic in `Noble.Salah.Integration`
3. Add UI components to `Noble.Salah.UI.Shared`
4. Register services in respective platform projects

### Cross-Platform Development
- Use `IFormFactor` for platform-specific behavior
- Implement platform services in respective UI projects
- Test on all target platforms
- Consider PWA capabilities for web version

## Conventions

### Code Organization
- Services: Singleton for global state (prayers, location)
- Components: Parameters for data, events for actions
- Models: Immutable where possible, validate in constructors
- Use enums for prayer names, calculation methods

### State Management
- Cache location data with expiry (1 hour)
- Persist settings in local storage
- Update prayer times on location/settings change
- Use timers for countdown updates

## Integration Points

### External Dependencies
- Batoulapps.Adhan: Prayer time calculations
- MudBlazor: UI components
- Location Services: Platform GPS/Browser geolocation
- LocalStorage: Settings persistence

### Cross-Component Communication
- Service injection for global state
- Parameters/events for component interaction
- LocalStorage for persistent settings
- Timer-based UI updates for prayer times
