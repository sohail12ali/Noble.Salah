using MudBlazor;
using Noble.Salah.Common.Enums;
using Noble.Salah.Common.Interfaces;

namespace Noble.Salah.UI.Shared.Services;

public class ThemeManager
{
    private readonly ILocalStorage? _localStorage;
    private const string ThemeKey = "App.SelectedTheme";
    public MudTheme Theme { get; private set; }
    public AppTheme CurrentTheme { get; private set; } = AppTheme.System;
    public event Action<MudTheme>? ThemeChanged;

    // Default constructor for DI (legacy)
    public ThemeManager() { Theme = GetDefaultTheme(); }

    // New constructor for DI with ILocalStorage
    public ThemeManager(ILocalStorage localStorage)
    {
        _localStorage = localStorage;
        Theme = GetDefaultTheme();
        // Remove async call from constructor to avoid issues
    }

    /// <summary>
    /// Initializes the theme manager asynchronously
    /// </summary>
    public async Task InitializeAsync()
    {
        await LoadThemeFromStorageAsync();
    }

    public MudTheme GetTheme(AppTheme appTheme)
    {
        return appTheme switch
        {
            AppTheme.System => GetDefaultTheme(),
            AppTheme.LightDefault => GetDefaultTheme(),
            AppTheme.DarkDefault => GetDarkTheme(),
            AppTheme.OceanBreeze => OceanBreeze,
            AppTheme.DesertSunset => DesertSunset,
            AppTheme.NightSky => NightSky,
            AppTheme.MorningDew => MorningDew,
            AppTheme.GoldenMosque => GoldenMosque,
            AppTheme.RoyalPurple => RoyalPurple,
            AppTheme.SerenityBlue => SerenityBlue,
            AppTheme.ForestHarmony => ForestHarmony,
            _ => GetDefaultTheme()
        };
    }

    public MudTheme ApplyTheme(AppTheme appTheme)
    {
        var theme = GetTheme(appTheme);
        SetTheme(theme);
        CurrentTheme = appTheme;
        _ = SaveThemeToStorageAsync(appTheme);
        return theme;
    }

    public void SetTheme(MudTheme theme)
    {
        Theme = theme;
        ThemeChanged?.Invoke(theme);
    }

    private async Task SaveThemeToStorageAsync(AppTheme appTheme)
    {
        if (_localStorage != null)
            await _localStorage.SaveAsync(ThemeKey, appTheme.ToString());
    }

    private async Task LoadThemeFromStorageAsync()
    {
        if (_localStorage == null) return;
        var themeStr = await _localStorage.LoadAsync<string>(ThemeKey);
        if (!string.IsNullOrEmpty(themeStr) && Enum.TryParse<AppTheme>(themeStr, out var savedTheme))
        {
            CurrentTheme = savedTheme;
            SetTheme(GetTheme(savedTheme));
        }
    }

