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
            EvaluationTestFeedbacks = _database.GetCollection<EvaluationTestFeedBack>("EvaluationTestFeedbacks");
            OntologyTerms = _database.GetCollection<OntologyTerm>("OntologyTerms");
            IssueTests = _database.GetCollection<IssueTest>("IssueTests");
            IssueExercises = _database.GetCollection<IssueExercise>("IssueExercises");
            IssueDrugs = _database.GetCollection<IssueDrug>("IssueDrugs");
            MedicalRecords = _database.GetCollection<MedicalRecord>("MedicalRecords");
            Payments = _database.GetCollection<Payment>("Payments");
            ScanTests = _database.GetCollection<ScanTest>("ScanTests");
        }

        public IMongoCollection<User> Users { get; }
        public IMongoCollection<Patient> Patients { get; }
        public IMongoCollection<Visit> Visits { get; }
        public IMongoCollection<AppointmentType> AppointmentTypes { get; }
        public IMongoCollection<OntologyTerm> OntologyTerms { get; }
        public IMongoCollection<AttendanceRecord> AttendanceRecords { get; }
        public IMongoCollection<Prescription> Prescriptions { get; }
        public IMongoCollection<BookAppointmentRequest> BookAppointmentRequests { get; }
        public IMongoCollection<CalendarEvent> CalendarEvents { get; }
        public IMongoCollection<EvaluationTest> EvaluationTests { get; }
        public IMongoCollection<EvaluationTestFeedBack> EvaluationTestFeedbacks { get; }
        public IMongoCollection<IssueTest> IssueTests { get; }
        public IMongoCollection<IssueDrug> IssueDrugs { get; }
        public IMongoCollection<IssueExercise> IssueExercises { get; }
        public IMongoCollection<MedicalRecord> MedicalRecords { get; }
        public IMongoCollection<Payment> Payments { get; }
        public IMongoCollection<ScanTest> ScanTests { get; }
    }
}
