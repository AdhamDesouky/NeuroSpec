# PDFOCRService `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Services.PDF_OCR_Service
  NeuroSpecCompanion.Services.PDF_OCR_Service.PDFOCRService[[PDFOCRService]]
  NeuroSpecCompanion.Services.PDF_OCR_Service.IPDFOCRService[[IPDFOCRService]]
  class NeuroSpecCompanion.Services.PDF_OCR_Service.IPDFOCRService interfaceStyle;
  end
NeuroSpecCompanion.Services.PDF_OCR_Service.IPDFOCRService --> NeuroSpecCompanion.Services.PDF_OCR_Service.PDFOCRService
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task`&lt;`string`&gt; | [`ReadTextFromPDFAsync`](#readtextfrompdfasync)(`Stream` pdfStream) |

## Details
### Inheritance
 - [
`IPDFOCRService`
](./neurospeccompanionservicespdf_ocr_service-IPDFOCRService)

### Constructors
#### PDFOCRService
```csharp
public PDFOCRService()
```

### Methods
#### ReadTextFromPDFAsync
```csharp
public virtual async Task<string> ReadTextFromPDFAsync(Stream pdfStream)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `Stream` | pdfStream |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
