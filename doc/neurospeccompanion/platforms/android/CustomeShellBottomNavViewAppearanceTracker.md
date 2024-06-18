# CustomeShellBottomNavViewAppearanceTracker `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Platforms.Android
  NeuroSpecCompanion.Platforms.Android.CustomeShellBottomNavViewAppearanceTracker[[CustomeShellBottomNavViewAppearanceTracker]]
  end
  subgraph Microsoft.Maui.Controls.Platform.Compatibility
Microsoft.Maui.Controls.Platform.Compatibility.IShellBottomNavViewAppearanceTracker[[IShellBottomNavViewAppearanceTracker]]
  end
  subgraph System
System.IDisposable[[IDisposable]]
  end
Microsoft.Maui.Controls.Platform.Compatibility.IShellBottomNavViewAppearanceTracker --> NeuroSpecCompanion.Platforms.Android.CustomeShellBottomNavViewAppearanceTracker
System.IDisposable --> NeuroSpecCompanion.Platforms.Android.CustomeShellBottomNavViewAppearanceTracker
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `void` | [`Dispose`](#dispose)() |
| `void` | [`ResetAppearance`](#resetappearance)(`BottomNavigationView` bottomView) |
| `void` | [`SetAppearance`](#setappearance)(`BottomNavigationView` bottomView, `IShellAppearanceElement` appearance) |

## Details
### Inheritance
 - `IShellBottomNavViewAppearanceTracker`
 - `IDisposable`

### Constructors
#### CustomeShellBottomNavViewAppearanceTracker
```csharp
public CustomeShellBottomNavViewAppearanceTracker()
```

### Methods
#### Dispose
[*Source code*](https://github.com///blob//NeuroSpecCompanion/Platforms/Android/CustomShellRender.cs#L29)
```csharp
public virtual void Dispose()
```

#### ResetAppearance
[*Source code*](https://github.com///blob//NeuroSpecCompanion/Platforms/Android/CustomShellRender.cs#L34)
```csharp
public virtual void ResetAppearance(BottomNavigationView bottomView)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `BottomNavigationView` | bottomView |   |

#### SetAppearance
[*Source code*](https://github.com///blob//NeuroSpecCompanion/Platforms/Android/CustomShellRender.cs#L39)
```csharp
public virtual void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `BottomNavigationView` | bottomView |   |
| `IShellAppearanceElement` | appearance |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
