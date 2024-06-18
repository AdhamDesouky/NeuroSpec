# IssueSNOMED `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpec.Shared.Models.DTO
  NeuroSpec.Shared.Models.DTO.IssueSNOMED[[IssueSNOMED]]
  NeuroSpec.Shared.Models.DTO.Issue[[Issue]]
  end
NeuroSpec.Shared.Models.DTO.Issue --> NeuroSpec.Shared.Models.DTO.IssueSNOMED
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `string` | [`SNOMEDDescription`](#snomeddescription) | `get, set` |
| `string` | [`SNOMEDID`](#snomedid) | `get, set` |
| `string` | [`SNOMEDName`](#snomedname) | `get, set` |

## Details
### Inheritance
 - [
`Issue`
](./neurospecsharedmodelsdto-Issue)

### Constructors
#### IssueSNOMED
```csharp
public IssueSNOMED()
```

### Properties
#### SNOMEDID
```csharp
public string SNOMEDID { get; set; }
```

#### SNOMEDName
```csharp
public string SNOMEDName { get; set; }
```

#### SNOMEDDescription
```csharp
public string SNOMEDDescription { get; set; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
