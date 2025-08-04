# Noble Salah - Immediate Tasks (Next 2 Weeks)

## 🎯 **Week 1: Foundation Completion**

### **Day 1-2: Fix MudBlazor Warnings**

#### **Task 1.1: Update MudBlazor Package**
```bash
# Update MudBlazor to latest version
dotnet add Noble.Salah.UI/Noble.Salah.UI.Shared/Noble.Salah.UI.Shared.csproj package MudBlazor --version 8.5.1
dotnet add Noble.Salah.UI.Web/Noble.Salah.UI.Web.csproj package MudBlazor --version 8.5.1
```

#### **Task 1.2: Fix Attribute Naming Conventions**
**Files to Update:**
- `Noble.Salah.UI.Shared/Components/PrayerCarousel.razor`
- `Noble.Salah.UI.Shared/Pages/Home.razor`
- `Noble.Salah.UI.Shared/Pages/Settings.razor`

**Changes Needed:**
```csharp
// OLD (deprecated)
<MudCarousel ShowArrows="true" ShowBullets="true" ShowDelimiters="true" DelimiterIcon="@Icons.Material.Filled.Circle">

// NEW (current)
<MudCarousel ShowArrows="true" ShowBullets="true" Show-Delimiters="true" Delimiter-Icon="@Icons.Material.Filled.Circle">
```

#### **Task 1.3: Add Error Handling**
**File: `Noble.Salah.Integration/Services/PrayerService.cs`**
```csharp
public PrayerTimesModel GetPrayerTimes(DateTime date)
{
    try
    {
        // Validate coordinates
        if (_coordinates.Latitude == 0 && _coordinates.Longitude == 0)
        {
            throw new InvalidOperationException("Location coordinates not set");
        }

        var paramsConfig = CalculationMethodExtensions.GetParameters(_method);
        paramsConfig.Madhab = _madhab;

        var dateComponents = new DateComponents(date.Year, date.Month, date.Day);
        var prayerTimes = new PrayerTimes(_coordinates, dateComponents, paramsConfig);

        return new PrayerTimesModel(/* existing parameters */);
    }
    catch (Exception ex)
    {
        // Log error and return default prayer times
        Console.WriteLine($"Error calculating prayer times: {ex.Message}");
        return GetDefaultPrayerTimes(date);
    }
}
```

### **Day 3-4: Complete Theme System**

#### **Task 1.4: Enhance Theme Manager**
**File: `Noble.Salah.Common/Models/ThemeManager.cs`** (Create new)
```csharp
public class ThemeManager
{
    public AppTheme CurrentTheme { get; private set; } = AppTheme.System;
    public bool IsDarkMode { get; private set; }
    
    public event Action? ThemeChanged;
    
    public async Task SetThemeAsync(AppTheme theme, ILocalStorage localStorage)
    {
        CurrentTheme = theme;
        await localStorage.SaveAsync("AppTheme", theme.ToString());
        ThemeChanged?.Invoke();
    }
    
    public async Task LoadThemeAsync(ILocalStorage localStorage)
    {
        var savedTheme = await localStorage.GetAsync<string>("AppTheme");
        if (Enum.TryParse<AppTheme>(savedTheme, out var theme))
        {
            CurrentTheme = theme;
        }
    }
}
```

#### **Task 1.5: Add Ramadan Theme**
**File: `Noble.Salah.Common/Models/RamadanTheme.cs`** (Create new)
```csharp
public static class RamadanTheme
{
    public static MudTheme GetRamadanTheme()
    {
        return new MudTheme()
        {
            Palette = new PaletteLight()
            {
                Primary = "#6B46C1", // Purple
                Secondary = "#F59E0B", // Orange
                Background = "#1F2937",
                Surface = "#374151",
                AppbarBackground = "#6B46C1",
                DrawerBackground = "#374151",
                DrawerText = "#F9FAFB"
            },
            PaletteDark = new PaletteDark()
            {
                Primary = "#8B5CF6", // Light Purple
                Secondary = "#FBBF24", // Light Orange
                Background = "#111827",
                Surface = "#1F2937",
                AppbarBackground = "#8B5CF6",
                DrawerBackground = "#1F2937",
                DrawerText = "#F9FAFB"
            }
        };
    }
}
```

#### **Task 1.6: Update MainLayout for Theme Persistence**
**File: `Noble.Salah.UI.Shared/Layout/MainLayout.razor`**
```csharp
@inject ILocalStorage LocalStorage
@inject ThemeManager ThemeManager

@code {
    protected override async Task OnInitializedAsync()
    {
        await ThemeManager.LoadThemeAsync(LocalStorage);
        ThemeManager.ThemeChanged += StateHasChanged;
    }
    
    private async Task ToggleTheme()
    {
        var newTheme = _currentTheme switch
        {
            AppTheme.Light => AppTheme.Dark,
            AppTheme.Dark => AppTheme.Ramadan,
            AppTheme.Ramadan => AppTheme.System,
            AppTheme.System => AppTheme.Light,
            _ => AppTheme.System
        };
        
        await ThemeManager.SetThemeAsync(newTheme, LocalStorage);
        await SetTheme(newTheme);
    }
}
```

