# Visit `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpec.Shared.Models.DTO
  NeuroSpec.Shared.Models.DTO.Visit[[Visit]]
  end
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `int` | [`AppointmentTypeID`](#appointmenttypeid) | `get, set` |
| `int` | [`DoctorID`](#doctorid) | `get, set` |
| `double` | [`Height`](#height) | `get, set` |
| `string` | [`Id`](#id) | `get, set` |
| `bool` | [`IsDone`](#isdone) | `get, set` |
| `int` | [`PatientID`](#patientid) | `get, set` |
| `string` | [`TherapistNotes`](#therapistnotes) | `get, set` |
| `DateTime` | [`TimeStamp`](#timestamp) | `get, set` |
| `int` | [`VisitID`](#visitid) | `get, set` |
| `double` | [`Weight`](#weight) | `get, set` |

## Details
### Constructors
#### Visit
```csharp
public Visit()
```

### Properties
#### Id
```csharp
public string Id { get; set; }
```

#### VisitID
```csharp
public int VisitID { get; set; }
```

#### DoctorID
```csharp
public int DoctorID { get; set; }
```

#### PatientID
```csharp
public int PatientID { get; set; }
```

#### AppointmentTypeID
```csharp
public int AppointmentTypeID { get; set; }
```

#### TimeStamp
```csharp
public DateTime TimeStamp { get; set; }
```

#### TherapistNotes
```csharp
public string TherapistNotes { get; set; }
```

#### Height
```csharp
public double Height { get; set; }
```

#### Weight
```csharp
public double Weight { get; set; }
```

#### IsDone
```csharp
public bool IsDone { get; set; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
