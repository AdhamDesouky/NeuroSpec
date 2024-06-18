# Resource `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion
  NeuroSpecCompanion.Resource[[Resource]]
  end
  subgraph _Microsoft.Android.Resource.Designer
_Microsoft.Android.Resource.Designer.ResourceConstant[[ResourceConstant]]
  end
_Microsoft.Android.Resource.Designer.ResourceConstant --> NeuroSpecCompanion.Resource
```

## Details
### Inheritance
 - `ResourceConstant`

### Constructors
#### Resource
```csharp
public Resource()
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
