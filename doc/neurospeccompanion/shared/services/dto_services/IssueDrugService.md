# IssueDrugService `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Shared.Services.DTO_Services
  NeuroSpecCompanion.Shared.Services.DTO_Services.IssueDrugService[[IssueDrugService]]
  end
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task` | [`DeleteIssueDrugAsync`](#deleteissuedrugasync)(`int` issueID) |
| `Task`&lt;`IEnumerable`&lt;[`IssueDrug`](../../../../neurospec/shared/models/dto/IssueDrug.md)&gt;&gt; | [`GetAllIssueDrugsAsync`](#getallissuedrugsasync)() |
| `Task`&lt;`IEnumerable`&lt;[`IssueDrug`](../../../../neurospec/shared/models/dto/IssueDrug.md)&gt;&gt; | [`GetAllIssueDrugsByPatientIDAsync`](#getallissuedrugsbypatientidasync)(`int` patientID) |
| `Task`&lt;`IEnumerable`&lt;[`IssueDrug`](../../../../neurospec/shared/models/dto/IssueDrug.md)&gt;&gt; | [`GetAllIssueDrugsByPrescriptionIDAsync`](#getallissuedrugsbyprescriptionidasync)(`int` prescriptionID) |
| `Task`&lt;[`IssueDrug`](../../../../neurospec/shared/models/dto/IssueDrug.md)&gt; | [`GetIssueDrugByIdAsync`](#getissuedrugbyidasync)(`int` issueID) |
| `Task`&lt;[`IssueDrug`](../../../../neurospec/shared/models/dto/IssueDrug.md)&gt; | [`InsertIssueDrugAsync`](#insertissuedrugasync)([`IssueDrug`](../../../../neurospec/shared/models/dto/IssueDrug.md) IssueDrug) |
| `Task` | [`UpdateIssueDrugAsync`](#updateissuedrugasync)(`int` issueID, [`IssueDrug`](../../../../neurospec/shared/models/dto/IssueDrug.md) IssueDrug) |

## Details
### Constructors
#### IssueDrugService
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Services/DTO_Services/IssueDrugService.cs#L15)
```csharp
public IssueDrugService()
```

### Methods
#### GetAllIssueDrugsAsync
```csharp
public async Task<IEnumerable<IssueDrug>> GetAllIssueDrugsAsync()
```

#### GetIssueDrugByIdAsync
```csharp
public async Task<IssueDrug> GetIssueDrugByIdAsync(int issueID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | issueID |   |

#### GetAllIssueDrugsByPatientIDAsync
```csharp
public async Task<IEnumerable<IssueDrug>> GetAllIssueDrugsByPatientIDAsync(int patientID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | patientID |   |

#### GetAllIssueDrugsByPrescriptionIDAsync
```csharp
public async Task<IEnumerable<IssueDrug>> GetAllIssueDrugsByPrescriptionIDAsync(int prescriptionID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | prescriptionID |   |

#### InsertIssueDrugAsync
```csharp
public async Task<IssueDrug> InsertIssueDrugAsync(IssueDrug IssueDrug)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| [`IssueDrug`](../../../../neurospec/shared/models/dto/IssueDrug.md) | IssueDrug |   |

#### UpdateIssueDrugAsync
```csharp
public async Task UpdateIssueDrugAsync(int issueID, IssueDrug IssueDrug)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | issueID |   |
| [`IssueDrug`](../../../../neurospec/shared/models/dto/IssueDrug.md) | IssueDrug |   |

#### DeleteIssueDrugAsync
```csharp
public async Task DeleteIssueDrugAsync(int issueID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | issueID |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
