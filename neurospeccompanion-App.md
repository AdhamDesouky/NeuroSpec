# App `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion
  NeuroSpecCompanion.App[[App]]
  end
  subgraph Microsoft.Maui.Controls
Microsoft.Maui.Controls.Application[[Application]]
  end
Microsoft.Maui.Controls.Application --> NeuroSpecCompanion.App
```

## Members
### Methods
#### Protected  methods
| Returns | Name |
| --- | --- |
| `void` | [`OnStart`](#onstart)() |

## Details
### Inheritance
 - `Application`

### Constructors
#### App
```csharp
public App()
```

### Methods
#### OnStart
```csharp
protected override async void OnStart()
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
