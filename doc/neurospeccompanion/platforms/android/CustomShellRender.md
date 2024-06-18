# CustomShellRender `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Platforms.Android
  NeuroSpecCompanion.Platforms.Android.CustomShellRender[[CustomShellRender]]
  end
  subgraph Microsoft.Maui.Controls.Handlers.Compatibility
Microsoft.Maui.Controls.Handlers.Compatibility.ShellRenderer[[ShellRenderer]]
  end
Microsoft.Maui.Controls.Handlers.Compatibility.ShellRenderer --> NeuroSpecCompanion.Platforms.Android.CustomShellRender
```

## Members
### Methods
#### Protected  methods
| Returns | Name |
| --- | --- |
| `IShellBottomNavViewAppearanceTracker` | [`CreateBottomNavViewAppearanceTracker`](#createbottomnavviewappearancetracker)(`ShellItem` shellItem) |

## Details
### Inheritance
 - `ShellRenderer`

### Constructors
#### CustomShellRender
[*Source code*](https://github.com///blob//NeuroSpecCompanion/Platforms/Android/CustomShellRender.cs#L17)
```csharp
public CustomShellRender(Context context)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `Context` | context |   |

### Methods
#### CreateBottomNavViewAppearanceTracker
[*Source code*](https://github.com///blob//NeuroSpecCompanion/Platforms/Android/CustomShellRender.cs#L22)
```csharp
protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `ShellItem` | shellItem |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
