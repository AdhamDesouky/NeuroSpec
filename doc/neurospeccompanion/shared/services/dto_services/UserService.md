# UserService `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.Shared.Services.DTO_Services
  NeuroSpecCompanion.Shared.Services.DTO_Services.UserService[[UserService]]
  end
```

## Members
### Methods
#### Public  methods
| Returns | Name |
| --- | --- |
| `Task` | [`DeleteUserAsync`](#deleteuserasync)(`int` id) |
| `Task`&lt;`IEnumerable`&lt;[`User`](../../../../neurospec/shared/models/dto/User.md)&gt;&gt; | [`GetAllAdminsAsync`](#getalladminsasync)() |
| `Task`&lt;`IEnumerable`&lt;[`User`](../../../../neurospec/shared/models/dto/User.md)&gt;&gt; | [`GetAllDoctorsAsync`](#getalldoctorsasync)() |
| `Task`&lt;`IEnumerable`&lt;[`User`](../../../../neurospec/shared/models/dto/User.md)&gt;&gt; | [`GetAllEmployeesAsync`](#getallemployeesasync)() |
| `Task`&lt;`IEnumerable`&lt;[`User`](../../../../neurospec/shared/models/dto/User.md)&gt;&gt; | [`GetAllUsersAsync`](#getallusersasync)() |
| `Task`&lt;[`User`](../../../../neurospec/shared/models/dto/User.md)&gt; | [`GetUserByIdAsync`](#getuserbyidasync)(`int` id) |
| `Task`&lt;[`User`](../../../../neurospec/shared/models/dto/User.md)&gt; | [`GetUserByNIDAsync`](#getuserbynidasync)(`string` nid) |
| `Task`&lt;[`User`](../../../../neurospec/shared/models/dto/User.md)&gt; | [`InsertUserAsync`](#insertuserasync)([`User`](../../../../neurospec/shared/models/dto/User.md) user) |

## Details
### Constructors
#### UserService
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Services/DTO_Services/UserService.cs#L16)
```csharp
public UserService()
```

### Methods
#### GetAllUsersAsync
```csharp
public async Task<IEnumerable<User>> GetAllUsersAsync()
```

#### GetUserByIdAsync
```csharp
public async Task<User> GetUserByIdAsync(int id)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | id |   |

#### GetAllEmployeesAsync
```csharp
public async Task<IEnumerable<User>> GetAllEmployeesAsync()
```

#### GetAllDoctorsAsync
```csharp
public async Task<IEnumerable<User>> GetAllDoctorsAsync()
```

#### GetAllAdminsAsync
```csharp
public async Task<IEnumerable<User>> GetAllAdminsAsync()
```

#### GetUserByNIDAsync
```csharp
public async Task<User> GetUserByNIDAsync(string nid)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `string` | nid |   |

#### InsertUserAsync
```csharp
public async Task<User> InsertUserAsync(User user)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| [`User`](../../../../neurospec/shared/models/dto/User.md) | user |   |

#### DeleteUserAsync
```csharp
public async Task DeleteUserAsync(int id)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | id |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
