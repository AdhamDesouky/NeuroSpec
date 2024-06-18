# IOCRService `Public interface`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Services.OCR_Service
  NeuroSpecCompanion.Services.OCR_Service.IOCRService[[IOCRService]]
  class NeuroSpecCompanion.Services.OCR_Service.IOCRService interfaceStyle;
  end
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task`&lt;`string`&gt; | [`ReadTextFromImageAsync`](#readtextfromimageasync)(`Stream` imageStream) |

## Details
### Methods
#### ReadTextFromImageAsync
```csharp
public Task<string> ReadTextFromImageAsync(Stream imageStream)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `Stream` | imageStream |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
