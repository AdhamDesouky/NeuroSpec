# PaymentService `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Shared.Services.DTO_Services
  NeuroSpecCompanion.Shared.Services.DTO_Services.PaymentService[[PaymentService]]
  end
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task` | [`DeletePaymentAsync`](#deletepaymentasync)(`int` paymentID) |
| `Task`&lt;`List`&lt;[`Payment`](./neurospecsharedmodelsdto-Payment)&gt;&gt; | [`GetAllPaymentsAsync`](#getallpaymentsasync)() |
| `Task`&lt;`List`&lt;[`Payment`](./neurospecsharedmodelsdto-Payment)&gt;&gt; | [`GetDoctorPaymentsAsync`](#getdoctorpaymentsasync)(`int` doctorID) |
| `Task`&lt;`List`&lt;[`Payment`](./neurospecsharedmodelsdto-Payment)&gt;&gt; | [`GetPatientPaymentsAsync`](#getpatientpaymentsasync)(`int` patientID) |
| `Task`&lt;[`Payment`](./neurospecsharedmodelsdto-Payment)&gt; | [`GetPaymentByIDAsync`](#getpaymentbyidasync)(`int` paymentID) |
| `Task`&lt;[`Payment`](./neurospecsharedmodelsdto-Payment)&gt; | [`InsertPaymentAsync`](#insertpaymentasync)([`Payment`](./neurospecsharedmodelsdto-Payment) payment) |
| `Task` | [`UpdatePaymentAsync`](#updatepaymentasync)(`int` paymentID, [`Payment`](./neurospecsharedmodelsdto-Payment) payment) |

## Details
### Constructors
#### PaymentService
```csharp
public PaymentService()
```

### Methods
#### GetAllPaymentsAsync
```csharp
public async Task<List<Payment>> GetAllPaymentsAsync()
```

#### GetPaymentByIDAsync
```csharp
public async Task<Payment> GetPaymentByIDAsync(int paymentID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | paymentID |   |

#### GetPatientPaymentsAsync
```csharp
public async Task<List<Payment>> GetPatientPaymentsAsync(int patientID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | patientID |   |

#### GetDoctorPaymentsAsync
```csharp
public async Task<List<Payment>> GetDoctorPaymentsAsync(int doctorID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | doctorID |   |

#### InsertPaymentAsync
```csharp
public async Task<Payment> InsertPaymentAsync(Payment payment)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| [`Payment`](./neurospecsharedmodelsdto-Payment) | payment |   |

#### UpdatePaymentAsync
```csharp
public async Task UpdatePaymentAsync(int paymentID, Payment payment)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | paymentID |   |
| [`Payment`](./neurospecsharedmodelsdto-Payment) | payment |   |

#### DeletePaymentAsync
```csharp
public async Task DeletePaymentAsync(int paymentID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | paymentID |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
