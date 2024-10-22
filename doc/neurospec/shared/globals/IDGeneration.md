# IDGeneration `Public class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpec.Shared.Globals
  NeuroSpec.Shared.Globals.IDGeneration[[IDGeneration]]
  end
```

## Members
### Methods
#### Public Static methods
| Returns | Name |
| --- | --- |
| `DateTime` | [`CalculateBirthdate`](#calculatebirthdate)(`int` age) |
| `string` | [`generateNewAdminID`](#generatenewadminid)() |
| `int` | [`generateNewAttendanceRecordID`](#generatenewattendancerecordid)(`int` userID) |
| `int` | [`generateNewCalendarEventID`](#generatenewcalendareventid)(`int` userID) |
| `int` | [`generateNewChatMessageID`](#generatenewchatmessageid)(`int` senderID) |
| `int` | [`generateNewChatRoomID`](#generatenewchatroomid)(`int` fID, `int` sID) |
| `int` | [`generateNewDoctorID`](#generatenewdoctorid)(`string` nid) |
| `int` | [`generateNewEmployeeID`](#generatenewemployeeid)(`string` nid) |
| `int` | [`generateNewExerciseID`](#generatenewexerciseid)() |
| `int` | [`generateNewIssueExerciseID`](#generatenewissueexerciseid)(`int` prescriptionID, `int` patientID) |
| `int` | [`generateNewPatientID`](#generatenewpatientid)(`string` phoneNumber) |
| `int` | [`generateNewPaymentID`](#generatenewpaymentid)(`int` patID, `DateTime` time) |
| `int` | [`generateNewPrescriptionID`](#generatenewprescriptionid)(`int` visitID, `DateTime` time) |
| `int` | [`generateNewRecordID`](#generatenewrecordid)(`int` patientID) |
| `int` | [`generateNewTestFeedBackID`](#generatenewtestfeedbackid)(`int` visitID, `int` patientID) |
| `int` | [`generateNewVisitID`](#generatenewvisitid)(`int` patID, `DateTime` time) |

## Details
### Methods
#### generateNewPatientID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L10)
```csharp
public static int generateNewPatientID(string phoneNumber)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `string` | phoneNumber |   |

#### CalculateBirthdate
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L18)
```csharp
public static DateTime CalculateBirthdate(int age)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | age |   |

#### generateNewRecordID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L26)
```csharp
public static int generateNewRecordID(int patientID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | patientID |   |

#### generateNewAttendanceRecordID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L35)
```csharp
public static int generateNewAttendanceRecordID(int userID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | userID |   |

#### generateNewCalendarEventID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L45)
```csharp
public static int generateNewCalendarEventID(int userID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | userID |   |

#### generateNewDoctorID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L55)
```csharp
public static int generateNewDoctorID(string nid)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `string` | nid |   |

#### generateNewEmployeeID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L66)
```csharp
public static int generateNewEmployeeID(string nid)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `string` | nid |   |

#### generateNewAdminID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L77)
```csharp
public static string generateNewAdminID()
```

#### generateNewVisitID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L85)
```csharp
public static int generateNewVisitID(int patID, DateTime time)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | patID |   |
| `DateTime` | time |   |

#### generateNewPaymentID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L91)
```csharp
public static int generateNewPaymentID(int patID, DateTime time)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | patID |   |
| `DateTime` | time |   |

#### generateNewPrescriptionID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L97)
```csharp
public static int generateNewPrescriptionID(int visitID, DateTime time)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | visitID |   |
| `DateTime` | time |   |

#### generateNewIssueExerciseID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L103)
```csharp
public static int generateNewIssueExerciseID(int prescriptionID, int patientID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | prescriptionID |   |
| `int` | patientID |   |

#### generateNewTestFeedBackID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L109)
```csharp
public static int generateNewTestFeedBackID(int visitID, int patientID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | visitID |   |
| `int` | patientID |   |

#### generateNewExerciseID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L115)
```csharp
public static int generateNewExerciseID()
```

#### generateNewChatRoomID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L121)
```csharp
public static int generateNewChatRoomID(int fID, int sID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | fID |   |
| `int` | sID |   |

#### generateNewChatMessageID
[*Source code*](https://github.com///blob//NeuroSpec.Shared/Globals/IDGeneration.cs#L127)
```csharp
public static int generateNewChatMessageID(int senderID)
```
##### Arguments
| Type | Name | Description |
| --- | --- | --- |
| `int` | senderID |   |

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
