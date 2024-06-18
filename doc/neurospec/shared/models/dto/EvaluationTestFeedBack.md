# EvaluationTestFeedBack `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpec.Shared.Models.DTO
  NeuroSpec.Shared.Models.DTO.EvaluationTestFeedBack[[EvaluationTestFeedBack]]
  end
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `string` | [`Id`](#id) | `get, set` |
| `string` | [`Notes`](#notes) | `get, set` |
| `int` | [`PatientID`](#patientid) | `get, set` |
| `int` | [`Severity`](#severity) | `get, set` |
| `int` | [`TestFeedBackID`](#testfeedbackid) | `get, set` |
| `int` | [`TestID`](#testid) | `get, set` |
| `DateTime` | [`TimeStamp`](#timestamp) | `get, set` |
| `int` | [`VisitID`](#visitid) | `get, set` |

## Details
### Constructors
#### EvaluationTestFeedBack
```csharp
public EvaluationTestFeedBack()
```

### Properties
#### Id
```csharp
public string Id { get; set; }
```

#### TestFeedBackID
```csharp
public int TestFeedBackID { get; set; }
```

#### Severity
```csharp
public int Severity { get; set; }
```

#### VisitID
```csharp
public int VisitID { get; set; }
```

#### PatientID
```csharp
public int PatientID { get; set; }
```

#### TestID
```csharp
public int TestID { get; set; }
```

#### Notes
```csharp
public string Notes { get; set; }
```

#### TimeStamp
```csharp
public DateTime TimeStamp { get; set; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
