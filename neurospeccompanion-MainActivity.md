# MainActivity `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion
  NeuroSpecCompanion.MainActivity[[MainActivity]]
  end
  subgraph Microsoft.Maui
Microsoft.Maui.MauiAppCompatActivity[[MauiAppCompatActivity]]
  end
Microsoft.Maui.MauiAppCompatActivity --> NeuroSpecCompanion.MainActivity
```

## Members
### Methods
#### Protected  methods
| Returns | Name |
| --- | --- |
| `void` | [`OnCreate`](#oncreate)(`Bundle` savedInstanceState) |

#### Public  methods
| Returns | Name |
| --- | --- |
| `void` | [`OnRequestPermissionsResult`](#onrequestpermissionsresult)(`int` requestCode, `string``[]` permissions, `Permission``[]` grantResults) |

## Details
### Inheritance
 - `MauiAppCompatActivity`

### Constructors
#### MainActivity
```csharp
public MainActivity()
```

### Methods
#### OnCreate
```csharp
protected override void OnCreate(Bundle savedInstanceState)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `Bundle` | savedInstanceState |   |

#### OnRequestPermissionsResult
```csharp
public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | requestCode |   |
| `string``[]` | permissions |   |
| `Permission``[]` | grantResults |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
