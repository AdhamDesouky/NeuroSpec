# ViewModelBase `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.ViewModels
  NeuroSpecCompanion.ViewModels.ViewModelBase[[ViewModelBase]]
  end
  subgraph Microsoft.Maui.Controls
Microsoft.Maui.Controls.BindableObject[[BindableObject]]
  end
Microsoft.Maui.Controls.BindableObject --> NeuroSpecCompanion.ViewModels.ViewModelBase
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task` | [`InitializeAsync`](#initializeasync)(`object` navigationData) |

## Details
### Inheritance
 - `BindableObject`

### Constructors
#### ViewModelBase
```csharp
public ViewModelBase()
```

### Methods
#### InitializeAsync
```csharp
public virtual Task InitializeAsync(object navigationData)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `object` | navigationData |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
