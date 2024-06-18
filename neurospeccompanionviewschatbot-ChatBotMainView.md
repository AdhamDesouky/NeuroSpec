# ChatBotMainView `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Views.ChatBot
  NeuroSpecCompanion.Views.ChatBot.ChatBotMainView[[ChatBotMainView]]
  end
  subgraph Microsoft.Maui.Controls
Microsoft.Maui.Controls.ContentPage[[ContentPage]]
  end
Microsoft.Maui.Controls.ContentPage --> NeuroSpecCompanion.Views.ChatBot.ChatBotMainView
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `HorizontalStackLayout` | [`CreateNewResponseMessage`](#createnewresponsemessage)(`string` text) |
| `HorizontalStackLayout` | [`CreateNewSenderMessage`](#createnewsendermessage)(`string` text) |

## Details
### Inheritance
 - `ContentPage`

### Constructors
#### ChatBotMainView
```csharp
public ChatBotMainView()
```

### Methods
#### CreateNewSenderMessage
```csharp
public HorizontalStackLayout CreateNewSenderMessage(string text)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `string` | text |   |

#### CreateNewResponseMessage
```csharp
public HorizontalStackLayout CreateNewResponseMessage(string text)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `string` | text |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