### **Day 5: Add Input Validation**

#### **Task 1.7: Create Validation Service**
**File: `Noble.Salah.Common/Services/ValidationService.cs`** (Create new)
```csharp
public static class ValidationService
{
    public static bool IsValidLatitude(double latitude)
    {
        return latitude >= -90 && latitude <= 90;
    }
    
    public static bool IsValidLongitude(double longitude)
    {
        return longitude >= -180 && longitude <= 180;
    }
    
    public static bool IsValidTimezone(string timezoneId)
    {
        try
        {
            TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
```

## 🎯 **Week 2: RTL Support & Settings Enhancement**

### **Day 1-2: Implement RTL Support**

#### **Task 2.1: Create Language Resources**
**File: `Noble.Salah.UI.Shared/Resources/Messages.ar.resx`** (Update existing)
```xml
<?xml version="1.0" encoding="utf-8"?>
<root>
  <data name="PrayerTimes" xml:space="preserve">
    <value>أوقات الصلاة</value>
  </data>
  <data name="Settings" xml:space="preserve">
    <value>الإعدادات</value>
  </data>
  <data name="Calendar" xml:space="preserve">
    <value>التقويم</value>
  </data>
  <data name="Fajr" xml:space="preserve">
    <value>الفجر</value>
  </data>
  <data name="Dhuhr" xml:space="preserve">
    <value>الظهر</value>
  </data>
  <data name="Asr" xml:space="preserve">
    <value>العصر</value>
  </data>
  <data name="Maghrib" xml:space="preserve">
    <value>المغرب</value>
  </data>
  <data name="Isha" xml:space="preserve">
    <value>العشاء</value>
  </data>
</root>
```

#### **Task 2.2: Create Language Service**
**File: `Noble.Salah.Common/Interfaces/ILanguageService.cs`** (Create new)
```csharp
public interface ILanguageService
{
    Task<string> GetCurrentLanguageAsync();
    Task SetLanguageAsync(string languageCode);
    Task<bool> IsRTLAsync();
    event Action? LanguageChanged;
}
```

#### **Task 2.3: Implement Language Service**
**File: `Noble.Salah.UI/Services/MauiLanguageService.cs`** (Create new)
```csharp
public class MauiLanguageService : ILanguageService
{
    private readonly ILocalStorage _localStorage;
    private string _currentLanguage = "en";
    
    public event Action? LanguageChanged;
    
    public MauiLanguageService(ILocalStorage localStorage)
    {
        _localStorage = localStorage;
    }
    
    public async Task<string> GetCurrentLanguageAsync()
    {
        var savedLanguage = await _localStorage.GetAsync<string>("Language");
        return savedLanguage ?? "en";
    }
    
    public async Task SetLanguageAsync(string languageCode)
    {
        _currentLanguage = languageCode;
        await _localStorage.SaveAsync("Language", languageCode);
        LanguageChanged?.Invoke();
    }
    
    public async Task<bool> IsRTLAsync()
    {
        var language = await GetCurrentLanguageAsync();
        return language == "ar" || language == "ur";
    }
}
```

#### **Task 2.4: Update MainLayout for RTL**
**File: `Noble.Salah.UI.Shared/Layout/MainLayout.razor`**
```csharp
@inject ILanguageService LanguageService

@code {
    private bool _isRTL = false;
    
    protected override async Task OnInitializedAsync()
    {
        _isRTL = await LanguageService.IsRTLAsync();
        LanguageService.LanguageChanged += async () => 
        {
            _isRTL = await LanguageService.IsRTLAsync();
            StateHasChanged();
        };
    }
}
```

### **Day 3-4: Settings Enhancement**

#### **Task 2.5: Add Language Selection to Settings**
**File: `Noble.Salah.UI.Shared/Pages/Settings.razor`**
```csharp
@inject ILanguageService LanguageService

<MudCard>
    <MudCardHeader>
        <MudText Typo="Typo.h6">Language & Region</MudText>
    </MudCardHeader>
    <MudCardContent>
        <MudSelect T="string" Label="Language" @bind-Value="@selectedLanguage" 
                   Variant="Variant.Outlined" Margin="Margin.Dense">
            <MudSelectItem Value="@("en")">English</MudSelectItem>
            <MudSelectItem Value="@("ar")">العربية</MudSelectItem>
            <MudSelectItem Value="@("ur")">اردو</MudSelectItem>
            <MudSelectItem Value="@("hi")">हिन्दी</MudSelectItem>
        </MudSelect>
    </MudCardContent>
</MudCard>

@code {
    private string selectedLanguage = "en";
    
    protected override async Task OnInitializedAsync()
    {
        selectedLanguage = await LanguageService.GetCurrentLanguageAsync();
    }
    
    private async Task OnLanguageChanged()
    {
        await LanguageService.SetLanguageAsync(selectedLanguage);
    }
}
```

