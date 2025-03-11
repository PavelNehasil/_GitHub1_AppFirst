WinUI

## Source Code Analysis

### App.xaml.cs
- **Purpose**: Initializes the application's services and configuration.
- **Key Components**:
  - `ConfigureServices`: Registers various services such as `ThemeService` and `JsonNavigationViewService`.
  - `OnLaunched`: Handles application launch and navigation.

### User.cs
- **Purpose**: Represents the `User` entity within the application.
- **Key Components**:
  - Properties: `Id`, `UserName`, `Password`, `IsAdmin`, `Email`, `FirstName`, `LastName`, `ImageSource`.
  - Implements `INotifyPropertyChanged` to enable UI updates when property values change.

### GlobalUsings.cs
- **Purpose**: Provides global using directives for commonly used namespaces.
- **Key Components**:
  - Includes namespaces such as `Microsoft.UI.Xaml`, `CommunityToolkit.Mvvm.ComponentModel`, and `AppFirst.Common`.

### AppConfig.cs
- **Purpose**: Manages application configuration using `JsonSettings`.
- **Key Components**:
  - Properties: `Version`, `FileName`, `LastUpdateCheck`.
  - Implements `IVersionable` to enforce version control on configuration settings.WinUI
