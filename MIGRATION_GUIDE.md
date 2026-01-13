# Uno Platform Migration Guide - v3.x/4.x to v6.4

This document describes the migration of the VolleMoehre application from Uno Platform 3.x/4.x to Uno Platform 6.4 with the new single-project structure.

## Overview

The migration consolidates four separate projects into a single multi-targeted project:
- **VolleMoehre.App.Shared** (Shared project with all common code)
- **VolleMoehre.App.Mobile** (Android, iOS, macOS targets)
- **VolleMoehre.App.UWP** (Universal Windows Platform)
- **VolleMoehre.App.Wasm** (WebAssembly)

**New Structure**: **VolleMoehre.App/VolleMoehre.App** (Single multi-targeted project)

## What Changed

### 1. Project Structure

**Before:**
```
VolleMoehre.App/
├── VolleMoehre.App.Shared/ (Shared Project - .shproj)
│   ├── App.xaml
│   ├── Pages/
│   ├── ViewModels/
│   └── Services/
├── VolleMoehre.App.Mobile/ (Multi-target for Android/iOS/macOS)
├── VolleMoehre.App.UWP/ (Windows UWP)
├── VolleMoehre.App.Wasm/ (WebAssembly)
├── VolleMoehre.App.iOS/ (iOS - deprecated)
└── VolleMoehre.App.Droid/ (Android - deprecated)
```

**After:**
```
VolleMoehre.App/
├── global.json (Uno SDK version configuration)
├── Directory.Build.props (Build configuration)
├── Directory.Packages.props (Central package management)
└── VolleMoehre.App/ (Single Project - .csproj)
    ├── App.xaml
    ├── Pages/
    ├── ViewModels/
    ├── Services/
    ├── Commands/
    ├── Converter/
    ├── Assets/
    └── Platforms/ (Platform-specific code)
        ├── Android/
        ├── iOS/
        ├── Desktop/
        └── WebAssembly/
```

### 2. Uno Platform Version

- **Before**: Uno Platform 3.x/4.x (varied across projects)
- **After**: Uno Platform 6.4.53 (unified)

### 3. .NET Version

- **Before**: .NET 6 and .NET 10 (mixed)
- **After**: .NET 10 (unified)

### 4. Target Frameworks

- **Before**: 
  - Mobile: `net10.0-android`, `net10.0-ios`, `net10.0-maccatalyst`, `net10.0-macos`
  - Wasm: `net6.0`
  - UWP: .NET Framework UWP
  
- **After**: Single project with:
  - `net10.0-android`
  - `net10.0-ios`
  - `net10.0-browserwasm`
  - `net10.0-desktop` (Windows/macOS/Linux)

### 5. API Namespaces

All Windows.UI.Xaml namespaces have been replaced with Microsoft.UI.Xaml (WinUI 3 APIs):

**Before:**
```csharp
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
```

**After:**
```csharp
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
```

### 6. Project SDK

- **Before**: `Microsoft.NET.Sdk` or `Microsoft.NET.Sdk.Web`
- **After**: `Uno.Sdk` (provides all Uno Platform functionality)

### 7. Package Management

Now uses Central Package Management (CPM):
- Package versions defined in `Directory.Packages.props`
- Individual projects reference packages without specifying versions

### 8. Logging

Simplified logging initialization:

**Before:**
```csharp
var factory = LoggerFactory.Create(builder =>
{
    #if __WASM__
        builder.AddProvider(new Uno.Extensions.Logging.WebAssembly.WebAssemblyConsoleLoggerProvider());
    #elif __IOS__
        builder.AddProvider(new Uno.Extensions.Logging.OSLogLoggerProvider());
    // ... more platform-specific code
});
global::Uno.Extensions.LogExtensionPoint.AmbientLoggerFactory = factory;
```

**After:**
```csharp
#if DEBUG
    Uno.UI.Adapter.Microsoft.Extensions.Logging.LoggingAdapter.Initialize();
#endif
```

## Breaking Changes

### 1. Window Initialization

**Before:**
```csharp
#if NET6_0_OR_GREATER && WINDOWS
    _window = new Window();
    _window.Activate();
#else
    _window = Windows.UI.Xaml.Window.Current;
#endif
```

**After:**
```csharp
_window = Microsoft.UI.Xaml.Window.Current;
```

### 2. Namespace Updates

