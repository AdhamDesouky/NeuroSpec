# PrescriptionService `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Shared.Services.DTO_Services
  NeuroSpecCompanion.Shared.Services.DTO_Services.PrescriptionService[[PrescriptionService]]
  end
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task` | [`DeletePrescriptionAsync`](#deleteprescriptionasync)(`int` prescriptionID) |
| `Task`&lt;`IEnumerable`&lt;[`Prescription`](../../../../neurospec/shared/models/dto/Prescription.md)&gt;&gt; | [`GetAllPrescriptionsAsync`](#getallprescriptionsasync)() |
| `Task`&lt;`IEnumerable`&lt;[`Prescription`](../../../../neurospec/shared/models/dto/Prescription.md)&gt;&gt; | [`GetAllPrescriptionsByPatientIDAsync`](#getallprescriptionsbypatientidasync)(`int` patientID) |
| `Task`&lt;`IEnumerable`&lt;[`Prescription`](../../../../neurospec/shared/models/dto/Prescription.md)&gt;&gt; | [`GetAllPrescriptionsByVisitIDAsync`](#getallprescriptionsbyvisitidasync)(`int` visitID) |
| `Task`&lt;[`Prescription`](../../../../neurospec/shared/models/dto/Prescription.md)&gt; | [`GetPrescriptionByIDAsync`](#getprescriptionbyidasync)(`int` prescriptionID) |
| `Task`&lt;[`Prescription`](../../../../neurospec/shared/models/dto/Prescription.md)&gt; | [`InsertPrescriptionAsync`](#insertprescriptionasync)([`Prescription`](../../../../neurospec/shared/models/dto/Prescription.md) prescription) |
| `Task` | [`UpdatePrescriptionAsync`](#updateprescriptionasync)(`int` prescriptionID, [`Prescription`](../../../../neurospec/shared/models/dto/Prescription.md) prescription) |

## Details
### Constructors
#### PrescriptionService
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Services/DTO_Services/PrescriptionService.cs#L16)
```csharp
public PrescriptionService()
```

### Methods
#### GetAllPrescriptionsAsync
```csharp
public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
```

#### GetPrescriptionByIDAsync
```csharp
public async Task<Prescription> GetPrescriptionByIDAsync(int prescriptionID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | prescriptionID |   |

#### GetAllPrescriptionsByPatientIDAsync
```csharp
public async Task<IEnumerable<Prescription>> GetAllPrescriptionsByPatientIDAsync(int patientID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | patientID |   |

#### GetAllPrescriptionsByVisitIDAsync
```csharp
public async Task<IEnumerable<Prescription>> GetAllPrescriptionsByVisitIDAsync(int visitID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | visitID |   |

#### InsertPrescriptionAsync
```csharp
public async Task<Prescription> InsertPrescriptionAsync(Prescription prescription)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| [`Prescription`](../../../../neurospec/shared/models/dto/Prescription.md) | prescription |   |

#### UpdatePrescriptionAsync
```csharp
public async Task UpdatePrescriptionAsync(int prescriptionID, Prescription prescription)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | prescriptionID |   |
| [`Prescription`](../../../../neurospec/shared/models/dto/Prescription.md) | prescription |   |

#### DeletePrescriptionAsync
```csharp
public async Task DeletePrescriptionAsync(int prescriptionID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | prescriptionID |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
