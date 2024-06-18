# ChatHomePage `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Views.ChatBot
  NeuroSpecCompanion.Views.ChatBot.ChatHomePage[[ChatHomePage]]
  end
  subgraph Microsoft.Maui.Controls
Microsoft.Maui.Controls.ContentPage[[ContentPage]]
  end
Microsoft.Maui.Controls.ContentPage --> NeuroSpecCompanion.Views.ChatBot.ChatHomePage
```

## Details
### Inheritance
 - `ContentPage`

### Constructors
#### ChatHomePage
[*Source code*](https://github.com///blob//NeuroSpecCompanion/Views/ChatBot/ChatHomePage.xaml.cs#L5)
```csharp
public ChatHomePage()
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
