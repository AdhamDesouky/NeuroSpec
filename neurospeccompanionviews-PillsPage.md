# PillsPage `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Views
  NeuroSpecCompanion.Views.PillsPage[[PillsPage]]
  end
  subgraph Microsoft.Maui.Controls
Microsoft.Maui.Controls.ContentPage[[ContentPage]]
  end
Microsoft.Maui.Controls.ContentPage --> NeuroSpecCompanion.Views.PillsPage
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `ObservableCollection`&lt;[`Assessment`](./neurospeccompanionviews-Assessment)&gt; | [`Assessments`](#assessments) | `get, set` |

## Details
### Inheritance
 - `ContentPage`

### Constructors
#### PillsPage
```csharp
public PillsPage()
```

### Properties
#### Assessments
```csharp
public ObservableCollection<Assessment> Assessments { get; set; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