#### **Task 2.6: Add Theme Selection to Settings**
**File: `Noble.Salah.UI.Shared/Pages/Settings.razor`** (Add to existing)
```csharp
<MudCard>
    <MudCardHeader>
        <MudText Typo="Typo.h6">Appearance</MudText>
    </MudCardHeader>
    <MudCardContent>
        <MudSelect T="AppTheme" Label="Theme" @bind-Value="@selectedTheme" 
                   Variant="Variant.Outlined" Margin="Margin.Dense">
            <MudSelectItem Value="@AppTheme.Light">Light</MudSelectItem>
            <MudSelectItem Value="@AppTheme.Dark">Dark</MudSelectItem>
            <MudSelectItem Value="@AppTheme.Ramadan">Ramadan</MudSelectItem>
            <MudSelectItem Value="@AppTheme.System">System</MudSelectItem>
        </MudSelect>
    </MudCardContent>
</MudCard>
```

### **Day 5: Testing & Documentation**

#### **Task 2.7: Create Basic Unit Tests**
**File: `Noble.Salah.Tests/PrayerServiceTests.cs`** (Create new test project)
```csharp
[Fact]
public void GetPrayerTimes_WithValidCoordinates_ReturnsPrayerTimes()
{
    // Arrange
    var prayerService = new PrayerService();
    prayerService.UpdateConfigs(40.7128, -74.0060, "America/New_York");
    
    // Act
    var prayerTimes = prayerService.GetPrayerTimes(DateTime.Today);
    
    // Assert
    Assert.NotNull(prayerTimes);
    Assert.NotNull(prayerTimes.Prayers);
    Assert.Equal(6, prayerTimes.Prayers.Count); // 5 prayers + sunrise
}

[Fact]
public void GetPrayerTimes_WithInvalidCoordinates_ThrowsException()
{
    // Arrange
    var prayerService = new PrayerService();
    
    // Act & Assert
    Assert.Throws<InvalidOperationException>(() => 
        prayerService.GetPrayerTimes(DateTime.Today));
}
```

#### **Task 2.8: Update Documentation**
**File: `README.md`** (Update existing)
```markdown
## Recent Updates

### Version 1.1.0 (Current)
- ✅ Enhanced theme system with Ramadan mode
- ✅ Complete RTL support for Arabic/Urdu
- ✅ Improved error handling and validation
- ✅ Language selection in settings
- ✅ Fixed MudBlazor warnings

### Getting Started
1. Clone the repository
2. Run `dotnet restore` to restore packages
3. Select your target platform and run
4. Configure your location and preferences in Settings
```

## 📋 **Daily Task Checklist**

### **Week 1 Checklist:**
- [ ] **Day 1**: Update MudBlazor packages
- [ ] **Day 2**: Fix attribute naming conventions
- [ ] **Day 3**: Add error handling to PrayerService
- [ ] **Day 4**: Create ThemeManager and Ramadan theme
- [ ] **Day 5**: Add input validation service

### **Week 2 Checklist:**
- [ ] **Day 1**: Create Arabic language resources
- [ ] **Day 2**: Implement LanguageService
- [ ] **Day 3**: Add RTL support to MainLayout
- [ ] **Day 4**: Enhance Settings page
- [ ] **Day 5**: Create basic unit tests

## 🚀 **Success Criteria**

### **Week 1 Success:**
- [ ] No MudBlazor warnings in build
- [ ] Theme system works with persistence
- [ ] Error handling prevents crashes
- [ ] Input validation catches invalid data

### **Week 2 Success:**
- [ ] RTL layout works for Arabic/Urdu
- [ ] Language switching works
- [ ] Settings page has all options
- [ ] Basic unit tests pass

## 📝 **Notes**

1. **Build After Each Task**: Test that the application still builds and runs
2. **Cross-Platform Testing**: Test changes on both MAUI and Blazor WASM
3. **User Experience**: Ensure changes don't break existing functionality
4. **Performance**: Monitor that changes don't impact prayer calculation speed
5. **Documentation**: Update relevant documentation as you go

---

*These tasks build upon the existing solid foundation and complete the core UI/UX features needed for a production-ready application.* 