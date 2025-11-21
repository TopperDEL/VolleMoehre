# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade VolleMoehre.Contracts\VolleMoehre.Contracts.csproj
4. Upgrade VolleMoehre.Adapter.Calender\VolleMoehre.Adapter.Calender.csproj
5. Upgrade VolleMoehre.Adapter.LiteDB\VolleMoehre.Adapter.LiteDB.csproj
6. Upgrade VolleMoehre.App\VolleMoehre.App.Mobile\VolleMoehre.App.Mobile.csproj
7. Upgrade VolleMoehre.Web\VolleMoehre.Web.csproj
8. Upgrade VolleMoehre.App\VolleMoehre.App.Wasm\VolleMoehre.App.Wasm.csproj
9. Upgrade VolleMoehre.App\VolleMoehre.App.UWP\VolleMoehre.App.UWP.csproj
10. Upgrade VolleMoehre.App\VolleMoehre.App.Shared\VolleMoehre.App.Shared.shproj
11. Upgrade VolleMoehre.API\VolleMoehre.API.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

Table below contains projects that do belong to the dependency graph for selected projects and should not be included in the upgrade.

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                         | Current Version                                     | New Version | Description                                                                                                   |
|:-------------------------------------|:----------------------------------------------------|:-----------:|:--------------------------------------------------------------------------------------------------------------|
| LiteDB                               | 5.0.2                                               | 5.0.21      | Security vulnerability                                                                                        |
| Microsoft.AspNetCore.App             | 2.2.8                                               | 2.2.8       | Deprecated - functionality included in framework reference                                                     |
| Microsoft.AspNetCore.Razor.Design    | 2.2.0                                               | 2.3.0       | Deprecated - functionality included in framework reference                                                     |
| Microsoft.Extensions.Logging         | 5.0.0                                               | 10.0.0      | Deprecated - update recommended for .NET 10.0                                                                 |
| Microsoft.Extensions.Logging.Console | 5.0.0                                               | 10.0.0      | Deprecated - update recommended for .NET 10.0                                                                 |
| Microsoft.Extensions.Logging.Debug   | 5.0.0                                               | 10.0.0      | Deprecated - update recommended for .NET 10.0                                                                 |
| Microsoft.Windows.Compatibility      | 5.0.0                                               | 10.0.0      | Deprecated - update recommended for .NET 10.0                                                                 |
| Microsoft.NETCore.UniversalWindowsPlatform | 6.2.11                                       |             | Replace with Microsoft.WindowsAppSDK 1.8.251106002; Microsoft.Graphics.Win2D 1.1.0; Microsoft.Windows.Compatibility 10.0.0 |
| Microsoft.UI.Xaml                    | 2.6.2                                               |             | Functionality included in new framework reference                                                             |
| RestSharp                            | 106.10.1                                            | 112.1.0     | Security vulnerability                                                                                        |
| Uno.UniversalImageLoader             | 1.9.35                                              |             | No supported version found for .NET 10.0                                                                      |
| Xamarin.Google.Android.Material      | 1.4.0.4                                             |             | No supported version found for .NET 10.0                                                                      |
| Xam.Plugin.Connectivity              | 3.2.0                                               | 3.2.0       | Deprecated - replacement keeps same version                                                                   |

### Project upgrade details

#### VolleMoehre.Contracts\VolleMoehre.Contracts.csproj modifications

Project properties changes:
  - Target framework should be changed to `net10.0`

Other changes:
  - Review for any API incompatibilities after framework change.

#### VolleMoehre.Adapter.Calender\VolleMoehre.Adapter.Calender.csproj modifications

Project properties changes:
  - Target framework should be changed to `net10.0`

Other changes:
  - Review calendar adapter dependencies for .NET 10 compatibility.

#### VolleMoehre.Adapter.LiteDB\VolleMoehre.Adapter.LiteDB.csproj modifications

Project properties changes:
  - Target framework should be changed to `net10.0`

