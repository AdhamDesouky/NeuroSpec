# CalendarEventService `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Shared.Services.DTO_Services
  NeuroSpecCompanion.Shared.Services.DTO_Services.CalendarEventService[[CalendarEventService]]
  end
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task` | [`DeleteCalendarEventAsync`](#deletecalendareventasync)(`int` eventID) |
| `Task`&lt;`IEnumerable`&lt;[`CalendarEvent`](../../../../neurospec/shared/models/dto/CalendarEvent.md)&gt;&gt; | [`GetAllCalendarEventsAsync`](#getallcalendareventsasync)() |
| `Task`&lt;[`CalendarEvent`](../../../../neurospec/shared/models/dto/CalendarEvent.md)&gt; | [`GetCalendarEventByIDAsync`](#getcalendareventbyidasync)(`int` eventID) |
| `Task`&lt;[`CalendarEvent`](../../../../neurospec/shared/models/dto/CalendarEvent.md)&gt; | [`InsertCalendarEventAsync`](#insertcalendareventasync)([`CalendarEvent`](../../../../neurospec/shared/models/dto/CalendarEvent.md) calendarEvent) |
| `Task` | [`UpdateCalendarEventAsync`](#updatecalendareventasync)(`int` eventID, [`CalendarEvent`](../../../../neurospec/shared/models/dto/CalendarEvent.md) calendarEvent) |

## Details
### Constructors
#### CalendarEventService
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Services/DTO_Services/CalendarEventService.cs#L16)
```csharp
public CalendarEventService()
```

### Methods
#### GetAllCalendarEventsAsync
```csharp
public async Task<IEnumerable<CalendarEvent>> GetAllCalendarEventsAsync()
```

#### GetCalendarEventByIDAsync
```csharp
public async Task<CalendarEvent> GetCalendarEventByIDAsync(int eventID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | eventID |   |

#### InsertCalendarEventAsync
```csharp
public async Task<CalendarEvent> InsertCalendarEventAsync(CalendarEvent calendarEvent)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| [`CalendarEvent`](../../../../neurospec/shared/models/dto/CalendarEvent.md) | calendarEvent |   |

#### UpdateCalendarEventAsync
```csharp
public async Task UpdateCalendarEventAsync(int eventID, CalendarEvent calendarEvent)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | eventID |   |
| [`CalendarEvent`](../../../../neurospec/shared/models/dto/CalendarEvent.md) | calendarEvent |   |

#### DeleteCalendarEventAsync
```csharp
public async Task DeleteCalendarEventAsync(int eventID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | eventID |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
