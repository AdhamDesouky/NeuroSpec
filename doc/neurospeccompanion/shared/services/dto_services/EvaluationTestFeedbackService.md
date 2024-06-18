# EvaluationTestFeedbackService `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Shared.Services.DTO_Services
  NeuroSpecCompanion.Shared.Services.DTO_Services.EvaluationTestFeedbackService[[EvaluationTestFeedbackService]]
  end
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task` | [`DeleteAllVisitFeedbackAsync`](#deleteallvisitfeedbackasync)(`int` visitId) |
| `Task` | [`DeleteFeedbackAsync`](#deletefeedbackasync)(`int` feedbackId) |
| `Task`&lt;`IEnumerable`&lt;[`EvaluationTestFeedBack`](../../../../neurospec/shared/models/dto/EvaluationTestFeedBack.md)&gt;&gt; | [`GetAllFeedbackAsync`](#getallfeedbackasync)() |
| `Task`&lt;[`EvaluationTestFeedBack`](../../../../neurospec/shared/models/dto/EvaluationTestFeedBack.md)&gt; | [`GetFeedbackByIdAsync`](#getfeedbackbyidasync)(`int` feedbackId) |
| `Task`&lt;`IEnumerable`&lt;[`EvaluationTestFeedBack`](../../../../neurospec/shared/models/dto/EvaluationTestFeedBack.md)&gt;&gt; | [`GetFeedbackByPatientAsync`](#getfeedbackbypatientasync)(`int` patientId) |
| `Task`&lt;`IEnumerable`&lt;[`EvaluationTestFeedBack`](../../../../neurospec/shared/models/dto/EvaluationTestFeedBack.md)&gt;&gt; | [`GetFeedbackByVisitAsync`](#getfeedbackbyvisitasync)(`int` visitId) |
| `Task`&lt;[`EvaluationTestFeedBack`](../../../../neurospec/shared/models/dto/EvaluationTestFeedBack.md)&gt; | [`InsertFeedbackAsync`](#insertfeedbackasync)([`EvaluationTestFeedBack`](../../../../neurospec/shared/models/dto/EvaluationTestFeedBack.md) feedback) |

## Details
### Constructors
#### EvaluationTestFeedbackService
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Services/DTO_Services/EvaluationTestFeedbackService.cs#L16)
```csharp
public EvaluationTestFeedbackService()
```

### Methods
#### GetAllFeedbackAsync
```csharp
public async Task<IEnumerable<EvaluationTestFeedBack>> GetAllFeedbackAsync()
```

#### GetFeedbackByIdAsync
```csharp
public async Task<EvaluationTestFeedBack> GetFeedbackByIdAsync(int feedbackId)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | feedbackId |   |

#### GetFeedbackByPatientAsync
```csharp
public async Task<IEnumerable<EvaluationTestFeedBack>> GetFeedbackByPatientAsync(int patientId)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | patientId |   |

#### GetFeedbackByVisitAsync
```csharp
public async Task<IEnumerable<EvaluationTestFeedBack>> GetFeedbackByVisitAsync(int visitId)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | visitId |   |

#### InsertFeedbackAsync
```csharp
public async Task<EvaluationTestFeedBack> InsertFeedbackAsync(EvaluationTestFeedBack feedback)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| [`EvaluationTestFeedBack`](../../../../neurospec/shared/models/dto/EvaluationTestFeedBack.md) | feedback |   |

#### DeleteFeedbackAsync
```csharp
public async Task DeleteFeedbackAsync(int feedbackId)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | feedbackId |   |

#### DeleteAllVisitFeedbackAsync
```csharp
public async Task DeleteAllVisitFeedbackAsync(int visitId)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | visitId |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