    public static MudTheme GetDefaultTheme() => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#1976D2",         // Material Blue
            Secondary = "#424242",       // Neutral Gray
            Background = "#FFFFFF",
            Surface = "#FFFFFF",
            AppbarBackground = "#1976D2",
            AppbarText = "#FFFFFF",
            TextPrimary = "#212121",
            TextSecondary = "#757575",
            DrawerBackground = "#FFFFFF",
            DrawerText = "#424242",
            ActionDefault = "#757575",
            ActionDisabled = "#BDBDBD",
            ActionDisabledBackground = "#F5F5F5"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#90CAF9",         // Light Blue
            Secondary = "#B0BEC5",       // Blue Grey
            Background = "#121212",
            Surface = "#1E1E1E",
            AppbarBackground = "#1E1E1E",
            AppbarText = "#FFFFFF",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#B0BEC5",
            DrawerBackground = "#1A1A1A",
            DrawerText = "#FFFFFF",
            ActionDefault = "#B0BEC5",
            ActionDisabled = "#545454",
            ActionDisabledBackground = "#2C2C2C"
        }
    };

    public static MudTheme GetDarkTheme() => new()
    {
        PaletteDark = new PaletteDark
        {
            Primary = "#BB86FC",         // Material Dark Purple
            Secondary = "#03DAC6",       // Material Dark Teal
            Background = "#121212",
            Surface = "#1E1E1E",
            AppbarBackground = "#1E1E1E",
            AppbarText = "#FFFFFF",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#B0BEC5",
            DrawerBackground = "#1A1A1A",
            DrawerText = "#FFFFFF",
            ActionDefault = "#BB86FC",
            ActionDisabled = "#545454",
            ActionDisabledBackground = "#2C2C2C"
        }
    };

    public static MudTheme OceanBreeze => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#00838F",         // Cyan 800
            Secondary = "#4FB3BF",       // Lighter Cyan
            Background = "#F5F9FA",      // Very Light Cyan
            Surface = "#FFFFFF",
            AppbarBackground = "#00838F",
            AppbarText = "#FFFFFF",
            TextPrimary = "#263238",     // Blue Grey 900
            TextSecondary = "#546E7A",   // Blue Grey 600
            DrawerBackground = "#FFFFFF",
            DrawerText = "#263238"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#4FB3BF",         // Light Cyan
            Secondary = "#80DEEA",       // Cyan 200
            Background = "#0A2129",      // Very Dark Cyan
            Surface = "#0F3441",         // Dark Cyan
            AppbarBackground = "#0A2129",
            AppbarText = "#FFFFFF",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#B0BEC5",   // Blue Grey 200
            DrawerBackground = "#0F3441",
            DrawerText = "#FFFFFF"
        }
    };

    public static MudTheme DesertSunset => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#FF8A65",         // Deep Orange 300
            Secondary = "#FFB74D",       // Orange 300
            Background = "#FFF8F1",      // Very Light Orange
            Surface = "#FFFFFF",
            AppbarBackground = "#FF8A65",
            AppbarText = "#2E1810",      // Very Dark Orange
            TextPrimary = "#3E2723",     // Brown 900
            TextSecondary = "#5D4037",   // Brown 700
            DrawerBackground = "#FFFFFF",
            DrawerText = "#3E2723"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#FFB74D",         // Orange 300
            Secondary = "#FFCC80",       // Orange 200
            Background = "#2E1810",      // Very Dark Orange
            Surface = "#3E2216",         // Dark Orange Brown
            AppbarBackground = "#2E1810",
            AppbarText = "#FFFFFF",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#FFCCBC",   // Deep Orange 100
            DrawerBackground = "#3E2216",
            DrawerText = "#FFFFFF"
        }
    };

    public static MudTheme NightSky => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#5C6BC0",         // Indigo 400
            Secondary = "#B39DDB",       // Deep Purple 200
            Background = "#F5F6FA",      // Very Light Indigo
            Surface = "#FFFFFF",
            AppbarBackground = "#5C6BC0",
            AppbarText = "#FFFFFF",
            TextPrimary = "#1A237E",     // Indigo 900
            TextSecondary = "#303F9F",   // Indigo 700
            DrawerBackground = "#FFFFFF",
            DrawerText = "#1A237E"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#7986CB",         // Indigo 300
            Secondary = "#B39DDB",       // Deep Purple 200
            Background = "#0D1117",      // Very Dark Blue
            Surface = "#161B22",         // Dark Blue Grey
            AppbarBackground = "#0D1117",
            AppbarText = "#FFFFFF",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#C5CAE9",   // Indigo 100
            DrawerBackground = "#161B22",
            DrawerText = "#FFFFFF"
        }
    };

    public static MudTheme MorningDew => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#66BB6A",         // Green 400
            Secondary = "#81C784",       // Green 300
            Background = "#F4F9F4",      // Very Light Green
            Surface = "#FFFFFF",
            AppbarBackground = "#66BB6A",
            AppbarText = "#1B5E20",      // Green 900
            TextPrimary = "#1B5E20",     // Green 900
            TextSecondary = "#2E7D32",   // Green 800
            DrawerBackground = "#FFFFFF",
            DrawerText = "#1B5E20"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#81C784",         // Green 300
            Secondary = "#A5D6A7",       // Green 200
            Background = "#0D1F0D",      // Very Dark Green
            Surface = "#132913",         // Dark Green
            AppbarBackground = "#0D1F0D",
            AppbarText = "#FFFFFF",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#C8E6C9",   // Green 100
            DrawerBackground = "#132913",
            DrawerText = "#FFFFFF"
        }
    };

    public static MudTheme GoldenMosque => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#D4AF37",         // Royal Gold
            Secondary = "#26A69A",       // Teal 400
            Background = "#FDF9EE",      // Very Light Gold
            Surface = "#FFFFFF",
            AppbarBackground = "#D4AF37",
            AppbarText = "#000000",
            TextPrimary = "#000000",
            TextSecondary = "#5D4037",   // Brown 700
            DrawerBackground = "#FFFFFF",
            DrawerText = "#000000"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#FFD700",         // Gold
            Secondary = "#26A69A",       // Teal 400
            Background = "#121212",      // Very Dark Grey
            Surface = "#1D1D1D",         // Dark Grey
            AppbarBackground = "#121212",
            AppbarText = "#FFD700",
            TextPrimary = "#FFD700",
            TextSecondary = "#B2A25A",   // Muted Gold
            DrawerBackground = "#1D1D1D",
            DrawerText = "#FFD700"
        }
    };

    public static MudTheme RoyalPurple => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#7B1FA2",         // Purple 700
            Secondary = "#FFD700",       // Gold
            Background = "#F6F2F9",      // Very Light Purple
            Surface = "#FFFFFF",
            AppbarBackground = "#7B1FA2",
            AppbarText = "#FFFFFF",
            TextPrimary = "#4A148C",     // Purple 900
            TextSecondary = "#6A1B9A",   // Purple 800
            DrawerBackground = "#FFFFFF",
            DrawerText = "#4A148C"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#AB47BC",         // Purple 400
            Secondary = "#FFD700",       // Gold
            Background = "#1A0F2B",      // Very Dark Purple
            Surface = "#2D1B44",         // Dark Purple
            AppbarBackground = "#1A0F2B",
            AppbarText = "#FFFFFF",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#E1BEE7",   // Purple 100
            DrawerBackground = "#2D1B44",
            DrawerText = "#FFFFFF"
        }
    };

    public static MudTheme SerenityBlue => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#1E88E5",         // Blue 600
            Secondary = "#64B5F6",       // Blue 300
            Background = "#F3F8FD",      // Very Light Blue
            Surface = "#FFFFFF",
            AppbarBackground = "#1E88E5",
            AppbarText = "#FFFFFF",
            TextPrimary = "#0D47A1",     // Blue 900
            TextSecondary = "#1565C0",   // Blue 800
            DrawerBackground = "#FFFFFF",
            DrawerText = "#0D47A1"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#64B5F6",         // Blue 300
            Secondary = "#90CAF9",       // Blue 200
            Background = "#0A1929",      // Very Dark Blue
            Surface = "#102A43",         // Dark Blue
            AppbarBackground = "#0A1929",
            AppbarText = "#FFFFFF",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#BBDEFB",   // Blue 100
            DrawerBackground = "#102A43",
            DrawerText = "#FFFFFF"
        }
    };

    public static MudTheme ForestHarmony => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#2E7D32",         // Green 800
            Secondary = "#795548",       // Brown 500
            Background = "#F5F8F5",      // Very Light Green
            Surface = "#FFFFFF",
            AppbarBackground = "#2E7D32",
            AppbarText = "#FFFFFF",
            TextPrimary = "#1B5E20",     // Green 900
            TextSecondary = "#33691E",   // Light Green 900
            DrawerBackground = "#FFFFFF",
            DrawerText = "#1B5E20"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#66BB6A",         // Green 400
            Secondary = "#8D6E63",       // Brown 400
            Background = "#0F1F13",      // Very Dark Forest Green
            Surface = "#1B2921",         // Dark Forest Green
            AppbarBackground = "#0F1F13",
            AppbarText = "#FFFFFF",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#C8E6C9",   // Green 100
            DrawerBackground = "#1B2921",
            DrawerText = "#FFFFFF"
        }
    };
}