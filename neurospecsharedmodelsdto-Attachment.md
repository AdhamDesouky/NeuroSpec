# Attachment `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpec.Shared.Models.DTO
  NeuroSpec.Shared.Models.DTO.Attachment[[Attachment]]
  end
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `string` | [`contentType`](#contenttype) | `get, set` |
| `string` | [`title`](#title) | `get, set` |
| `string` | [`url`](#url) | `get, set` |

## Details
### Constructors
#### Attachment
```csharp
public Attachment()
```

### Properties
#### url
```csharp
public string url { get; set; }
```

#### title
```csharp
public string title { get; set; }
```

#### contentType
```csharp
public string contentType { get; set; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
