# HomePage `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Views
  NeuroSpecCompanion.Views.HomePage[[HomePage]]
  end
  subgraph Microsoft.Maui.Controls
Microsoft.Maui.Controls.ContentPage[[ContentPage]]
  end
Microsoft.Maui.Controls.ContentPage --> NeuroSpecCompanion.Views.HomePage
```

## Details
### Inheritance
 - `ContentPage`

### Constructors
#### HomePage
```csharp
public HomePage()
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