All code using `Windows.UI.Xaml` needs to use `Microsoft.UI.Xaml`:
- Frame navigation
- Controls
- Data binding
- Visual states
- Converters

### 3. Application Lifecycle

PrelaunchActivated checks are no longer needed in Uno 6.4:

**Before:**
```csharp
#if !(NET6_0_OR_GREATER && WINDOWS)
    if (args.PrelaunchActivated == false)
#endif
    {
        // Navigation code
    }
```

**After:**
```csharp
// Navigation code directly
```

## Migration Steps Performed

1. ✅ Created new single-project structure using Uno.Sdk
2. ✅ Copied all shared code from VolleMoehre.App.Shared
3. ✅ Copied platform-specific files to Platforms folder
4. ✅ Updated all namespace references from Windows.UI.Xaml to Microsoft.UI.Xaml
5. ✅ Simplified logging initialization
6. ✅ Updated Window initialization code
7. ✅ Created central package management configuration
8. ✅ Updated solution file
9. ✅ Moved old projects to VolleMoehre.App.Old (for reference)

## Building the New Project

### Prerequisites

```bash
# Install .NET 10 SDK
# Download from: https://dotnet.microsoft.com/download/dotnet/10.0

# Install required workloads
dotnet workload install android ios wasm-tools
```

### Build Commands

```bash
# Restore packages
dotnet restore

# Build all targets
dotnet build

# Build specific platform
dotnet build -f net10.0-android
dotnet build -f net10.0-ios
dotnet build -f net10.0-browserwasm
dotnet build -f net10.0-desktop

# Run on specific platform
dotnet run -f net10.0-desktop
```

## Uno Material

The project is configured to use Uno Material for consistent Material Design theming:

```xml
<UnoFeatures>
  Material;
</UnoFeatures>
```

This is defined in the project file and automatically includes Material Design resources.

## Dependencies Updated

Key package updates:
- Uno.Sdk: 6.4.53
- Fody: 6.8.2 (from 6.1.1/6.6.0)
- PropertyChanged.Fody: 4.1.0 (from 3.1.3/3.4.0)
- Microsoft.AspNet.WebApi.Client: 6.0.0 (from 5.2.7)
- MonkeyCache.FileStore: 1.6.3 (unified)

## Unchanged Projects

The following projects were not modified and continue to work with the new app:
- **VolleMoehre.Contracts** - Shared contracts/models
- **VolleMoehre.API** - Backend API
- **VolleMoehre.Adapter.LiteDB** - Database adapter
- **VolleMoehre.Adapter.Calender** - Calendar integration
- **VolleMoehre.Web** - Web project

## Testing Recommendations

1. **Build Verification**: Ensure project builds for all target platforms
2. **Runtime Testing**: Test app functionality on:
   - Android device/emulator
   - iOS device/simulator
   - Windows desktop
   - WebAssembly (browser)
3. **API Integration**: Verify API connectivity and data synchronization
4. **UI/UX**: Check that Material Design theming is applied correctly
5. **Navigation**: Test page navigation flows
6. **Data Persistence**: Verify MonkeyCache functionality

## Known Issues & Limitations

1. **Workload Requirements**: Building requires platform workloads to be installed
2. **Third-party Libraries**: Some older libraries may need updates for .NET 10 compatibility
3. **Platform-specific Code**: Custom renderers or platform-specific implementations may need review
4. **Build Performance**: First build may take longer due to multi-targeting

## Rollback Procedure

If rollback is needed:
1. The old project structure is preserved in `VolleMoehre.App.Old/`
2. Restore `VolleMoehre.sln.old` to `VolleMoehre.sln`
3. Remove `VolleMoehre.App/` and rename `VolleMoehre.App.Old/` back to `VolleMoehre.App/`

## Additional Resources

- [Uno Platform Documentation](https://platform.uno/docs/)
- [Uno Platform 6.4 Release Notes](https://platform.uno/blog/)
- [Single Project Structure Guide](https://aka.platform.uno/singleproject)
- [Migration Guide from Uno 4.x to 5.x](https://platform.uno/docs/articles/migrating-from-previous-releases.html)

## Support

For issues or questions:
1. Check Uno Platform GitHub Issues: https://github.com/unoplatform/uno/issues
2. Uno Platform Discord: https://discord.gg/eBHZSKG
3. StackOverflow tag: `uno-platform`
