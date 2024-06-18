# MemoryGame `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Views.MemoryTest
  NeuroSpecCompanion.Views.MemoryTest.MemoryGame[[MemoryGame]]
  end
  subgraph Microsoft.Maui.Controls
Microsoft.Maui.Controls.ContentPage[[ContentPage]]
  end
Microsoft.Maui.Controls.ContentPage --> NeuroSpecCompanion.Views.MemoryTest.MemoryGame
```

## Details
### Inheritance
 - `ContentPage`

### Constructors
#### MemoryGame
```csharp
public MemoryGame()
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
