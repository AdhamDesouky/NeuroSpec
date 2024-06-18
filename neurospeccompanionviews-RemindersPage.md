# RemindersPage `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Views
  NeuroSpecCompanion.Views.RemindersPage[[RemindersPage]]
  end
  subgraph Microsoft.Maui.Controls
Microsoft.Maui.Controls.ContentPage[[ContentPage]]
  end
Microsoft.Maui.Controls.ContentPage --> NeuroSpecCompanion.Views.RemindersPage
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `ObservableCollection`&lt;[`Reminder`](./neurospeccompanionviews-Reminder)&gt; | [`Reminders`](#reminders) | `get, set` |

## Details
### Inheritance
 - `ContentPage`

### Constructors
#### RemindersPage
```csharp
public RemindersPage()
```

### Properties
#### Reminders
```csharp
public ObservableCollection<Reminder> Reminders { get; set; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
