# AppointmentTypeService `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Shared.Services.DTO_Services
  NeuroSpecCompanion.Shared.Services.DTO_Services.AppointmentTypeService[[AppointmentTypeService]]
  end
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task` | [`DeleteAppointmentTypeAsync`](#deleteappointmenttypeasync)(`int` id) |
| `Task`&lt;`List`&lt;[`AppointmentType`](./neurospecsharedmodelsdto-AppointmentType)&gt;&gt; | [`GetAllAppointmentTypesAsync`](#getallappointmenttypesasync)() |
| `Task`&lt;[`AppointmentType`](./neurospecsharedmodelsdto-AppointmentType)&gt; | [`GetAppointmentTypeByIDAsync`](#getappointmenttypebyidasync)(`int` id) |
| `Task`&lt;[`AppointmentType`](./neurospecsharedmodelsdto-AppointmentType)&gt; | [`InsertAppointmentTypeAsync`](#insertappointmenttypeasync)([`AppointmentType`](./neurospecsharedmodelsdto-AppointmentType) appointmentType) |
| `Task` | [`UpdateAppointmentTypeAsync`](#updateappointmenttypeasync)(`int` id, [`AppointmentType`](./neurospecsharedmodelsdto-AppointmentType) appointmentType) |

## Details
### Constructors
#### AppointmentTypeService
```csharp
public AppointmentTypeService()
```

### Methods
#### GetAllAppointmentTypesAsync
```csharp
public async Task<List<AppointmentType>> GetAllAppointmentTypesAsync()
```

#### GetAppointmentTypeByIDAsync
```csharp
public async Task<AppointmentType> GetAppointmentTypeByIDAsync(int id)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | id |   |

#### InsertAppointmentTypeAsync
```csharp
public async Task<AppointmentType> InsertAppointmentTypeAsync(AppointmentType appointmentType)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| [`AppointmentType`](./neurospecsharedmodelsdto-AppointmentType) | appointmentType |   |

#### UpdateAppointmentTypeAsync
```csharp
public async Task UpdateAppointmentTypeAsync(int id, AppointmentType appointmentType)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | id |   |
| [`AppointmentType`](./neurospecsharedmodelsdto-AppointmentType) | appointmentType |   |

#### DeleteAppointmentTypeAsync
```csharp
public async Task DeleteAppointmentTypeAsync(int id)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | id |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
