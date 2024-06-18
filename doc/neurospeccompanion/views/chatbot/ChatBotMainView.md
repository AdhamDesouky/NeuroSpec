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
[*Source code*](https://github.com///blob//NeuroSpecCompanion/Views/ChatBot/ChatBotMainView.xaml.cs#L96)
```csharp
public ChatBotMainView()
```

### Methods
#### CreateNewSenderMessage
[*Source code*](https://github.com///blob//NeuroSpecCompanion/Views/ChatBot/ChatBotMainView.xaml.cs#L22)
```csharp
public HorizontalStackLayout CreateNewSenderMessage(string text)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `string` | text |   |

#### CreateNewResponseMessage
[*Source code*](https://github.com///blob//NeuroSpecCompanion/Views/ChatBot/ChatBotMainView.xaml.cs#L60)
```csharp
public HorizontalStackLayout CreateNewResponseMessage(string text)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `string` | text |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
