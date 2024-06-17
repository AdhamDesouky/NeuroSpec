using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using NeuroSpec.Shared.Models.DTO;
namespace NeuroSpecBackend.Model
{
    public class NeuroDbContext
    {
        private readonly IMongoDatabase _database;

        public NeuroDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDbConnection"));
            _database = client.GetDatabase("NeuroDb");

            Users = _database.GetCollection<User>("Users");
            Patients = _database.GetCollection<Patient>("Patients");
            Visits = _database.GetCollection<Visit>("Visits");
            AppointmentTypes = _database.GetCollection<AppointmentType>("AppointmentTypes");
            BookAppointmentRequests = _database.GetCollection<BookAppointmentRequest>("BookAppointmentRequests");
            AttendanceRecords = _database.GetCollection<AttendanceRecord>("AttendanceRecords");
            Prescriptions = _database.GetCollection<Prescription>("Prescriptions");
            CalendarEvents = _database.GetCollection<CalendarEvent>("CalendarEvents");
            EvaluationTests = _database.GetCollection<EvaluationTest>("EvaluationTests");
            Exercises = _database.GetCollection<Exercise>("Exercises");
            EvaluationTestFeedbacks = _database.GetCollection<EvaluationTestFeedBack>("EvaluationTestFeedbacks");
            IssueSNOMEDs = _database.GetCollection<IssueSNOMED>("IssueSNOMEDs");
            IssueDrugs = _database.GetCollection<IssueDrug>("IssueDrugs");
            Issues = _database.GetCollection<Issue>("Issues");

            MedicalRecords = _database.GetCollection<MedicalRecord>("MedicalRecords");
            Payments = _database.GetCollection<Payment>("Payments");
            ScanTests = _database.GetCollection<ScanTest>("ScanTests");
            PatientChronics = _database.GetCollection<PatientChronic>("PatientChronics");
        }

        public IMongoCollection<User> Users { get; }
        public IMongoCollection<Patient> Patients { get; }
        public IMongoCollection<Visit> Visits { get; }
        public IMongoCollection<AppointmentType> AppointmentTypes { get; }
        public IMongoCollection<AttendanceRecord> AttendanceRecords { get; }
        public IMongoCollection<Prescription> Prescriptions { get; }
        public IMongoCollection<BookAppointmentRequest> BookAppointmentRequests { get; }
        public IMongoCollection<CalendarEvent> CalendarEvents { get; }
        public IMongoCollection<EvaluationTest> EvaluationTests { get; }
        public IMongoCollection<Exercise> Exercises{ get; }
        public IMongoCollection<EvaluationTestFeedBack> EvaluationTestFeedbacks { get; }
        public IMongoCollection<IssueDrug> IssueDrugs { get; }
        public IMongoCollection<IssueSNOMED> IssueSNOMEDs { get; }
        public IMongoCollection<Issue> Issues { get; }

        public IMongoCollection<MedicalRecord> MedicalRecords { get; }
        public IMongoCollection<Payment> Payments { get; }
        public IMongoCollection<PatientChronic> PatientChronics { get; }
        public IMongoCollection<ScanTest> ScanTests { get; }
    }
}
