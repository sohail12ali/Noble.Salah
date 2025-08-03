# Project Configuration Guidelines

## .NET Aspire Configuration

### 1. AppHost Setup

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add Aspire services
builder.AddServiceDefaults();

// Add application services
builder.Services.AddPrayerService();
builder.Services.AddLocationService();

// Configure distributed caching
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// Add monitoring
builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddMeter("Noble.Salah.Metrics");
    })
    .WithTracing(tracing =>
    {
        tracing.AddSource("Noble.Salah.Traces");
    });
```

### 2. Service Configuration

```json
{
  "ConnectionStrings": {
    "Redis": "localhost:6379",
    "ApplicationInsights": "your-key-here"
  },
  "PrayerSettings": {
    "DefaultCalculationMethod": "MUSLIM_WORLD_LEAGUE",
    "DefaultSchool": "SHAFI"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Environment-Specific Configuration

### 1. Development

```json
{
  "DetailedErrors": true,
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information"
    }
  }
}
```

### 2. Production

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Error"
    }
  },
  "AllowedHosts": "*",
  "Azure": {
    "SignalR": {
      "Enabled": "true"
    }
  }
}
```

## Platform-Specific Configuration

### 1. MAUI Configuration

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
    <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
</manifest>
```

### 2. Web Configuration

```json
{
  "PWA": {
    "EnableServiceWorker": true,
    "CacheFiles": [
      "/css/app.css",
      "/js/app.js"
    ]
  }
}
```

## Service Registration

### 1. Common Services

```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonServices(
        this IServiceCollection services)
    {
        services.AddScoped<IPrayerService, PrayerService>();
        services.Configure<PrayerSettings>(
            services.BuildServiceProvider()
                .GetService<IConfiguration>()
                .GetSection("PrayerSettings"));
        
        return services;
    }
}
```

### 2. Platform Services

```csharp
// MAUI
services.AddScoped<ILocationService, MauiLocationService>();

// Web
services.AddScoped<ILocationService, BrowserLocationService>();
```

## Monitoring Setup

### 1. Application Insights

```csharp
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = 
        builder.Configuration["ApplicationInsights:ConnectionString"];
});
```

### 2. OpenTelemetry

```csharp
var meters = new[] { "Noble.Salah.Metrics" };
var sources = new[] { "Noble.Salah.Traces" };

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(serviceName: "Noble.Salah"))
    .WithMetrics(metrics => metrics
        .AddMeter(meters)
        .AddOtlpExporter())
    .WithTracing(tracing => tracing
        .AddSource(sources)
        .AddOtlpExporter());
```

## Error Handling

### 1. Global Error Handler

```csharp
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandler = 
            context.Features.Get<IExceptionHandlerPathFeature>();
        
        // Log error
        logger.LogError(exceptionHandler.Error, 
            "Error handling request: {Path}", 
            exceptionHandler.Path);
        
        // Return error response
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new
        {
            error = "An error occurred processing your request"
        });
    });
});
```

### 2. Validation

```csharp
services.AddValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<Program>();
});
```
