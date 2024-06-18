# PathGame `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Views
  NeuroSpecCompanion.Views.PathGame[[PathGame]]
  end
  subgraph Microsoft.Maui.Controls
Microsoft.Maui.Controls.ContentPage[[ContentPage]]
  end
Microsoft.Maui.Controls.ContentPage --> NeuroSpecCompanion.Views.PathGame
```

## Details
### Inheritance
 - `ContentPage`

### Nested types
#### Enums
 - `TouchActionType`

#### Classes
 - `TouchActionEventArgs`

### Constructors
#### PathGame
```csharp
public PathGame()
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
