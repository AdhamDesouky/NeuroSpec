# IssueDrug `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpec.Shared.Models.DTO
  NeuroSpec.Shared.Models.DTO.IssueDrug[[IssueDrug]]
  NeuroSpec.Shared.Models.DTO.Issue[[Issue]]
  end
NeuroSpec.Shared.Models.DTO.Issue --> NeuroSpec.Shared.Models.DTO.IssueDrug
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `string` | [`Name`](#name) | `get, set` |
| `string` | [`URL`](#url) | `get, set` |

## Details
### Inheritance
 - [
`Issue`
](./neurospecsharedmodelsdto-Issue)

### Constructors
#### IssueDrug
```csharp
public IssueDrug()
```

### Properties
#### Name
```csharp
public string Name { get; set; }
```

#### URL
```csharp
public string URL { get; set; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
