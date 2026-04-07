# RevitCloudKit
![Revit](https://img.shields.io/badge/Autodesk_Revit-%23186BFF.svg?style=for-the-badge&logo=autodeskrevit&logoColor=white)
![Supabase](https://img.shields.io/badge/Supabase-3ECF8E?style=for-the-badge&logo=supabase&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![License](https://img.shields.io/github/license/NikitaSnizhko/RevitCloudKit?style=for-the-badge)
[![GitHub](https://img.shields.io/badge/GitHub-Open_Source-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/NikitaSnizhko/RevitCloudKit)
[![NuGet](https://img.shields.io/badge/NuGet-available-004880?style=for-the-badge&logo=nuget&logoColor=white)](https://www.nuget.org/profiles/NikitaSnizhko)


RevitCloudKit is a .NET template for building Revit Add-ins with cloud connectivity. 
It provides a modern, modular architecture with built-in support for Supabase authentication, 
async Revit API calls, and cloud data storage.

### Features
- Multi-Revit-version support (2025/2026) via build configs
- Supabase auth with session persistence
- Dependency injection (Microsoft.Extensions.DI) pre-configured
- MVVM (CommunityToolkit.Mvvm) with WPF
- Async Revit API (Revit.Async) built-in
- Cloud Postgrest CRUD example (materials)
- Cloud Storage integration example

### Installation
This template is available as an official NuGet package on [NuGet Gallery](https://www.nuget.org/profiles/NikitaSnizhko).

```shell
dotnet new install RevitCloudKit
```

### Usage
- CLI: command to create a new add-in project from the template:
   ```shell
      dotnet new revit-supabase -n [NewAddinName]
    ```
- IDE: In IDE(Rider, Visual Studio, etc) create a new project from the "RevitCloudKit" template.


### Configuration
To start exploring the template follow this [Guide](https://github.com/NikitaSnizhko/RevitCloudKit/wiki).
