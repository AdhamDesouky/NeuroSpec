# NeuroSpec: A System for Parkinson’s Disease Assessment

## About The Project
This paper presents the development and implementation of a specialized biomedical information system designed to assess Parkinson's disease symptoms through interactive neuro games. The system consists of two integrated components: a patient-facing mobile application and a doctor-facing management system. The patient application features four distinct games aimed at measuring various symptoms, while the doctor’s interface facilitates data analysis and patient management. The system adheres to FHIR standards for interoperability and leverages advanced technologies such as machine learning and natural language processing to enhance its functionality.

## Built With
- **C#**
- **MUAI .NET** version 8.0.60
- **MongoDB** version 2.26.0
- **Firebase** version 1.0.3
- **Hl7.Fhir.R5** version 5.8.1

## Prerequisites
Before installing the system, ensure you have the following installed:
- [Visual Studio](https://visualstudio.microsoft.com/)
- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [.NET MAUI](https://dotnet.microsoft.com/apps/maui)
- [MongoDB](https://www.mongodb.com/try/download/community)
- [Firebase CLI](https://firebase.google.com/docs/cli)
- [HL7 SDK or Library](https://www.hl7.org/)
- [FHIR SDK or Library](https://www.hl7.org/fhir/)

## Installation Steps
1. **Clone the Repository**
   ```bash
   git clone <repository_url>
   ```
2. **Open the Project in Visual Studio**
   - Navigate to the cloned repository folder and open the solution file (.sln).
   - Restore NuGet Packages.
3. **Install .NET MAUI**
   ```bash
   dotnet workload install maui
   ```
4. **Build the Project**
   - Open the MAUI project in Visual Studio.
   - Ensure all dependencies are restored.
   - Build the project by selecting `Build > Build Solution`.
5. **Set Environment Variables**
   - Set any required environment variables as per the project's needs.

## Backend Controllers
### Appointment Type Controller
Manages appointment types.
- **Insert**: Adds a new appointment type (InsertAppointmentType).
- **Update**: Modifies an existing appointment type (UpdateAppointmentType).
- **Delete**: Removes an appointment type by ID (DeleteAppointmentType).
- **Get by ID**: Retrieves an appointment type by its ID (GetAppointmentTypeByID).
- **Get All**: Fetches all appointment types (GetAllAppointmentTypes).
- **Private Method**: `AppointmentTypeExists(int id)` checks if an appointment type with a given ID exists in the database.

### Attendance Record Controller
Manages attendance records.
- **Insert**: Adds a new attendance record (InsertAttendanceRecord).
- **Update**: Modifies an existing attendance record (UpdateAttendanceRecord).
- **Delete**: Removes an attendance record by ID (DeleteAttendanceRecord).
- **Get by ID**: Retrieves an attendance record by its ID (GetAttendanceRecordByID).
- **Get All**: Fetches all attendance records (GetAllAttendanceRecords).
- **Get by Date**: Fetches attendance records for a specific date (GetAttendanceRecordsByDate).
- **Get by User**: Fetches attendance records for a specific user by their user ID (GetUserAttendanceRecords).
- **Private Method**: `AttendanceRecordExists(int recordID)` checks if an attendance record with a given ID exists in the database.

### Book Appointment Controller
Manages booking appointment requests.
- **Insert**: Adds a new book appointment request (InsertBookAppointmentRequest).
- **Update**: Modifies an existing book appointment request (UpdateBookAppointmentRequest).
- **Delete**: Removes a book appointment request by ID (DeleteBookAppointmentRequest).
- **Get by ID**: Retrieves a book appointment request by its ID (GetBookAppointmentRequestByID).
- **Mark as Confirmed**: Marks an appointment request as confirmed (MarkAsDone).
- **Get All Not Confirmed**: Fetches all book appointment requests that are not confirmed (GetAllNotConfirmedBookAppointmentRequests).
- **Get by Patient ID**: Fetches all book appointment requests for a specific patient by their ID (GetAllBookAppointmentRequestsByPatientID).

### Calendar Event Controller
Manages calendar events.
- **Insert**: Adds a new calendar event (InsertCalendarEvent).
- **Update**: Modifies an existing calendar event (UpdateCalendarEvent).
- **Delete**: Removes a calendar event by ID (DeleteCalendarEvent).
- **Get by ID**: Retrieves a calendar event by its ID (GetCalendarEventByID).
- **Get All**: Fetches all calendar events (GetAllCalendarEvents).
- **Get by User ID and Date**: Fetches calendar events for a specific user and date (GetCalendarEventsByUserIDAndDate).
- **Private Method**: `CalendarEventExists(int eventID)` checks if a calendar event with a given ID exists in the database.

### Evaluation Test Controller
Manages evaluation tests.
- **Get All Tests**: Retrieves all evaluation tests (GetAllTests).
- **Get Test by ID**: Retrieves a specific evaluation test by its ID (GetTestById).
- **Insert Test**: Adds a new evaluation test (InsertTest).
- **Update Test**: Modifies an existing evaluation test (UpdateTest).
- **Delete Test**: Removes an evaluation test by ID (DeleteTest).
- **Private Method**: `TestExists(int testId)` checks if an evaluation test with a given ID exists in the database.

### Evaluation Test Feedback Controller
Manages evaluation test feedback.
- **Insert Feedback**: Adds a new evaluation test feedback (InsertFeedback).
- **Get All Feedback**: Retrieves all evaluation test feedback (GetAllFeedback).
- **Get Feedback by ID**: Retrieves a specific evaluation test feedback by its ID (GetFeedbackById).
- **Get Feedback by Patient**: Retrieves evaluation test feedback for a specific patient by their ID (GetFeedbackByPatient).
- **Get Feedback by Visit**: Retrieves evaluation test feedback for a specific visit by its ID (GetFeedbackByVisit).
- **Delete Feedback**: Removes an evaluation test feedback by ID (DeleteFeedback).
- **Delete All Visit Feedback**: Removes all evaluation test feedback associated with a specific visit by its ID (DeleteAllVisitFeedback).

### Exercise Controller
Manages exercises.
- **Insert Exercise**: Creates a new exercise entry (InsertExercise).
- **Get All Exercises**: Retrieves all exercises (GetAllExercises).
- **Get Exercise by ID**: Retrieves a specific exercise by its ID (GetExerciseById).
- **Delete Exercise**: Deletes an exercise by its ID (DeleteExercise).

### Issue Drug Controller
Manages drugs issued to patients.
- **Insert Issue Drug**: Creates a new entry for an issued drug (InsertIssueDrug).
- **Get All Issue Drugs**: Retrieves all issued drugs (GetAllIssueDrugs).
- **Get Issue Drug by ID**: Retrieves a specific issued drug by its ID (GetIssueDrugById).
- **Get Issue Drugs by Patient ID**: Retrieves all issued drugs for a specific patient by their ID (GetAllIssueDrugsByPatientID).
- **Get Issue Drugs by Prescription ID**: Retrieves all issued drugs for a specific prescription by its ID (GetAllIssueDrugsByPrescriptionID).
- **Update Issue Drug**: Updates information about an issued drug (UpdateIssueDrug).
- **Delete Issue Drug**: Deletes an issued drug by its ID (DeleteIssueDrug).
- **Private Method**: `IssueDrugExists(int issueID)` checks if an issued drug with a given ID exists in the database.

### Issue Scan Controller
Manages issue scans.
- **Insert**: Adds a new issue scan (InsertIssueScan).
- **Update**: Modifies an existing issue scan (UpdateIssueScan).
- **Delete**: Removes an issue scan by ID (DeleteIssueScan).
- **Get by ID**: Retrieves an issue scan by its ID (GetIssueScanById).
- **Get All**: Fetches all issue scans (GetAllIssueScans).
- **Get by Patient**: Retrieves issue scans associated with a specific patient (GetAllIssueScansByPatientID).
- **Get by Prescription**: Retrieves issue scans associated with a specific prescription (GetAllIssueScansByPrescriptionID).

### Medical Record Controller
Manages medical records.
- **InsertMedicalRecord**: Creates a new medical record entry in the database.
- **GetAllMedicalRecords**: Retrieves all medical records from the database.
- **GetMedicalRecordByID**: Retrieves a specific medical record by its ID.
- **GetMedicalRecordOnFHIRByID**: Retrieves a specific medical record formatted as a FHIR DiagnosticReport using FHIRMapper.
- **GetAllPatientRecords**: Retrieves all medical records associated with a specific patient ID.
- **UpdateMedicalRecord**: Updates an existing medical record identified by its ID.
- **DeleteMedicalRecord**: Deletes a medical record from the database based on its ID.
- **MedicalRecordExists**: Checks if a medical record exists based on its ID.

### Ontology Term Controller
Manages ontology terms.
- **Insert**: Adds a new ontology term (InsertOntologyTerm).
- **Update**: Modifies an existing ontology term (UpdateOntologyTerm).
- **Delete**: Removes an ontology term by ID (DeleteOntologyTerm).
- **Get by ID**: Retrieves an ontology term by its ID (GetOntologyTermById).

### Patient Controller
Manages patient information.
- **GetAllChronics**: Retrieves all patient chronic records from the database.
- **InsertChronic**: Adds a new patient chronic record to

 the database.
- **DeleteChronic**: Deletes a patient chronic record from the database based on its ID.
- **InsertPatientInfo**: Adds new patient information to the database.
- **GetAllPatientInfo**: Retrieves all patient information from the database.
- **UpdatePatientInfo**: Updates existing patient information in the database.
- **DeletePatientInfo**: Deletes patient information from the database based on patient ID.
- **GetPatientInfoByID**: Retrieves specific patient information from the database based on patient ID.
- **GetPatientFullInfo**: Retrieves detailed patient information, including contacts and medical records, based on patient ID.
- **GetChronicByID**: Retrieves specific patient chronic information from the database based on chronic ID.
- **UpdateChronic**: Updates existing patient chronic information in the database.
- **InsertPatientContact**: Adds new patient contact information to the database.
- **UpdatePatientContact**: Updates existing patient contact information in the database.
- **DeletePatientContact**: Deletes patient contact information from the database based on contact ID.
- **GetPatientContactByID**: Retrieves specific patient contact information from the database based on contact ID.
- **GetAllContacts**: Retrieves all patient contact records from the database.
- **InsertPatientVisit**: Adds new patient visit information to the database.
- **GetAllVisits**: Retrieves all patient visit records from the database.
- **GetPatientVisitByID**: Retrieves specific patient visit information from the database based on visit ID.
- **DeletePatientVisit**: Deletes a patient visit record from the database based on visit ID.
- **UpdatePatientVisit**: Updates existing patient visit information in the database.

### Prescription Controller
Manages prescriptions.
- **InsertPrescription**: Adds a new prescription to the database.
- **GetAllPrescriptions**: Retrieves all prescriptions from the database.
- **GetPrescriptionByID**: Retrieves a specific prescription from the database based on its ID.
- **UpdatePrescription**: Updates an existing prescription in the database.
- **DeletePrescription**: Deletes a prescription from the database based on its ID.
- **GetAllPrescriptionsByPatientID**: Retrieves all prescriptions associated with a specific patient ID.
- **GetPrescriptionFHIRByID**: Retrieves a specific prescription formatted as a FHIR resource using FHIRMapper.
- **PrescriptionExists**: Checks if a prescription exists based on its ID.

### Study Controller
Manages studies.
- **Insert**: Adds a new study (InsertStudy).
- **Update**: Modifies an existing study (UpdateStudy).
- **Delete**: Removes a study by ID (DeleteStudy).
- **Get by ID**: Retrieves a study by its ID (GetStudyById).
- **Get All**: Fetches all studies (GetAllStudies).

## Usage
After installation, the system can be used by medical professionals to assess Parkinson's disease symptoms through the neuro games and manage patient data effectively. The data collected from patient interactions with the neuro games is analyzed using machine learning algorithms to provide insights and aid in treatment decisions.

## Contributing
Contributions to the project are welcome. Please follow these steps:
1. Fork the project repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/YourFeature`).
5. Open a pull request.
