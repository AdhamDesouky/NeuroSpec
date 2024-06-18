# TapGame `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Views.TapTest
  NeuroSpecCompanion.Views.TapTest.TapGame[[TapGame]]
  end
  subgraph Microsoft.Maui.Controls
Microsoft.Maui.Controls.ContentPage[[ContentPage]]
  end
Microsoft.Maui.Controls.ContentPage --> NeuroSpecCompanion.Views.TapTest.TapGame
```

## Details
### Inheritance
 - `ContentPage`

### Constructors
#### TapGame
[*Source code*](https://github.com///blob//NeuroSpecCompanion/Views/TapTest/TapGame.xaml.cs#L22)
```csharp
public TapGame()
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
