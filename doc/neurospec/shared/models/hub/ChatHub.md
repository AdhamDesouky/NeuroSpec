# ChatHub `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpec.Shared.Models.HUB
  NeuroSpec.Shared.Models.HUB.ChatHub[[ChatHub]]
  end
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task` | [`SendMessage`](#sendmessage)(`string` userName, `string` message) |

## Details
### Constructors
#### ChatHub
```csharp
public ChatHub()
```

### Methods
#### SendMessage
```csharp
public async Task SendMessage(string userName, string message)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `string` | userName |   |
| `string` | message |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
