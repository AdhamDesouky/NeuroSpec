# IPDFOCRService `Public interface`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Services.PDF_OCR_Service
  NeuroSpecCompanion.Services.PDF_OCR_Service.IPDFOCRService[[IPDFOCRService]]
  class NeuroSpecCompanion.Services.PDF_OCR_Service.IPDFOCRService interfaceStyle;
  end
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task`&lt;`string`&gt; | [`ReadTextFromPDFAsync`](#readtextfrompdfasync)(`Stream` pdfStream) |

## Details
### Methods
#### ReadTextFromPDFAsync
```csharp
public Task<string> ReadTextFromPDFAsync(Stream pdfStream)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `Stream` | pdfStream |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
