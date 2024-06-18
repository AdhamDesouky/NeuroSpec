# Address `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpec.Shared.Models.DTO
  NeuroSpec.Shared.Models.DTO.Address[[Address]]
  end
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `string` | [`City`](#city) | `get, set` |
| `string` | [`Country`](#country) | `get, set` |
| `string` | [`State`](#state) | `get, set` |
| `string` | [`Street`](#street) | `get, set` |
| `string` | [`ZipCode`](#zipcode) | `get, set` |

## Details
### Constructors
#### Address
```csharp
public Address()
```

### Properties
#### Street
```csharp
public string Street { get; set; }
```

#### City
```csharp
public string City { get; set; }
```

#### State
```csharp
public string State { get; set; }
```

#### ZipCode
```csharp
public string ZipCode { get; set; }
```

#### Country
```csharp
public string Country { get; set; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
