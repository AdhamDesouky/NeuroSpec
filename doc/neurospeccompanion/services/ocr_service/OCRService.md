# OCRService `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Services.OCR_Service
  NeuroSpecCompanion.Services.OCR_Service.OCRService[[OCRService]]
  NeuroSpecCompanion.Services.OCR_Service.IOCRService[[IOCRService]]
  class NeuroSpecCompanion.Services.OCR_Service.IOCRService interfaceStyle;
  end
NeuroSpecCompanion.Services.OCR_Service.IOCRService --> NeuroSpecCompanion.Services.OCR_Service.OCRService
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task`&lt;`string`&gt; | [`ReadTextFromImageAsync`](#readtextfromimageasync)(`Stream` imageStream) |

## Details
### Inheritance
 - [
`IOCRService`
](./IOCRService.md)

### Constructors
#### OCRService
```csharp
public OCRService()
```

### Methods
#### ReadTextFromImageAsync
```csharp
public virtual async Task<string> ReadTextFromImageAsync(Stream imageStream)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `Stream` | imageStream |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
