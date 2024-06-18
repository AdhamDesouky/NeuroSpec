# AppShell `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion
  NeuroSpecCompanion.AppShell[[AppShell]]
  end
  subgraph Microsoft.Maui.Controls
Microsoft.Maui.Controls.Shell[[Shell]]
  end
Microsoft.Maui.Controls.Shell --> NeuroSpecCompanion.AppShell
```

## Details
### Inheritance
 - `Shell`

### Constructors
#### AppShell
[*Source code*](https://github.com///blob//NeuroSpecCompanion/AppShell.xaml.cs#L8)
```csharp
public AppShell()
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
