# ViewAllAppointmentsViewModel `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.ViewModels
  NeuroSpecCompanion.ViewModels.ViewAllAppointmentsViewModel[[ViewAllAppointmentsViewModel]]
  end
  subgraph MvvmHelpers
MvvmHelpers.BaseViewModel[[BaseViewModel]]
  end
MvvmHelpers.BaseViewModel --> NeuroSpecCompanion.ViewModels.ViewAllAppointmentsViewModel
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `ICommand` | [`BookAppointmentCommand`](#bookappointmentcommand) | `get` |
| `ICommand` | [`DeleteVisitCommand`](#deletevisitcommand) | `get` |
| `ICommand` | [`ViewVisitCommand`](#viewvisitcommand) | `get` |
| `ObservableCollection`&lt;[`Visit`](./neurospecsharedmodelsdto-Visit)&gt; | [`Visits`](#visits) | `get` |

## Details
### Inheritance
 - `BaseViewModel`

### Constructors
#### ViewAllAppointmentsViewModel
```csharp
public ViewAllAppointmentsViewModel()
```

### Properties
#### Visits
```csharp
public ObservableCollection<Visit> Visits { get; }
```

#### DeleteVisitCommand
```csharp
public ICommand DeleteVisitCommand { get; }
```

#### ViewVisitCommand
```csharp
public ICommand ViewVisitCommand { get; }
```

#### BookAppointmentCommand
```csharp
public ICommand BookAppointmentCommand { get; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
