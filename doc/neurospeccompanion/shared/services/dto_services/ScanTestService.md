# ScanTestService `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Shared.Services.DTO_Services
  NeuroSpecCompanion.Shared.Services.DTO_Services.ScanTestService[[ScanTestService]]
  end
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task` | [`DeleteScanTestAsync`](#deletescantestasync)(`int` scanTestID) |
| `Task`&lt;`IEnumerable`&lt;[`ScanTest`](../../../../neurospec/shared/models/dto/ScanTest.md)&gt;&gt; | [`GetAllScanTestsAsync`](#getallscantestsasync)() |
| `Task`&lt;[`ScanTest`](../../../../neurospec/shared/models/dto/ScanTest.md)&gt; | [`GetScanTestByIdAsync`](#getscantestbyidasync)(`int` scanTestID) |
| `Task`&lt;[`ScanTest`](../../../../neurospec/shared/models/dto/ScanTest.md)&gt; | [`InsertScanTestAsync`](#insertscantestasync)([`ScanTest`](../../../../neurospec/shared/models/dto/ScanTest.md) scanTest) |
| `Task` | [`UpdateScanTestAsync`](#updatescantestasync)(`int` scanTestID, [`ScanTest`](../../../../neurospec/shared/models/dto/ScanTest.md) scanTest) |

## Details
### Constructors
#### ScanTestService
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Services/DTO_Services/ScanTestService.cs#L16)
```csharp
public ScanTestService()
```

### Methods
#### GetAllScanTestsAsync
```csharp
public async Task<IEnumerable<ScanTest>> GetAllScanTestsAsync()
```

#### GetScanTestByIdAsync
```csharp
public async Task<ScanTest> GetScanTestByIdAsync(int scanTestID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | scanTestID |   |

#### InsertScanTestAsync
```csharp
public async Task<ScanTest> InsertScanTestAsync(ScanTest scanTest)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| [`ScanTest`](../../../../neurospec/shared/models/dto/ScanTest.md) | scanTest |   |

#### UpdateScanTestAsync
```csharp
public async Task UpdateScanTestAsync(int scanTestID, ScanTest scanTest)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | scanTestID |   |
| [`ScanTest`](../../../../neurospec/shared/models/dto/ScanTest.md) | scanTest |   |

#### DeleteScanTestAsync
```csharp
public async Task DeleteScanTestAsync(int scanTestID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | scanTestID |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