NuGet packages changes:
  - LiteDB should be updated from `5.0.2` to `5.0.21` (*security vulnerability*)

Other changes:
  - Verify LiteDB API compatibility.

#### VolleMoehre.App\VolleMoehre.App.Mobile\VolleMoehre.App.Mobile.csproj modifications

Project properties changes:
  - Target frameworks should be changed from `net6.0-android;net6.0-ios;net6.0-maccatalyst;net6.0-macos` to `net6.0-android;net6.0-ios;net6.0-maccatalyst;net6.0-macos;net10.0--android;net10.0--ios;net10.0--maccatalyst;net10.0--macos`

NuGet packages changes:
  - Microsoft.Extensions.Logging should be updated from `5.0.0` to `10.0.0` (*deprecated*)
  - Microsoft.Extensions.Logging.Console should be updated from `5.0.0` to `10.0.0` (*deprecated*)
  - Uno.UniversalImageLoader requires removal (*no supported version*)
  - Xamarin.Google.Android.Material requires removal (*no supported version*)
  - Xam.Plugin.Connectivity replace with same version `3.2.0` (*deprecated*)

Other changes:
  - Assess migration path for removed mobile-specific packages.

#### VolleMoehre.Web\VolleMoehre.Web.csproj modifications

Project properties changes:
  - Target framework should be changed from `net6.0` to `net10.0`

NuGet packages changes:
  - RestSharp should be updated from `106.10.1` to `112.1.0` (*security vulnerability*)

Other changes:
  - Validate Razor Pages for .NET 10 tag helper/API changes.

#### VolleMoehre.App\VolleMoehre.App.Wasm\VolleMoehre.App.Wasm.csproj modifications

Project properties changes:
  - Target framework should be changed from `net6.0` to `net10.0`

NuGet packages changes:
  - Microsoft.Windows.Compatibility should be updated from `5.0.0` to `10.0.0` (*deprecated*)
  - Microsoft.Extensions.Logging should be updated from `5.0.0` to `10.0.0` (*deprecated*)
  - Xam.Plugin.Connectivity replace with same version `3.2.0` (*deprecated*)

Other changes:
  - Review WebAssembly-specific APIs for .NET 10.

#### VolleMoehre.App\VolleMoehre.App.UWP\VolleMoehre.App.UWP.csproj modifications

Project properties changes:
  - Target framework should be changed from `.NETCore,Version=v5.0` to `net10.0-windows10.0.22000.0`
  - Convert project to SDK style.

NuGet packages changes:
  - Microsoft.NETCore.UniversalWindowsPlatform should be removed and replaced with Microsoft.WindowsAppSDK `1.8.251106002`, Microsoft.Graphics.Win2D `1.1.0`, Microsoft.Windows.Compatibility `10.0.0`
  - Microsoft.Extensions.Logging should be updated from `5.0.0` to `10.0.0` (*deprecated*)
  - Microsoft.Extensions.Logging.Debug should be updated from `5.0.0` to `10.0.0` (*deprecated*)
  - Microsoft.UI.Xaml functionality moved to framework reference (remove)
  - Xam.Plugin.Connectivity replace with same version `3.2.0` (*deprecated*)

Other changes:
  - Apply WinAppSDK migration guidance.

#### VolleMoehre.App\VolleMoehre.App.Shared\VolleMoehre.App.Shared.shproj modifications

Project properties changes:
  - Target framework should be changed from `.NETCore,Version=v4.5.1` to `net10.0`

Other changes:
  - Validate shared code compatibility across new multi-targeting.

#### VolleMoehre.API\VolleMoehre.API.csproj modifications

Project properties changes:
  - Target framework should be changed from `net6.0` to `net10.0`

NuGet packages changes:
  - Microsoft.AspNetCore.App deprecated; functionality included in framework reference.
  - Microsoft.AspNetCore.Razor.Design should be updated from `2.2.0` to `2.3.0` (*deprecated*)

Other changes:
  - Remove obsolete ASP.NET Core package references, ensure using SDK implicit framework references.
