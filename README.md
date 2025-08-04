# Noble.Salah

Noble.Salah is a cross-platform application built with .NET MAUI Blazor and MudBlazor for calculating and displaying Islamic prayer times. The application supports multiple platforms including Windows, Android, iOS, and macOS.

## Features

- Real-time prayer time calculations using the Adhan library
- Location-based prayer times
- Support for different calculation methods
- Modern UI using MudBlazor components
- Cross-platform support (Windows, Android, iOS, macOS)
- Progressive Web App (PWA) support

## Project Structure

The solution consists of several projects:

- **Noble.Salah.UI**: MAUI Blazor application (Main UI for desktop and mobile)
- **Noble.Salah.UI.Web**: Blazor WebAssembly project (Web version)
- **Noble.Salah.UI.Shared**: Shared UI components and resources
- **Noble.Salah.Common**: Core interfaces and models
- **Noble.Salah.Integration**: Integration with Adhan library for prayer calculations
- **Noble.Salah.AppHost**: .NET Aspire host application
- **Noble.Salah.ServiceDefaults**: Shared service configurations

## Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or later with:
  - .NET MAUI workload
  - ASP.NET and web development workload
  - .NET desktop development workload
- For iOS/macOS development:
  - Mac with XCode installed
  - Visual Studio for Mac (optional)

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/sohail12ali/Noble.Salah.git
   ```

2. Open the solution in Visual Studio 2022:
   ```bash
   cd Noble.Salah
   start Noble.Salah.sln
   ```

3. Select your target platform (Windows, Android, iOS, or Browser)

4. Build and run the application

## Development

The application uses the following key technologies:

- **.NET MAUI Blazor**: For cross-platform native UI
- **MudBlazor**: For Material Design components
- **Adhan**: For accurate prayer time calculations
- **.NET Aspire**: For cloud-native application support
- **OpenTelemetry**: For observability

## Architecture Overview

The application follows a clean architecture pattern with separation of concerns between:
- Presentation layer (UI projects)
- Application logic (Common and Integration projects)
- Data access (ServiceDefaults for configuration)

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/YourFeature`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Push to the branch (`git push origin feature/YourFeature`)
5. Create a new Pull Request

## License

This project is licensed under the MIT License - see the LICENSE.txt file for details.

---
description: Best practices for .NET MAUI Blazor development in this project
---

This rule provides guidelines for developing with .NET MAUI Blazor in the Noble.Salah project:

1. **Use MudBlazor Components**: Leverage MudBlazor's Material Design components for consistent UI across platforms
2. **Follow Clean Architecture**: Maintain separation between UI, business logic, and data layers
3. **Implement Proper State Management**: Use Blazor's state management patterns for prayer time calculations
4. **Optimize for Mobile**: Ensure responsive design that works well on both mobile and desktop
5. **Test Cross-Platform**: Verify functionality across Windows, Android, iOS, and Web platforms
6. **Use Adhan Library**: Utilize the Adhan library for accurate prayer time calculations
7. **Implement PWA Features**: Enable Progressive Web App capabilities for web deployment
8. **Follow .NET Aspire Patterns**: Use the AppHost and ServiceDefaults projects for cloud-native deployment

# Noble Salah Website
Our Web Site is deployed on **https://huggingface.co/spaces/Sohail-Ali/noblesalah**