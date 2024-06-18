# Issue `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpec.Shared.Models.DTO
  NeuroSpec.Shared.Models.DTO.Issue[[Issue]]
  end
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `int` | [`DoctorID`](#doctorid) | `get, set` |
| `string` | [`Id`](#id) | `get, set` |
| `string` | [`IssueID`](#issueid) | `get, set` |
| `string` | [`Notes`](#notes) | `get, set` |
| `int` | [`PatientID`](#patientid) | `get, set` |
| `int` | [`PrescriptionID`](#prescriptionid) | `get, set` |
| `int` | [`VisitID`](#visitid) | `get, set` |

## Details
### Constructors
#### Issue
```csharp
public Issue()
```

### Properties
#### Id
```csharp
public string Id { get; set; }
```

#### IssueID
```csharp
public string IssueID { get; set; }
```

#### PrescriptionID
```csharp
public int PrescriptionID { get; set; }
```

#### PatientID
```csharp
public int PatientID { get; set; }
```

#### DoctorID
```csharp
public int DoctorID { get; set; }
```

#### VisitID
```csharp
public int VisitID { get; set; }
```

#### Notes
```csharp
public string Notes { get; set; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
