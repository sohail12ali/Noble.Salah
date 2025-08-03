# Architecture Guidelines for Noble.Salah

## Project Structure

Noble.Salah follows a clean architecture pattern with these key projects:

```
Noble.Salah/
├── Noble.Salah.UI/              # MAUI Blazor UI (Mobile/Desktop)
├── Noble.Salah.UI.Web/          # Blazor WebAssembly (Browser)
├── Noble.Salah.UI.Shared/       # Shared UI Components
├── Noble.Salah.Common/          # Core Interfaces & Models
├── Noble.Salah.Integration/     # External Services Integration
├── Noble.Salah.AppHost/         # .NET Aspire Host
└── Noble.Salah.ServiceDefaults/ # Shared Configurations
```

## Key Architectural Principles

1. **Separation of Concerns**
   - UI logic stays in UI projects
   - Business logic goes in Common/Integration
   - Configuration in ServiceDefaults

2. **Interface-Based Design**
   - Define interfaces in Common
   - Implement in appropriate projects
   - Example: `IPrayerService`, `ILocationService`

3. **Cross-Platform Considerations**
   - Platform-specific code in respective UI projects
   - Shared logic in Common/Integration
   - Use dependency injection for platform services

4. **Service Pattern**
   - Services implement interfaces from Common
   - Platform-specific implementations in UI projects
   - Core implementations in Integration

## Code Organization

### Models and Interfaces
```csharp
// Place in Noble.Salah.Common
namespace Noble.Salah.Common.Models
{
    public class PrayerModel
    {
        public PrayerName PrayerName { get; set; }
        public DateTime PrayerTime { get; set; }
        // ...
    }
}
```

### Service Implementation
```csharp
// Place in Noble.Salah.Integration
namespace Noble.Salah.Integration.Services
{
    public class PrayerService : IPrayerService
    {
        // Implementation details
    }
}
```

### UI Components
```csharp
// Place in Noble.Salah.UI.Shared
namespace Noble.Salah.UI.Shared.Components
{
    public partial class PrayerTimeDisplay
    {
        // Component implementation
    }
}
```

## Best Practices

1. **Dependency Injection**
   - Register services in appropriate projects
   - Use constructor injection
   - Avoid service locator pattern

2. **Error Handling**
   - Define custom exceptions in Common
   - Handle platform-specific errors in UI
   - Use consistent logging patterns

3. **Configuration**
   - Use appsettings.json for config
   - Override in platform-specific projects
   - Use options pattern for complex configs

4. **Testing**
   - Unit tests for services
   - UI tests for components
   - Integration tests for end-to-end flows
