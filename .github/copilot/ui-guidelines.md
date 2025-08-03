# UI Development Guidelines

## Technology Stack

- **MAUI Blazor**: Cross-platform UI framework
- **MudBlazor**: Material Design components
- **Blazor WebAssembly**: Web platform support

## Component Architecture

### 1. Shared Components

Place reusable components in `Noble.Salah.UI.Shared`:

```razor
@namespace Noble.Salah.UI.Shared.Components

<MudCard>
    <MudCardHeader>
        <MudText Typo="Typo.h6">@PrayerName</MudText>
    </MudCardHeader>
    <MudCardContent>
        <MudText>@FormattedTime</MudText>
    </MudCardContent>
</MudCard>

@code {
    [Parameter] public string PrayerName { get; set; }
    [Parameter] public DateTime Time { get; set; }
    
    private string FormattedTime => Time.ToString("HH:mm");
}
```

### 2. Platform-Specific Components

Handle platform differences using dependency injection:

```csharp
public interface ILocationService
{
    Task<(double Latitude, double Longitude)?> GetCurrentLocationAsync();
}

// MAUI Implementation
public class MauiLocationService : ILocationService
{
    public async Task<(double Latitude, double Longitude)?> GetCurrentLocationAsync()
    {
        // MAUI-specific implementation
    }
}

// Web Implementation
public class BrowserLocationService : ILocationService
{
    public async Task<(double Latitude, double Longitude)?> GetCurrentLocationAsync()
    {
        // Browser-specific implementation
    }
}
```

## Layout Guidelines

### 1. Responsive Design

```razor
<MudGrid>
    <MudItem xs="12" sm="6" md="4">
        <!-- Content adapts to screen size -->
    </MudItem>
</MudGrid>
```

### 2. Theme Support

```csharp
public class ThemeProvider
{
    public MudTheme Theme => new MudTheme
    {
        Palette = new PaletteLight
        {
            Primary = "#00695C",
            Secondary = "#FFA000",
            Background = "#FAFAFA"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#4DB6AC",
            Secondary = "#FFD54F",
            Background = "#121212"
        }
    };
}
```

### 3. RTL Support

```razor
<MudRTLProvider RightToLeft="@IsRTL">
    <!-- RTL-aware content -->
</MudRTLProvider>
```

## State Management

### 1. Component State

```csharp
@implements IDisposable

private Timer _timer;
private PrayerTimesModel _prayerTimes;

protected override void OnInitialized()
{
    _timer = new Timer(UpdateTimes, null, 0, 60000);
}

public void Dispose()
{
    _timer?.Dispose();
}
```

### 2. Application State

```csharp
public class AppState
{
    public event Action OnChange;
    private PrayerTimesModel _currentPrayerTimes;

    public PrayerTimesModel CurrentPrayerTimes
    {
        get => _currentPrayerTimes;
        set
        {
            _currentPrayerTimes = value;
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
```

## Performance Guidelines

1. **Component Optimization**
   - Use `@key` directive for lists
   - Implement `ShouldRender()`
   - Avoid unnecessary renders

2. **Resource Management**
   - Dispose of timers and subscriptions
   - Cache heavy computations
   - Lazy load components when possible

3. **Loading States**
   - Show loading indicators
   - Handle errors gracefully
   - Provide feedback for async operations

## Accessibility

1. **ARIA Attributes**
   ```razor
   <MudButton aria-label="Next prayer">
       <MudIcon Icon="@Icons.Material.Filled.Schedule" />
   </MudButton>
   ```

2. **Keyboard Navigation**
   ```razor
   <MudNavMenu>
       <MudNavLink Href="/" tabindex="0">Home</MudNavLink>
   </MudNavMenu>
   ```

3. **Color Contrast**
   - Use MudBlazor's built-in contrast checking
   - Support high contrast mode
   - Test with screen readers

## Testing

1. **Component Testing**
   ```csharp
   [Fact]
   public void PrayerTimeDisplay_ShowsCorrectTime()
   {
       // Arrange
       var cut = ctx.RenderComponent<PrayerTimeDisplay>(parameters => parameters
           .Add(p => p.Time, new DateTime(2024, 1, 1, 12, 0, 0))
       );

       // Act
       var timeDisplay = cut.Find(".time-display");

       // Assert
       timeDisplay.TextContent.Should().Be("12:00");
   }
   ```

2. **Integration Testing**
   ```csharp
   [Fact]
   public async Task LocationService_GetsCurrent()
   {
       // Test location service integration
   }
   ```
