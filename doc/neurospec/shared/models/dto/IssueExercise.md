# IssueExercise `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpec.Shared.Models.DTO
  NeuroSpec.Shared.Models.DTO.IssueExercise[[IssueExercise]]
  NeuroSpec.Shared.Models.DTO.Issue[[Issue]]
  end
NeuroSpec.Shared.Models.DTO.Issue --> NeuroSpec.Shared.Models.DTO.IssueExercise
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `string` | [`Duration`](#duration) | `get, set` |
| `int` | [`ExerciseID`](#exerciseid) | `get, set` |
| `string` | [`Frequency`](#frequency) | `get, set` |
| `string` | [`Intensity`](#intensity) | `get, set` |
| `int` | [`Quantity`](#quantity) | `get, set` |

## Details
### Inheritance
 - [
`Issue`
](./Issue.md)

### Constructors
#### IssueExercise
```csharp
public IssueExercise()
```

### Properties
#### ExerciseID
```csharp
public int ExerciseID { get; set; }
```

#### Quantity
```csharp
public int Quantity { get; set; }
```

#### Duration
```csharp
public string Duration { get; set; }
```

#### Frequency
```csharp
public string Frequency { get; set; }
```

#### Intensity
```csharp
public string Intensity { get; set; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
