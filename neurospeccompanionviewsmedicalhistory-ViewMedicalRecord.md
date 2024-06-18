# ViewMedicalRecord `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Views.MedicalHistory
  NeuroSpecCompanion.Views.MedicalHistory.ViewMedicalRecord[[ViewMedicalRecord]]
  end
  subgraph Microsoft.Maui.Controls
Microsoft.Maui.Controls.IQueryAttributable[[IQueryAttributable]]
Microsoft.Maui.Controls.ContentPage[[ContentPage]]
  end
Microsoft.Maui.Controls.IQueryAttributable --> NeuroSpecCompanion.Views.MedicalHistory.ViewMedicalRecord
Microsoft.Maui.Controls.ContentPage --> NeuroSpecCompanion.Views.MedicalHistory.ViewMedicalRecord
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `void` | [`ApplyQueryAttributes`](#applyqueryattributes)(`IDictionary`&lt;`string`, `object`&gt; query) |

## Details
### Inheritance
 - `IQueryAttributable`
 - `ContentPage`

### Constructors
#### ViewMedicalRecord
```csharp
public ViewMedicalRecord()
```

### Methods
#### ApplyQueryAttributes
```csharp
public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `IDictionary`&lt;`string`, `object`&gt; | query |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
