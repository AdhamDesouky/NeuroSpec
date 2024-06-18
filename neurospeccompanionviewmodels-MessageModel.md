# MessageModel `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.ViewModels
  NeuroSpecCompanion.ViewModels.MessageModel[[MessageModel]]
  end
```

## Members
### Properties
#### Public  properties
| Type | Name | Methods |
| --- | --- | --- |
| `bool` | [`IsSender`](#issender) | `get, set` |
| `string` | [`Text`](#text) | `get, set` |

## Details
### Constructors
#### MessageModel
```csharp
public MessageModel()
```

### Properties
#### Text
```csharp
public string Text { get; set; }
```

#### IsSender
```csharp
public bool IsSender { get; set; }
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
