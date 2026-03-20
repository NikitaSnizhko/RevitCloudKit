# RevitCloudKit
![Revit](https://img.shields.io/badge/Autodesk_Revit-%23186BFF.svg?style=for-the-badge&logo=autodeskrevit&logoColor=white)
![Supabase](https://img.shields.io/badge/Supabase-3ECF8E?style=for-the-badge&logo=supabase&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![License](https://img.shields.io/github/license/YOU/REPO?style=for-the-badge)


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
To start exploring the template follow this Guide:....