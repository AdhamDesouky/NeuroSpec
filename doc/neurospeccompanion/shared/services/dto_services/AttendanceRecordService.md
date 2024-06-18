# AttendanceRecordService `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Shared.Services.DTO_Services
  NeuroSpecCompanion.Shared.Services.DTO_Services.AttendanceRecordService[[AttendanceRecordService]]
  end
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task` | [`DeleteAttendanceRecordAsync`](#deleteattendancerecordasync)(`int` recordID) |
| `Task`&lt;`IEnumerable`&lt;[`AttendanceRecord`](../../../../neurospec/shared/models/dto/AttendanceRecord.md)&gt;&gt; | [`GetAllAttendanceRecordsAsync`](#getallattendancerecordsasync)() |
| `Task`&lt;[`AttendanceRecord`](../../../../neurospec/shared/models/dto/AttendanceRecord.md)&gt; | [`GetAttendanceRecordByIDAsync`](#getattendancerecordbyidasync)(`int` recordID) |
| `Task`&lt;`IEnumerable`&lt;[`AttendanceRecord`](../../../../neurospec/shared/models/dto/AttendanceRecord.md)&gt;&gt; | [`GetAttendanceRecordsByDateAsync`](#getattendancerecordsbydateasync)(`DateTime` date) |
| `Task`&lt;`IEnumerable`&lt;[`AttendanceRecord`](../../../../neurospec/shared/models/dto/AttendanceRecord.md)&gt;&gt; | [`GetUserAttendanceRecordsAsync`](#getuserattendancerecordsasync)(`int` userID) |
| `Task`&lt;[`AttendanceRecord`](../../../../neurospec/shared/models/dto/AttendanceRecord.md)&gt; | [`InsertAttendanceRecordAsync`](#insertattendancerecordasync)([`AttendanceRecord`](../../../../neurospec/shared/models/dto/AttendanceRecord.md) attendanceRecord) |
| `Task` | [`UpdateAttendanceRecordAsync`](#updateattendancerecordasync)(`int` recordID, [`AttendanceRecord`](../../../../neurospec/shared/models/dto/AttendanceRecord.md) attendanceRecord) |

## Details
### Constructors
#### AttendanceRecordService
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Services/DTO_Services/AttendanceRecordService.cs#L16)
```csharp
public AttendanceRecordService()
```

### Methods
#### GetAllAttendanceRecordsAsync
```csharp
public async Task<IEnumerable<AttendanceRecord>> GetAllAttendanceRecordsAsync()
```

#### GetAttendanceRecordByIDAsync
```csharp
public async Task<AttendanceRecord> GetAttendanceRecordByIDAsync(int recordID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | recordID |   |

#### GetAttendanceRecordsByDateAsync
```csharp
public async Task<IEnumerable<AttendanceRecord>> GetAttendanceRecordsByDateAsync(DateTime date)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `DateTime` | date |   |

#### GetUserAttendanceRecordsAsync
```csharp
public async Task<IEnumerable<AttendanceRecord>> GetUserAttendanceRecordsAsync(int userID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | userID |   |

#### InsertAttendanceRecordAsync
```csharp
public async Task<AttendanceRecord> InsertAttendanceRecordAsync(AttendanceRecord attendanceRecord)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| [`AttendanceRecord`](../../../../neurospec/shared/models/dto/AttendanceRecord.md) | attendanceRecord |   |

#### UpdateAttendanceRecordAsync
```csharp
public async Task UpdateAttendanceRecordAsync(int recordID, AttendanceRecord attendanceRecord)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | recordID |   |
| [`AttendanceRecord`](../../../../neurospec/shared/models/dto/AttendanceRecord.md) | attendanceRecord |   |

#### DeleteAttendanceRecordAsync
```csharp
public async Task DeleteAttendanceRecordAsync(int recordID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | recordID |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
