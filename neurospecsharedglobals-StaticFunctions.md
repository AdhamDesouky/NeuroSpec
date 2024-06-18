# StaticFunctions `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpec.Shared.Globals
  NeuroSpec.Shared.Globals.StaticFunctions[[StaticFunctions]]
  end
```

## Members
### Methods
#### Public Static methods
| Returns | Name |
| --- | --- |
| `int` | [`CalculateAge`](#calculateage)(`DateTime` birthDate) |
| `DateTime` | [`CalculateBirthdate`](#calculatebirthdate)(`int` age) |

## Details
### Methods
#### CalculateAge
```csharp
public static int CalculateAge(DateTime birthDate)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `DateTime` | birthDate |   |

#### CalculateBirthdate
```csharp
public static DateTime CalculateBirthdate(int age)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | age |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
