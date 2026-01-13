# VolleMoehre App - Uno Platform 6.4

This directory contains the migrated VolleMoehre application using Uno Platform 6.4 with the new single-project structure.

## Project Structure

The app now uses the modern single-project structure introduced in Uno Platform 5.0+ and refined in 6.x:

- **VolleMoehre.App/** - Main application project
  - **Platforms/** - Platform-specific code and resources
    - **Android/** - Android-specific files
    - **iOS/** - iOS-specific files
    - **Desktop/** - Windows/macOS/Linux desktop files
    - **WebAssembly/** - WebAssembly-specific files
  - **Pages/** - Application pages (XAML + code-behind)
  - **ViewModels/** - View models for MVVM pattern
  - **Services/** - Business logic services
  - **Commands/** - ICommand implementations
  - **Converter/** - Value converters for XAML bindings
  - **Assets/** - Shared assets (images, fonts, etc.)
  - **Strings/** - Localized resources

## Key Changes from Previous Version

### 1. Single Project Structure
- Replaced the old Shared Project (.shproj) with a single multi-targeted project
- All platform-specific projects (Mobile, UWP, Wasm, iOS, Droid) consolidated into one
- Platform-specific code is now in the Platforms folder

### 2. Uno Platform 6.4
- Updated from Uno Platform 3.x/4.x to 6.4
- Uses Uno.Sdk instead of Microsoft.NET.Sdk
- WinUI 3 APIs (Microsoft.UI.Xaml) instead of UWP APIs (Windows.UI.Xaml)

### 3. .NET 10
- Upgraded to .NET 10 (from .NET 6)
- Target frameworks: net10.0-android, net10.0-ios, net10.0-browserwasm, net10.0-desktop

### 4. Central Package Management
- Uses Directory.Packages.props for centralized package version management
- More maintainable dependency management

### 5. Uno Material
- Configured to use Uno Material for consistent Material Design theming

## Building the Project

### Prerequisites
- .NET 10 SDK
- Visual Studio 2022 (17.13+) or Visual Studio Code with C# extension
- Platform-specific workloads:
  ```bash
  dotnet workload install android ios wasm-tools
  ```

### Build Commands
```bash
# Restore dependencies
dotnet restore

# Build for all platforms
dotnet build

# Build for specific platform
dotnet build -f net10.0-android
dotnet build -f net10.0-ios
dotnet build -f net10.0-browserwasm
dotnet build -f net10.0-desktop
```

### Run Commands
```bash
# Run on Android
dotnet run -f net10.0-android

# Run on iOS
dotnet run -f net10.0-ios

# Run on WebAssembly
dotnet run -f net10.0-browserwasm

# Run on Desktop (Windows/macOS/Linux)
dotnet run -f net10.0-desktop
```

## Migration Notes

### API Changes
- All `Windows.UI.Xaml` namespaces changed to `Microsoft.UI.Xaml`
- Window initialization simplified in Uno 6.x
- Logging initialization streamlined

### Known Issues
- Some platform-specific features may need testing
- Third-party libraries may need updates for compatibility

## Dependencies

See `Directory.Packages.props` for the complete list of dependencies. Key dependencies include:
- Uno.Sdk: 6.4.53
- PropertyChanged.Fody: For automatic INotifyPropertyChanged implementation
- Microsoft.AspNet.WebApi.Client: For HTTP API calls
- MonkeyCache.FileStore: For local data caching

## Related Projects

This app depends on the following projects (unchanged):
- VolleMoehre.Contracts - Shared contracts/models
- VolleMoehre.API - Backend API
- VolleMoehre.Adapter.LiteDB - Database adapter
- VolleMoehre.Adapter.Calender - Calendar integration
- VolleMoehre.Web - Web project
