# MainActivity `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion
  NeuroSpecCompanion.MainActivity[[MainActivity]]
  end
  subgraph Microsoft.Maui
Microsoft.Maui.MauiAppCompatActivity[[MauiAppCompatActivity]]
  end
Microsoft.Maui.MauiAppCompatActivity --> NeuroSpecCompanion.MainActivity
```

## Details
### Inheritance
 - `MauiAppCompatActivity`

### Constructors
#### MainActivity
```csharp
public MainActivity()
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
