# MainPage `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Views
  NeuroSpecCompanion.Views.MainPage[[MainPage]]
  end
  subgraph Microsoft.Maui.Controls
Microsoft.Maui.Controls.ContentPage[[ContentPage]]
  end
Microsoft.Maui.Controls.ContentPage --> NeuroSpecCompanion.Views.MainPage
```

## Details
### Inheritance
 - `ContentPage`

### Constructors
#### MainPage
```csharp
public MainPage()
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
