using clinical.BaseClasses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using static clinical.BaseClasses.ontology;

namespace clinical
{
    class DB
    {
        static MySqlConnection connection;
        public DB()
        {
            string connectionString = "server=localhost;database=neuro;user=root;password=root;";

            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Message: " + ex.Message);

                }
            }
        }

        /// <User>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        /// </User>

        public static void InsertUser(User user)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO User (userID, firstName, lastName, gender, hireDate, birthdate, address, phoneNumber, email, nationalID, password) VALUES (@userID, @firstName, @lastName, @gender, @hireDate, @birthdate, @address, @phoneNumber, @email, @nationalID, @password)", connection))
                    {
                        cmd.Parameters.AddWithValue("@userID", user.UserID);
                        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", user.LastName);
                        cmd.Parameters.AddWithValue("@gender", user.Gender == "Male");
                        cmd.Parameters.AddWithValue("@hireDate", user.HireDate);
                        cmd.Parameters.AddWithValue("@birthdate", user.Birthdate);
                        cmd.Parameters.AddWithValue("@address", user.Address);
                        cmd.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@nationalID", user.NationalID);
                        cmd.Parameters.AddWithValue("@password", user.Password);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e) { }
            }

        }

        public static void DeleteUser(int userID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM User WHERE userID = @userID", connection))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static List<User> GetAllUserswRecordsByDate(DateTime date)
        {
            List<User> users = new List<User>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT u.* FROM User u JOIN AttendanceRecord ar ON u.userID = ar.userID WHERE DATE(ar.timeStamp) = DATE(@date)", connection))
                    {
                        cmd.Parameters.AddWithValue("@date", date);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = MapUser(reader);

                                users.Add(user);
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return users;
        }
        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM User", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = MapUser(reader);

                                users.Add(user);
                            }
                        }

                    }
                }
                catch (Exception ex) { }
            }

            return users;
        }

        public static User GetUserById(int userID)
        {

            using (connection)
            {
                User user = null;
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM User WHERE userID = @userID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);


                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            //MessageBox.Show(reader.Read().ToString());

                            if (reader.Read())
                            {
                                user = MapUser(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return user;

            }

        }

        public static List<User> GetAllEmployees()
        {
            List<User> employees = new List<User>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM User WHERE SUBSTRING(userID, 1, 1) = '3'", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = MapUser(reader);
                                employees.Add(user);
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            }
            return employees;
        }

        public static List<User> GetAllDoctors()
        {
            List<User> Doctors = new List<User>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM User WHERE SUBSTRING(userID, 1, 1) = '2'", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = MapUser(reader);
                                Doctors.Add(user);
                            }
                        }
                    }
                }
                catch (Exception e) { }
            }
            return Doctors;
        }

        public static List<User> GetAllAdmins()
        {
            List<User> admins = new List<User>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM User WHERE SUBSTRING(userID, 1, 1) = '2'", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = MapUser(reader);
                                admins.Add(user);
                            }
                        }
                    }
                }
                catch (Exception e) { }
            }

            return admins;
        }

        public static List<User> GetDoctorsWithVisitsOnDate(DateTime specificDate)
        {
            List<User> patients = new List<User>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT p.* FROM user p JOIN visit v ON p.userID = v.userID WHERE DATE(v.timeStamp) = @specificDate", connection))
                    {
                        cmd.Parameters.AddWithValue("@specificDate", specificDate.Date);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User patient = MapUser(reader);
                                patients.Add(patient);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return patients;
        }

        public static User GetUserByNID(string nid)
        {
            using(connection)
            {
                User user = null;
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM User WHERE nationalID LIKE @userID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", nid);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            //MessageBox.Show(reader.Read().ToString());

                            if (reader.Read())
                            {
                                user = MapUser(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return user;

            }
        }

        private static User MapUser(MySqlDataReader reader)
        {
            return new User(
                Convert.ToInt32(reader["userID"]),
                reader["firstName"].ToString(),
                reader["lastName"].ToString(),
                (bool)reader["gender"] ? "Male" : "Female",
                Convert.ToDateTime(reader["hireDate"]),
                Convert.ToDateTime(reader["birthdate"]),
                reader["address"].ToString(),
                reader["phoneNumber"].ToString(),
                reader["email"].ToString(),
                reader["nationalID"].ToString(),
                reader["password"].ToString()
            );
        }

        /// user work days
        ///////////////////////////////////////////////////////        
        ///

        public static List<int> GetUserWorkDaysById(int userID)
        {
            List<int> days = new List<int>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT workDay FROM userWorkDayRelation WHERE userID = @userID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);


                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int d = Convert.ToInt32(reader);
                                days.Add(d);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return days;

            }
        }

        public static void updateUserWorkDays(int userID, List<int> days)
        {
            DeleteUserWorkDaysById(userID);
            foreach (int i in days)
            {
                InsertUserWorkDay(userID, i);

            }
        }

        public static void DeleteUserWorkDaysById(int userID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "Delete FROM userWorkDayRelation WHERE userID = @userID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }


            }
        }

        public static void InsertUserWorkDay(int userId, int day)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO userWorkDayRelation (userID, workDay) VALUES (@userID, @day)", connection))
                    {
                        cmd.Parameters.AddWithValue("@userID", userId);
                        cmd.Parameters.AddWithValue("@day", day);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

        }



        ///<Patient>
        /////////////////////////complete?////////////////////////////////
        ///</Patient>

        public static Patient MapPatient(MySqlDataReader reader)
        {

            Patient p = new Patient(
                Convert.ToInt32(reader["patientID"]),
                reader["firstName"].ToString(),
                reader["lastName"].ToString(),
                reader["password"].ToString(),
                Convert.ToDateTime(reader["birthdate"]),
                reader["gender"].ToString(),
                reader["phoneNumber"].ToString(),
                reader["email"].ToString(),
                reader["address"].ToString(),
                Convert.ToInt32(reader["DoctorID"]),
                Convert.ToBoolean(reader["referred"]),
                Convert.ToBoolean(reader["previouslyTreated"]),
                Convert.ToDouble(reader["height"]),
                Convert.ToDouble(reader["weight"]),
                Convert.ToDouble(reader["dueAmount"]),
                reader["referringName"].ToString(),
                reader["referringPN"].ToString()


            );
            return p;
        }
        public static Patient GetPatientById(int patientID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Patient WHERE patientID = @patientID", connection))
                    {
                        cmd.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                return MapPatient(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }

            return null;
        }
        public static List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM patient", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Patient patient = MapPatient(reader);
                                patients.Add(patient);
                                //MessageBox.Show("done");

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);

                }
            }

            return patients;
        }
        public static List<Patient> GetPatientsWithVisitsOnDate(DateTime specificDate)
        {
            List<Patient> patients = new List<Patient>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT p.* FROM patient p JOIN visit v ON p.patientID = v.patientID WHERE DATE(v.timeStamp) = @specificDate", connection))
                    {
                        cmd.Parameters.AddWithValue("@specificDate", specificDate.Date);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Patient patient = MapPatient(reader);
                                patients.Add(patient);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return patients;
        }
        public static List<Patient> GetPatientsWithVisitsOnDateByDoctorID(int DoctorID, DateTime specificDate)
        {
            List<Patient> patients = new List<Patient>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT p.* FROM patient p JOIN visit v ON p.patientID = v.patientID WHERE DATE(v.timeStamp) = @specificDate AND v.userID=@DoctorID", connection))
                    {
                        cmd.Parameters.AddWithValue("@specificDate", specificDate.Date);
                        cmd.Parameters.AddWithValue("@DoctorID", DoctorID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Patient patient = MapPatient(reader);
                                patients.Add(patient);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return patients;
        }
        public static List<Patient> GetAllPatientsByDoctorID(int DoctorID)
        {
            List<Patient> patients = new List<Patient>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Patient WHERE DoctorID = @DoctorID", connection))
                    {
                        cmd.Parameters.AddWithValue("@DoctorID", DoctorID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Patient patient = MapPatient(reader);
                                patients.Add(patient);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }

            return patients;
        }
        public static void DeletePatient(int patientID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM Patient WHERE patientID = @patientID", connection))
                    {
                        cmd.Parameters.AddWithValue("@patientID", patientID);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public static void InsertPatient(Patient patient)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO Patient (patientID, firstName, lastName, password, birthdate, gender, phoneNumber, email, address, referred, previouslyTreated, height, weight, dueAmount, DoctorID, referringName, referringPN, activePackageID, remainingSessions)" +
                        "VALUES (@patientID, @firstName, @lastName, @password, @birthdate, @gender, @phoneNumber, @email, @address, @referred, @previouslyTreated, @height, @weight, @dueAmount, @DoctorID, @referringName, @referringPN, @activePackageID, @remainingSessions)", connection))
                    {
                        cmd.Parameters.AddWithValue("@patientID", patient.PatientID);
                        cmd.Parameters.AddWithValue("@firstName", patient.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", patient.LastName);
                        cmd.Parameters.AddWithValue("@password", patient.Password);
                        cmd.Parameters.AddWithValue("@birthdate", patient.Birthdate);
                        cmd.Parameters.AddWithValue("@gender", patient.Gender == "Male");
                        cmd.Parameters.AddWithValue("@phoneNumber", patient.PhoneNumber);
                        cmd.Parameters.AddWithValue("@email", patient.Email);
                        cmd.Parameters.AddWithValue("@address", patient.Address);
                        cmd.Parameters.AddWithValue("@DoctorID", patient.DoctorID);
                        cmd.Parameters.AddWithValue("@referred", patient.Referred == true);
                        cmd.Parameters.AddWithValue("@previouslyTreated", patient.PreviouslyTreated == true);
                        cmd.Parameters.AddWithValue("@height", patient.Height);
                        cmd.Parameters.AddWithValue("@weight", patient.Weight);
                        cmd.Parameters.AddWithValue("@dueAmount", patient.DueAmount);
                        cmd.Parameters.AddWithValue("@referringName", patient.referringName);
                        cmd.Parameters.AddWithValue("@referringPN", patient.referringPN);


                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static void UpdatePatient(Patient patient)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE Patient SET firstName = @firstName, lastName = @lastName,password=@password, birthdate = @birthdate, gender = @gender, phoneNumber=@phoneNumber," +
                        " email = @email, address = @address, referred = @referred, previouslyTreated = @previouslyTreated" +
                        ", height = @height, weight = @weight, dueAmount = @dueAmount, DoctorID = @DoctorID, referringName = @referringName, referringPN = @referringPN, remainingSessions=@remainingSessions WHERE patientID = @patientID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patient.PatientID);
                        command.Parameters.AddWithValue("@firstName", patient.FirstName);
                        command.Parameters.AddWithValue("@lastName", patient.LastName);
                        command.Parameters.AddWithValue("@password", patient.Password);
                        command.Parameters.AddWithValue("@birthdate", patient.Birthdate);
                        command.Parameters.AddWithValue("@gender", patient.Gender == "Male");
                        command.Parameters.AddWithValue("@phoneNumber", patient.PhoneNumber);
                        command.Parameters.AddWithValue("@email", patient.Email);
                        command.Parameters.AddWithValue("@address", patient.Address);
                        command.Parameters.AddWithValue("@DoctorID", patient.DoctorID);
                        command.Parameters.AddWithValue("@referred", patient.Referred == true);
                        command.Parameters.AddWithValue("@previouslyTreated", patient.PreviouslyTreated == true);
                        command.Parameters.AddWithValue("@height", patient.Height);
                        command.Parameters.AddWithValue("@weight", patient.Weight);
                        command.Parameters.AddWithValue("@dueAmount", patient.DueAmount);
                        command.Parameters.AddWithValue("@referringName", patient.referringName);
                        command.Parameters.AddWithValue("@referringPN", patient.referringPN);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static List<Patient> GetAllPatientsToday()
        {
            List<Patient> patientsToday = new List<Patient>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT DISTINCT PatientID FROM Visit WHERE DATE(TimeStamp) = CURDATE()";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                patientsToday.Add(GetPatientById(Convert.ToInt32(reader["PatientID"])));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return patientsToday;
        }

        public static List<OntologyTerm> GetAllDiseasesByPatientID(int patientID)
        {
            List<OntologyTerm> chronicDiseases = new List<OntologyTerm>();

            using (connection)
            {
                connection.Open();

                string query = "SELECT diseaseName FROM patientChronicRelation WHERE patientID=@patientID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@patientID", patientID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OntologyTerm term = searchForTerm(reader["diseaseName"].ToString());
                        
                            if(term!=null)
                                chronicDiseases.Add(term);
                        }
                    }
                }
            }

            return chronicDiseases;
        }
        public static void InsertPatientDiseases(string diseaseName, int patientID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO patientChronicRelation (diseaseName, patientID) " +
                        "VALUES (@chronicID, @chronicDiseaseName)",
                        connection))
                    {

                        cmd.Parameters.AddWithValue("@chronicID", diseaseName);
                        cmd.Parameters.AddWithValue("@chronicDiseaseName", patientID);

                        cmd.ExecuteNonQuery();


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        /////ChronicDisease
        //////////////////////////////////////////////////////////
        /////

        //public static ChronicDisease GetChronicDiseaseById(int chronicDiseaseID)
        //{
        //    using (connection)
        //    {
        //        try
        //        {
        //            if (connection.State == ConnectionState.Closed) connection.Open();
        //            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM ChronicDisease WHERE chronicDiseaseID = @chronicDiseaseID", connection))
        //            {
        //                cmd.Parameters.AddWithValue("@chronicDiseaseID", chronicDiseaseID);

        //                using (MySqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        return MapChronicDisease(reader);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle exception (log, throw, etc.)
        //            MessageBox.Show(ex.Message);
        //        }
        //    }

        //    return null;
        //}

        //public static List<ChronicDisease> GetAllChronicDiseases()
        //{
        //    List<ChronicDisease> chronicDiseases = new List<ChronicDisease>();
        //    using (connection)
        //    {
        //        try
        //        {
        //            if (connection.State == ConnectionState.Closed) connection.Open();
        //            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM ChronicDisease", connection))
        //            {
        //                using (MySqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        ChronicDisease chronicDisease = MapChronicDisease(reader);
        //                        chronicDiseases.Add(chronicDisease);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle exception (log, throw, etc.)
        //            MessageBox.Show(ex.Message);
        //        }
        //    }

        //    return chronicDiseases;
        //}
        //public static void DeleteChronicDisease(int chronicDiseaseID)
        //{
        //    using (connection)
        //    {
        //        try
        //        {
        //            if (connection.State == ConnectionState.Closed) connection.Open();
        //            using (MySqlCommand cmd = new MySqlCommand("DELETE FROM ChronicDisease WHERE chronicDiseaseID = @chronicDiseaseID", connection))
        //            {
        //                cmd.Parameters.AddWithValue("@chronicDiseaseID", chronicDiseaseID);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle exception (log, throw, etc.)
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //}

        public static void DeletePatientsChronicDiseases(int patientID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM patientChronicRelation WHERE patientID = @chronicDiseaseID", connection))
                    {
                        cmd.Parameters.AddWithValue("@chronicDiseaseID", patientID);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //public static void DeletePatientsInjuries(int patientID)
        //{
        //    using (connection)
        //    {
        //        try
        //        {
        //            if (connection.State == ConnectionState.Closed) connection.Open();
        //            using (MySqlCommand cmd = new MySqlCommand("DELETE FROM patientInjuryRelation WHERE patientID = @chronicDiseaseID", connection))
        //            {
        //                cmd.Parameters.AddWithValue("@chronicDiseaseID", patientID);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle exception (log, throw, etc.)
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //}

        //public static void InsertChronicDisease(ChronicDisease chronicDisease)
        //{
        //    using (connection)
        //    {
        //        try
        //        {
        //            if (connection.State == ConnectionState.Closed) connection.Open();
        //            using (MySqlCommand cmd = new MySqlCommand(
        //                "INSERT INTO ChronicDisease (chronicDiseaseID, chronicDiseaseName, description) " +
        //                "VALUES (@chronicDiseaseID, @chronicDiseaseName, @description)",
        //                connection))
        //            {
        //                cmd.Parameters.AddWithValue("@chronicDiseaseID", chronicDisease.ChronicDiseaseID);
        //                cmd.Parameters.AddWithValue("@chronicDiseaseName", chronicDisease.ChronicDiseaseName);
        //                cmd.Parameters.AddWithValue("@description", chronicDisease.Description);

        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle exception (log, throw, etc.)
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //}



        //internal static void updateChronicDisease(ChronicDisease ch)
        //{
        //    using (connection)
        //    {
        //        try
        //        {
        //            if (connection.State == ConnectionState.Closed) connection.Open();
        //            using (MySqlCommand cmd = new MySqlCommand(
        //                "UPDATE ChronicDisease SET chronicDiseaseName= @chronicDiseaseName, description=@description WHERE chronicDiseaseID=@chronicDiseaseID",
        //            connection))
        //            {
        //                cmd.Parameters.AddWithValue("@chronicDiseaseID", ch.ChronicDiseaseID);
        //                cmd.Parameters.AddWithValue("@chronicDiseaseName", ch.ChronicDiseaseName);
        //                cmd.Parameters.AddWithValue("@description", ch.Description);

        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle exception (log, throw, etc.)
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //}

        //private static ChronicDisease MapChronicDisease(MySqlDataReader reader)
        //{
        //    return new ChronicDisease(
        //        reader.GetInt32("chronicDiseaseID"),
        //        reader.GetString("chronicDiseaseName"),
        //        reader.IsDBNull("description") ? null : reader.GetString("description")
        //    );
        //}




        /// chatRoom
        ////////////////////////////////////////////////////////////////
        ///
        public static void InsertChatRoom(ChatRoom chatRoom)
        {
            using (connection)
            {
                try
                {
                    if (GetChatRoomByID(chatRoom.ChatRoomID) != null) { return; }
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO ChatRoom (chatRoomID, firstUserID, secondUserID, chatRoomName,LastVisit) " +
                                   "VALUES (@chatRoomID, @firstUserID, @secondUserID, @chatRoomName,@lastvisit)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatRoomID", chatRoom.ChatRoomID);
                        command.Parameters.AddWithValue("@firstUserID", chatRoom.FirstUserID);
                        command.Parameters.AddWithValue("@secondUserID", chatRoom.SecondUserID);
                        command.Parameters.AddWithValue("@chatRoomName", chatRoom.ChatRoomName);
                        command.Parameters.AddWithValue("@lastvisit", chatRoom.LastVisit);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Insertion error, " + ex.Message);
                }
            }
        }

        public static void DeleteChatRoom(int chatRoomID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM ChatRoom WHERE chatRoomID = @chatRoomID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatRoomID", chatRoomID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("ChatRoom deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdateLastVisitChatRoom(int chatRoomID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE chatRoom SET LastVisit = @LastVisit WHERE chatRoomID = @chatRoomID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatRoomID", chatRoomID);
                        command.Parameters.AddWithValue("@LastVisit", DateTime.Now);

                        command.ExecuteNonQuery();

                        //MessageBox.Show("ChatRoom deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }


        public static ChatRoom GetChatRoomByID(int chatRoomID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatRoom WHERE chatRoomID = @chatRoomID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatRoomID", chatRoomID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new ChatRoom(
                                    Convert.ToInt32(reader["chatRoomID"]),
                                    Convert.ToInt32(reader["firstUserID"]),
                                    Convert.ToInt32(reader["secondUserID"]),
                                    reader["chatRoomName"].ToString(),
                                    Convert.ToDateTime(reader["LastVisit"])

                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<ChatRoom> GetAllChatRooms()
        {
            List<ChatRoom> chatRooms = new List<ChatRoom>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatRoom";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ChatRoom chatRoom = new ChatRoom(
                                    Convert.ToInt32(reader["chatRoomID"]),
                                    Convert.ToInt32(reader["firstUserID"]),
                                    Convert.ToInt32(reader["secondUserID"]),
                                    reader["chatRoomName"].ToString(),
                                    Convert.ToDateTime(reader["LastVisit"])

                                );

                                chatRooms.Add(chatRoom);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return chatRooms;
            }
        }

        public static List<ChatRoom> GetChatRoomsByUserID(int userID)
        {
            List<ChatRoom> chatRooms = new List<ChatRoom>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatRoom WHERE firstUserID = @userID OR secondUserID=@userID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ChatRoom chatRoom = new ChatRoom(
                                    Convert.ToInt32(reader["chatRoomID"]),
                                    Convert.ToInt32(reader["firstUserID"]),
                                    Convert.ToInt32(reader["secondUserID"]),
                                    reader["chatRoomName"].ToString(),
                                    Convert.ToDateTime(reader["LastVisit"])

                                );

                                chatRooms.Add(chatRoom);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return chatRooms;
            }
        }

        /// chatMessage
        ////////////////////////////////////////////////////////////////
        ///

        public static void InsertChatMessage(ChatMessage chatMessage)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO ChatMessage (messageID, senderID, chatRoomID, messageContent, timeStamp) " +
                                   "VALUES (@messageID, @senderID, @chatRoomID, @messageContent, @timeStamp)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@messageID", chatMessage.MessageID);
                        command.Parameters.AddWithValue("@senderID", chatMessage.SenderID);
                        command.Parameters.AddWithValue("@chatRoomID", chatMessage.ChatRoomID);
                        command.Parameters.AddWithValue("@messageContent", chatMessage.MessageContent);
                        command.Parameters.AddWithValue("@timeStamp", chatMessage.TimeStamp);

                        command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteChatMessage(int messageID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM ChatMessage WHERE messageID = @messageID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@messageID", messageID);

                        command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static ChatMessage GetChatMessageByID(int messageID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatMessage WHERE messageID = @messageID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@messageID", messageID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new ChatMessage(
                                    Convert.ToInt32(reader["messageID"]),
                                    Convert.ToInt32(reader["senderID"]),
                                    Convert.ToInt32(reader["chatRoomID"]),
                                    reader["messageContent"].ToString(),
                                    Convert.ToDateTime(reader["timeStamp"])
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        //public static List<ChatMessage> GetAllChatMessages()
        //{
        //    List<ChatMessage> chatMessages = new List<ChatMessage>();

        //    using (connection)
        //    {
        //        try
        //        {
        //            if (connection.State == ConnectionState.Closed) connection.Open();

        //            string query = "SELECT * FROM ChatMessage";

        //            using (MySqlCommand command = new MySqlCommand(query, connection))
        //            {
        //                using (MySqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        ChatMessage chatMessage = new ChatMessage(
        //                            Convert.ToInt32(reader["messageID"]),
        //                            Convert.ToInt32(reader["senderID"]),
        //                            Convert.ToInt32(reader["chatRoomID"]),
        //                            reader["messageContent"].ToString(),
        //                            Convert.ToDateTime(reader["timeStamp"])
        //                        );

        //                        chatMessages.Add(chatMessage);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Error: {ex.Message}");
        //        }

        //        return chatMessages;
        //    }
        //}

        public static List<ChatMessage> GetAllChatMessagesByRoomID(int chatRoomID)
        {
            List<ChatMessage> chatMessages = new List<ChatMessage>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatMessage WHERE chatRoomID = @chatRoomID ORDER BY timeStamp ASC;";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatRoomID", chatRoomID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ChatMessage chatMessage = new ChatMessage(
                                    Convert.ToInt32(reader["messageID"]),
                                    Convert.ToInt32(reader["senderID"]),
                                    Convert.ToInt32(reader["chatRoomID"]),
                                    reader["messageContent"].ToString(),
                                    Convert.ToDateTime(reader["timeStamp"])
                                );

                                chatMessages.Add(chatMessage);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return chatMessages;
            }
        }

        public static List<ChatMessage> GetAllUnreadMessagesByRoomID(int chatRoomID)
        {
            List<ChatMessage> chatMessages = new List<ChatMessage>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT cm.* FROM ChatMessage cm JOIN chatRoom cr ON cm.chatroomID = cr.chatroomID WHERE cm.timeStamp >= NOW() - INTERVAL 0.7 SECOND AND cr.chatroomID = @chatRoomID ORDER BY timeStamp ASC;";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatRoomID", chatRoomID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ChatMessage chatMessage = new ChatMessage(
                                    Convert.ToInt32(reader["messageID"]),
                                    Convert.ToInt32(reader["senderID"]),
                                    Convert.ToInt32(reader["chatRoomID"]),
                                    reader["messageContent"].ToString(),
                                    Convert.ToDateTime(reader["timeStamp"])
                                );

                                chatMessages.Add(chatMessage);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return chatMessages;
            }
        }

        public static ChatMessage GetLastSentChatMessageByChatRoomID(int chatRoomID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatMessage WHERE chatRoomID = @chatRoomID ORDER BY timeStamp DESC LIMIT 1";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatRoomID", chatRoomID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new ChatMessage(
                                Convert.ToInt32(reader["messageID"]),
                                Convert.ToInt32(reader["senderID"]),
                                Convert.ToInt32(reader["chatRoomID"]),
                                reader["messageContent"].ToString(),
                                Convert.ToDateTime(reader["timeStamp"])
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }


        /// chatGroup
        ////////////////////////////////////////////////////////////////////
        ///

        public static void InsertChatGroup(ChatGroup chatGroup)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO ChatGroup (chatGroupID, usersIDs, chatGroupName) " +
                                   "VALUES (@chatGroupID, @usersIDs, @chatGroupName)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatGroupID", chatGroup.ChatGroupID);
                        command.Parameters.AddWithValue("@usersIDs", string.Join(",", chatGroup.UsersIDs));
                        command.Parameters.AddWithValue("@chatGroupName", chatGroup.ChatGroupName);

                        command.ExecuteNonQuery();

                        MessageBox.Show("ChatGroup inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteChatGroup(int chatGroupID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM ChatGroup WHERE chatGroupID = @chatGroupID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatGroupID", chatGroupID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("ChatGroup deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static ChatGroup GetChatGroupByID(int chatGroupID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatGroup WHERE chatGroupID = @chatGroupID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatGroupID", chatGroupID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                List<int> usersIDs = reader["usersIDs"].ToString().Split(',').Select(int.Parse).ToList();

                                return new ChatGroup(
                                    Convert.ToInt32(reader["chatGroupID"]),
                                    usersIDs,
                                    reader["chatGroupName"].ToString()
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<ChatGroup> GetAllChatGroups()
        {
            List<ChatGroup> chatGroups = new List<ChatGroup>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatGroup";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List<int> usersIDs = reader["usersIDs"].ToString().Split(',').Select(int.Parse).ToList();

                                ChatGroup chatGroup = new ChatGroup(
                                    Convert.ToInt32(reader["chatGroupID"]),
                                    usersIDs,
                                    reader["chatGroupName"].ToString()
                                );

                                chatGroups.Add(chatGroup);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return chatGroups;
            }
        }

        public List<ChatGroup> GetChatGroupsByUserID(int userID)
        {
            List<ChatGroup> chatGroups = new List<ChatGroup>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT c.* FROM ChatGroup c " +
                                   "JOIN ChatGroupRelation cr ON c.chatGroupID = cr.chatGroupID " +
                                   "WHERE cr.userID = @userID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List<int> usersIDs = reader["usersIDs"].ToString().Split(',').Select(int.Parse).ToList();

                                ChatGroup chatGroup = new ChatGroup(
                                    Convert.ToInt32(reader["chatGroupID"]),
                                    usersIDs,
                                    reader["chatGroupName"].ToString()
                                );

                                chatGroups.Add(chatGroup);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return chatGroups;
            }
        }


        /// scanTest
        /////////////////////////////////////////////////////////////////////////
        ///

        public static void InsertScanTest(ScanTest scanTest)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = "INSERT INTO scanTest (scanTestID, scanTestName, recommendedLab, notes) " +
                                   "VALUES (@scanTestID, @scanTestName, @recommendedLab, @notes)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@scanTestID", scanTest.ScanTestID);
                        command.Parameters.AddWithValue("@scanTestName", scanTest.ScanTestName);
                        command.Parameters.AddWithValue("@recommendedLab", scanTest.RecommendedLab);
                        command.Parameters.AddWithValue("@notes", scanTest.Notes);

                        command.ExecuteNonQuery();

                        MessageBox.Show("ScanTest inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static List<ScanTest> GetAllScanTests()
        {
            List<ScanTest> scanTests = new List<ScanTest>();

            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM scanTest";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ScanTest scanTest = new ScanTest(
                                Convert.ToInt32(reader["scanTestID"]),
                                Convert.ToString(reader["scanTestName"]),
                                Convert.ToString(reader["recommendedLab"]),
                                Convert.ToString(reader["notes"])
                            );

                            scanTests.Add(scanTest);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return scanTests;
        }

        public static ScanTest GetScanTestById(int scanTestID)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM scanTest WHERE scanTestID = @scanTestID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@scanTestID", scanTestID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new ScanTest(
                                    Convert.ToInt32(reader["scanTestID"]),
                                    Convert.ToString(reader["scanTestName"]),
                                    Convert.ToString(reader["recommendedLab"]),
                                    Convert.ToString(reader["notes"])
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return null; // Return null if the scan test with the specified ID is not found
        }

        public static void UpdateScanTest(ScanTest scanTest)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = "UPDATE scanTest SET scanTestName = @scanTestName, " +
                                   "recommendedLab = @recommendedLab, notes = @notes " +
                                   "WHERE scanTestID = @scanTestID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@scanTestName", scanTest.ScanTestName);
                        command.Parameters.AddWithValue("@recommendedLab", scanTest.RecommendedLab);
                        command.Parameters.AddWithValue("@notes", scanTest.Notes);
                        command.Parameters.AddWithValue("@scanTestID", scanTest.ScanTestID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("ScanTest updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteScanTest(int scanTestID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = "DELETE FROM scanTest WHERE scanTestID = @scanTestID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@scanTestID", scanTestID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("ScanTest deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }



        /// attendanceRecord
        /////////////////////////////////////////////////////////////////////
        ///
        public static void InsertAttendanceRecord(AttendanceRecord attendanceRecord)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO AttendanceRecord (recordID, timeStamp, userID, present) " +
                                   "VALUES (@recordID, @timeStamp, @userID, @isPresent)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", attendanceRecord.RecordID);
                        command.Parameters.AddWithValue("@timeStamp", attendanceRecord.TimeStamp);
                        command.Parameters.AddWithValue("@userID", attendanceRecord.UserID);
                        command.Parameters.AddWithValue("@isPresent", attendanceRecord.IsPresent);

                        command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        public static void UpdateAttendanceRecord(AttendanceRecord attendanceRecord)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE AttendanceRecord SET timeStamp = @timeStamp, " +
                                   "userID = @userID, present = @isPresent WHERE recordID = @recordID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", attendanceRecord.RecordID);
                        command.Parameters.AddWithValue("@timeStamp", attendanceRecord.TimeStamp);
                        command.Parameters.AddWithValue("@userID", attendanceRecord.UserID);
                        command.Parameters.AddWithValue("@isPresent", attendanceRecord.IsPresent);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Attendance record updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteAttendanceRecord(int recordID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM AttendanceRecord WHERE recordID = @recordID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", recordID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Attendance record deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static AttendanceRecord GetAttendanceRecordByID(int recordID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM AttendanceRecord WHERE recordID = @recordID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", recordID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new AttendanceRecord(
                                    Convert.ToInt32(reader["recordID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToBoolean(reader["present"])
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<AttendanceRecord> GetAllAttendanceRecords()
        {
            List<AttendanceRecord> attendanceRecords = new List<AttendanceRecord>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM AttendanceRecord";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AttendanceRecord attendanceRecord = new AttendanceRecord(
                                    Convert.ToInt32(reader["recordID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToBoolean(reader["present"])
                                );

                                attendanceRecords.Add(attendanceRecord);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return attendanceRecords;
            }
        }

        public static List<AttendanceRecord> GetAttendanceRecordsByDate(DateTime date)
        {
            List<AttendanceRecord> attendanceRecords = new List<AttendanceRecord>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM AttendanceRecord WHERE DATE(timeStamp) = DATE(@date)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@date", date);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AttendanceRecord attendanceRecord = new AttendanceRecord(
                                    Convert.ToInt32(reader["recordID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToBoolean(reader["present"])
                                );

                                attendanceRecords.Add(attendanceRecord);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return attendanceRecords;
            }
        }
        public static List<AttendanceRecord> GetUserAttendanceRecords(int userID)
        {
            List<AttendanceRecord> attendanceRecords = new List<AttendanceRecord>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM AttendanceRecord WHERE userID = @userId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AttendanceRecord attendanceRecord = new AttendanceRecord(
                                    Convert.ToInt32(reader["recordID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToBoolean(reader["present"])
                                );

                                attendanceRecords.Add(attendanceRecord);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return attendanceRecords;
            }
        }



        ///visit
        /////////////////////////////////////////////////////////////////////
        ///
        public static void InsertVisit(Visit visit)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO Visit (visitID, userID, patientID, packageID, timeStamp, roomID, therapistNotes, height, weight, isDone, visitTypeID) " +
                                   "VALUES (@visitID, @userID, @patientID, @packageID, @timeStamp, @roomID, @therapistNotes, @height, @weight, @done, @visitTypeID)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@visitID", visit.VisitID);
                        command.Parameters.AddWithValue("@userID", visit.DoctorID);
                        command.Parameters.AddWithValue("@patientID", visit.PatientID);
                        command.Parameters.AddWithValue("@timeStamp", visit.TimeStamp);
                        command.Parameters.AddWithValue("@therapistNotes", visit.TherapistNotes);
                        command.Parameters.AddWithValue("@height", visit.Height);
                        command.Parameters.AddWithValue("@weight", visit.Weight);
                        command.Parameters.AddWithValue("@done", visit.IsDone);
                        command.Parameters.AddWithValue("@visitTypeID", visit.AppointmentTypeID);
                        command.ExecuteNonQuery();

                        MessageBox.Show($"Visit Booked on {visit.TimeStamp.ToString("g")}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdateVisit(Visit visit)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE Visit SET userID = @userID, patientID = @patientID, " +
                                   "packageID = @packageID, timeStamp = @timeStamp, roomID = @roomID, therapistNotes = @therapistNotes, height = @height , weight = @weight, isDone=@done, visitTypeID=@visitTypeID WHERE visitID = @visitID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@visitID", visit.VisitID);
                        command.Parameters.AddWithValue("@userID", visit.DoctorID);
                        command.Parameters.AddWithValue("@patientID", visit.PatientID);
                        command.Parameters.AddWithValue("@timeStamp", visit.TimeStamp);
                        command.Parameters.AddWithValue("@therapistNotes", visit.TherapistNotes);
                        command.Parameters.AddWithValue("@height", visit.Height);
                        command.Parameters.AddWithValue("@weight", visit.Weight);
                        command.Parameters.AddWithValue("@done", visit.IsDone);
                        command.Parameters.AddWithValue("@visitTypeID", visit.AppointmentTypeID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteVisit(int visitID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM Visit WHERE visitID = @visitID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@visitID", visitID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Visit record deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static Visit GetVisitByID(int visitID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE visitID = @visitID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@visitID", visitID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapVisit(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }
        public static Visit GetMostRecentVisitByPatientID(int patientId)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE patientID = @patientID AND isDone = 1 ORDER BY timeStamp DESC LIMIT 1";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patientId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapVisit(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<Visit> GetAllVisits()
        {
            List<Visit> visitList = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {


                                visitList.Add(MapVisit(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return visitList;
            }
        }
        public static List<Visit> GetAllFutureVisits()
        {
            List<Visit> visitList = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE timeStamp >= CURDATE() ORDER BY timeStamp ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                visitList.Add(MapVisit(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return visitList;
            }
        }
        public static List<Visit> GetPatientPrevVisits(int patientID)
        {
            List<Visit> visitList = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE patientID=@patientID AND (timeStamp < CURDATE() OR isDone=1) ORDER BY timeStamp ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {


                                visitList.Add(MapVisit(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return visitList;
            }
        }
        public static List<Visit> GetPatientUpcomingVisits(int patientID)
        {
            List<Visit> visitList = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE patientID=@patientID AND timeStamp > CURDATE() AND isDone=0 ORDER BY timeStamp ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {


                                visitList.Add(MapVisit(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return visitList;
            }
        }
        public static List<Visit> GetPatientTodayVisits(int patientID)
        {
            List<Visit> visitList = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE patientID=@patientID AND DATE(timeStamp) = CURDATE() AND isDone=0 ORDER BY timeStamp ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                visitList.Add(MapVisit(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return visitList;
            }
        }
        public static List<Visit> GetAllVisitsByDoctorID(int DoctorID)
        {
            List<Visit> visitsForDoctor = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT v.* FROM Visit v " +
                                   "INNER JOIN User u ON v.userID = u.userID " +
                                   "WHERE u.userID = @DoctorID ORDER BY timeStamp ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {


                                visitsForDoctor.Add(MapVisit(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return visitsForDoctor;
        }
        public static List<Visit> GetTodayVisits()
        {
            List<Visit> todayVisits = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE DATE(timeStamp) = CURDATE() ORDER BY timeStamp ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {


                                todayVisits.Add(MapVisit(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return todayVisits;
        }
        public static List<Visit> GetFutureDoctorVisits(int DoctorID)
        {
            List<Visit> todayDoctorVisits = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT v.* FROM Visit v " +
                                   "INNER JOIN User u ON v.userID = u.userID " +
                                   "WHERE u.userID = @DoctorID AND DATE(v.timeStamp) >= CURDATE() AND isDone=0 ORDER BY timeStamp ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {


                                todayDoctorVisits.Add(MapVisit(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return todayDoctorVisits;
        }
        public static List<Visit> GetDoctorVisitsOnDate(int DoctorID, DateTime date)
        {
            List<Visit> DoctorVisitsOnDate = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE userID = @DoctorID AND DATE(timeStamp) = @visitDate ORDER BY timeStamp ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);
                        command.Parameters.AddWithValue("@visitDate", date.ToString("yyyy-MM-dd"));

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                DoctorVisitsOnDate.Add(MapVisit(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return DoctorVisitsOnDate;
        }
        public static List<Visit> GetAllVisitsOnDate(DateTime date)
        {
            List<Visit> DoctorVisitsOnDate = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE DATE(timeStamp) = @visitDate ORDER BY timeStamp ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@visitDate", date.ToString("yyyy-MM-dd"));

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DoctorVisitsOnDate.Add(MapVisit(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return DoctorVisitsOnDate;
        }
        private static Visit MapVisit(MySqlDataReader reader)
        {
            return new Visit(
                Convert.ToInt32(reader["visitID"]),
                Convert.ToInt32(reader["userID"]),
                Convert.ToInt32(reader["patientID"]),
                Convert.ToDateTime(reader["timeStamp"]),
                reader["therapistNotes"].ToString(),
                Convert.ToDouble(reader["height"]),
                Convert.ToDouble(reader["weight"]),
                Convert.ToBoolean(reader["isDone"]),
                Convert.ToInt32(reader["visitTypeID"])
            );
        }



        /// evaluationTest
        ///////////////////////////////////////////////////////////////////
        ///
        public static List<EvaluationTest> GetAllTests()
        {
            List<EvaluationTest> tests = new List<EvaluationTest>();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM evaluationTest";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EvaluationTest test = new EvaluationTest(
                                Convert.ToInt32(reader["testID"]),
                                Convert.ToString(reader["testName"]),
                                Convert.ToString(reader["testDescription"])

                                );

                            tests.Add(test);
                        }
                    }
                }
            }

            return tests;
        }

        public static EvaluationTest GetTestById(int testId)
        {
            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM evaluationTest WHERE testID = @TestID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TestID", testId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new EvaluationTest(
                                Convert.ToInt32(reader["testID"]),
                                Convert.ToString(reader["testName"]),
                                Convert.ToString(reader["testDescription"])
                            );
                        }
                    }
                }
            }

            return null; // Test not found
        }

        public static void InsertTest(EvaluationTest test)
        {
            using (connection)
            {
                connection.Open();

                string query = "INSERT INTO evaluationTest (testID, testName, testDescription) " +
                               "VALUES (@TestID, @TestName, @TestDescription)";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TestID", test.TestID);
                    cmd.Parameters.AddWithValue("@TestName", test.TestName);
                    cmd.Parameters.AddWithValue("@TestDescription", test.TestDescription);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateTest(EvaluationTest test)
        {
            using (connection)
            {
                connection.Open();

                string query = "UPDATE evaluationTest SET testName=@TestName, testDescription=@TestDescription WHERE testID=@TestID ";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TestID", test.TestID);
                    cmd.Parameters.AddWithValue("@TestName", test.TestName);
                    cmd.Parameters.AddWithValue("@TestDescription", test.TestDescription);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static void DeleteTest(int testId)
        {
            using (connection)
            {
                connection.Open();

                string query = "DELETE FROM evaluationTest WHERE testID = @TestID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TestID", testId);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        ///Test feedback
        /////////////////////////////////////////////////////////////////
        ///
        public static void InsertFeedback(EvaluationTestFeedBack feedback)
        {
            using (connection)
            {
                connection.Open();

                string query = "INSERT INTO testFeedBack (testFeedBackID, severity, visitID, patientID, testID, notes, timeStamp) " +
                               "VALUES (@TestFeedbackID, @Severity, @VisitID, @PatientID, @TestID, @Notes, @TimeStamp)";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TestFeedbackID", feedback.TestFeedBackID);
                    cmd.Parameters.AddWithValue("@Severity", feedback.Severity);
                    cmd.Parameters.AddWithValue("@VisitID", feedback.VisitID);
                    cmd.Parameters.AddWithValue("@PatientID", feedback.PatientID);
                    cmd.Parameters.AddWithValue("@TestID", feedback.TestID);
                    cmd.Parameters.AddWithValue("@Notes", feedback.Notes);
                    cmd.Parameters.AddWithValue("@TimeStamp", feedback.TimeStamp);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<EvaluationTestFeedBack> GetAllFeedback()
        {
            List<EvaluationTestFeedBack> visitFeedbackList = new List<EvaluationTestFeedBack>();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM testFeedBack";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EvaluationTestFeedBack feedback = new EvaluationTestFeedBack(
                                Convert.ToInt32(reader["testFeedBackID"]),
                                Convert.ToInt32(reader["severity"]),
                                Convert.ToInt32(reader["visitID"]),
                                Convert.ToInt32(reader["patientID"]),
                                Convert.ToInt32(reader["testID"]),
                                reader["notes"].ToString(),
                                Convert.ToDateTime(reader["timeStamp"])


                            );

                            visitFeedbackList.Add(feedback);
                        }
                    }
                }
            }

            return visitFeedbackList;
        }

        public static List<EvaluationTestFeedBack> GetFeedbackByPatient(int patientId)
        {
            List<EvaluationTestFeedBack> visitFeedbackList = new List<EvaluationTestFeedBack>();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM testFeedBack WHERE patientID = @VisitID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@VisitID", patientId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EvaluationTestFeedBack feedback = new EvaluationTestFeedBack(
                                Convert.ToInt32(reader["testFeedBackID"]),
                                Convert.ToInt32(reader["severity"]),
                                Convert.ToInt32(reader["visitID"]),
                                Convert.ToInt32(reader["patientID"]),
                                Convert.ToInt32(reader["testID"]),
                                reader["notes"].ToString(),
                                Convert.ToDateTime(reader["timeStamp"])
                            );

                            visitFeedbackList.Add(feedback);
                        }
                    }
                }
            }

            return visitFeedbackList;
        }

        public static List<EvaluationTestFeedBack> GetFeedbackByVisit(int visitId)
        {
            List<EvaluationTestFeedBack> visitFeedbackList = new List<EvaluationTestFeedBack>();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM testFeedBack WHERE visitID = @VisitID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@VisitID", visitId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EvaluationTestFeedBack feedback = new EvaluationTestFeedBack(
                                Convert.ToInt32(reader["testFeedBackID"]),
                                Convert.ToInt32(reader["severity"]),
                                Convert.ToInt32(reader["visitID"]),
                                Convert.ToInt32(reader["patientID"]),
                                Convert.ToInt32(reader["testID"]),
                                reader["notes"].ToString(),
                                Convert.ToDateTime(reader["timeStamp"])
                            );

                            visitFeedbackList.Add(feedback);
                        }
                    }
                }
            }

            return visitFeedbackList;
        }

        public static void DeleteAllVisitFeedback(int visitID)
        {
            using (connection)
            {
                connection.Open();

                string query = "DELETE FROM testFeedBack WHERE visitID = @TestFeedbackID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TestFeedbackID", visitID);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteFeedback(int feedbackId)
        {
            using (connection)
            {
                connection.Open();

                string query = "DELETE FROM testFeedBack WHERE testFeedBackID = @TestFeedbackID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TestFeedbackID", feedbackId);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        ///prescription
        ////////////////////////////////////////////////////////////////
        ///
        public static void InsertPrescription(Prescription prescription)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO Prescription (prescriptionID, timeStamp, patientID, userID, visitID) " +
                                   "VALUES (@prescriptionID, @timeStamp, @patientID, @userID, @visitID)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@prescriptionID", prescription.PrescriptionID);
                        command.Parameters.AddWithValue("@timeStamp", prescription.TimeStamp);
                        command.Parameters.AddWithValue("@patientID", prescription.PatientID);
                        command.Parameters.AddWithValue("@userID", prescription.UserID);
                        command.Parameters.AddWithValue("@visitID", prescription.VisitID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Prescription record inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        public static void UpdatePrescription(Prescription prescription)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE Prescription SET timeStamp = @timeStamp, " +
                                   "patientID = @patientID, userID = @userID, visitID = @visitID " +
                                   "WHERE prescriptionID = @prescriptionID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@prescriptionID", prescription.PrescriptionID);
                        command.Parameters.AddWithValue("@timeStamp", prescription.TimeStamp);
                        command.Parameters.AddWithValue("@patientID", prescription.PatientID);
                        command.Parameters.AddWithValue("@userID", prescription.UserID);
                        command.Parameters.AddWithValue("@visitID", prescription.VisitID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Prescription record updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        public static void DeletePrescription(int prescriptionID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM Prescription WHERE prescriptionID = @prescriptionID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@prescriptionID", prescriptionID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Prescription record deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        public static Prescription GetPrescriptionByID(int prescriptionID)
        {
            using (connection)
            {
                Prescription toReturn = new Prescription();

                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Prescription WHERE prescriptionID = @prescriptionID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@prescriptionID", prescriptionID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                toReturn = new Prescription(
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["visitID"])

                                );
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }
        public static List<Prescription> GetAllPrescriptions()
        {
            List<Prescription> prescriptionList = new List<Prescription>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Prescription";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Prescription prescription = new Prescription(
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["visitID"])
                                );

                                prescriptionList.Add(prescription);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return prescriptionList;
            }
        }
        public static List<Prescription> GetAllPrescriptionsByPatientID(int patientID)
        {
            List<Prescription> prescriptionList = new List<Prescription>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Prescription WHERE patientID=@patientID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Prescription prescription = new Prescription(
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["visitID"])
                                );

                                prescriptionList.Add(prescription);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show($"Error: {ex.Message}");
                }

                return prescriptionList;
            }
        }
        public static List<Prescription> GetAllPrescriptionsByVisitID(int visitID)
        {
            List<Prescription> prescriptionList = new List<Prescription>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Prescription WHERE visitID=@visitID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@visitID", visitID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Prescription prescription = new Prescription(
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["visitID"])
                                );

                                prescriptionList.Add(prescription);
                            }
                        }
                    }
                }

                catch (Exception ex)
                {

                    MessageBox.Show($"Error: {ex.Message}");
                }

                return prescriptionList;
            }
        }



        ///medicalRecord
        ////////////////////////////////////////////////////////////////////////
        ///
        public static void InsertMedicalRecord(MedicalRecord medicalRecord)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO MedicalRecord (recordID, type, timeStamp, report, images, patientID, DoctorNotes) " +
                                   "VALUES (@recordID, @type, @timeStamp, @report, @images, @patientID, @DoctorNotes)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", medicalRecord.RecordID);
                        command.Parameters.AddWithValue("@type", medicalRecord.Type);
                        command.Parameters.AddWithValue("@timeStamp", medicalRecord.TimeStamp);
                        command.Parameters.AddWithValue("@report", medicalRecord.Report);
                        command.Parameters.AddWithValue("@images", string.Join(",", medicalRecord.Images));
                        command.Parameters.AddWithValue("@patientID", medicalRecord.PatientID);
                        command.Parameters.AddWithValue("@DoctorNotes", medicalRecord.DoctorNotes);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Medical record inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        public static void UpdateMedicalRecord(MedicalRecord medicalRecord)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE MedicalRecord SET type = @type, " +
                                   "timeStamp = @timeStamp, report = @report, images = @images, " +
                                   "patientID = @patientID, DoctorNotes = @DoctorNotes " +
                                   "WHERE recordID = @recordID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", medicalRecord.RecordID);
                        command.Parameters.AddWithValue("@type", medicalRecord.Type);
                        command.Parameters.AddWithValue("@timeStamp", medicalRecord.TimeStamp);
                        command.Parameters.AddWithValue("@report", medicalRecord.Report);
                        command.Parameters.AddWithValue("@images", string.Join(",", medicalRecord.Images));
                        command.Parameters.AddWithValue("@patientID", medicalRecord.PatientID);
                        command.Parameters.AddWithValue("@DoctorNotes", medicalRecord.DoctorNotes);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Medical record updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        public static void DeleteMedicalRecord(int recordID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM MedicalRecord WHERE recordID = @recordID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", recordID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Medical record deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        public static MedicalRecord GetMedicalRecordByID(int recordID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM MedicalRecord WHERE recordID = @recordID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", recordID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new MedicalRecord(
                                    Convert.ToInt32(reader["recordID"]),
                                    reader["type"].ToString(),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    reader["report"].ToString(),
                                    new List<string>(reader["images"].ToString().Split(',')),
                                    Convert.ToInt32(reader["patientID"]),
                                    reader["DoctorNotes"].ToString()
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }
        public static List<MedicalRecord> GetAllMedicalRecords()
        {
            List<MedicalRecord> medicalRecordList = new List<MedicalRecord>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM MedicalRecord";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MedicalRecord medicalRecord = new MedicalRecord(
                                    Convert.ToInt32(reader["recordID"]),
                                    reader["type"].ToString(),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    reader["report"].ToString(),
                                    new List<string>(reader["images"].ToString().Split(',')),
                                    Convert.ToInt32(reader["patientID"]),
                                    reader["DoctorNotes"].ToString()
                                );

                                medicalRecordList.Add(medicalRecord);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return medicalRecordList;
            }
        }
        public static List<MedicalRecord> GetAllPatientRecords(int patientID)
        {
            List<MedicalRecord> patientMedicalRecords = new List<MedicalRecord>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM MedicalRecord WHERE patientID = @patientID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MedicalRecord medicalRecord = new MedicalRecord(
                                    Convert.ToInt32(reader["recordID"]),
                                    reader["type"].ToString(),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    reader["report"].ToString(),
                                    new List<string>(reader["images"].ToString().Split(',')),
                                    Convert.ToInt32(reader["patientID"]),
                                    reader["DoctorNotes"].ToString()
                                );

                                patientMedicalRecords.Add(medicalRecord);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return patientMedicalRecords;
            }
        }


        ///issueScan
        ///////////////////////////////////////////////////////////////////////////
        ///

        public static void InsertIssueScan(IssueScan issueScan)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = "INSERT INTO IssueScan (issueID, scanTestID, prescriptionID, patientID, notes) " +
                                   "VALUES (@issueID, @scanTestID, @prescriptionID, @patientID, @notes)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@issueID", issueScan.IssueID);
                        command.Parameters.AddWithValue("@scanTestID", issueScan.ScanTestID);
                        command.Parameters.AddWithValue("@prescriptionID", issueScan.PrescriptionID);
                        command.Parameters.AddWithValue("@patientID", issueScan.PatientID);
                        command.Parameters.AddWithValue("@notes", issueScan.Notes);

                        command.ExecuteNonQuery();

                        //MessageBox.Show("IssueScan inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        public static List<IssueScan> GetAllIssueScans()
        {
            List<IssueScan> issueScans = new List<IssueScan>();

            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM IssueScan";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IssueScan issueScan = new IssueScan(
                                Convert.ToInt32(reader["issueID"]),
                                Convert.ToInt32(reader["scanTestID"]),
                                Convert.ToInt32(reader["prescriptionID"]),
                                Convert.ToInt32(reader["patientID"]),
                                Convert.ToString(reader["notes"])
                            );

                            issueScans.Add(issueScan);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return issueScans;
        }
        public static List<IssueScan> GetAllIssueScansByPatientID(int patientID)
        {
            List<IssueScan> issueScans = new List<IssueScan>();

            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM IssueScan WHERE patientID=@pId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@pId", patientID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IssueScan issueScan = new IssueScan(
                                    Convert.ToInt32(reader["issueID"]),
                                    Convert.ToInt32(reader["scanTestID"]),
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToString(reader["notes"])
                                );

                                issueScans.Add(issueScan);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return issueScans;
        }
        public static List<IssueScan> GetAllIssueScansByPrescriptionID(int prescriptionID)
        {
            List<IssueScan> issueScans = new List<IssueScan>();

            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM IssueScan WHERE prescriptionID=@pId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@pId", prescriptionID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IssueScan issueScan = new IssueScan(
                                    Convert.ToInt32(reader["issueID"]),
                                    Convert.ToInt32(reader["scanTestID"]),
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToString(reader["notes"])
                                );

                                issueScans.Add(issueScan);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return issueScans;
        }
        public static IssueScan GetIssueScanById(int issueID)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM IssueScan WHERE issueID = @issueID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@issueID", issueID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new IssueScan(
                                    Convert.ToInt32(reader["issueID"]),
                                    Convert.ToInt32(reader["scanTestID"]),
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToString(reader["notes"])
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return null; // Return null if the issue scan with the specified ID is not found
        }
        public static void UpdateIssueScan(IssueScan issueScan)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = "UPDATE IssueScan SET scanTestID = @scanTestID, " +
                                   "prescriptionID = @prescriptionID, patientID = @patientID, " +
                                   "notes = @notes WHERE issueID = @issueID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@scanTestID", issueScan.ScanTestID);
                        command.Parameters.AddWithValue("@prescriptionID", issueScan.PrescriptionID);
                        command.Parameters.AddWithValue("@patientID", issueScan.PatientID);
                        command.Parameters.AddWithValue("@notes", issueScan.Notes);
                        command.Parameters.AddWithValue("@issueID", issueScan.IssueID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("IssueScan updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        public static void DeleteIssueScan(int issueID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = "DELETE FROM IssueScan WHERE issueID = @issueID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@issueID", issueID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("IssueScan deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }


        ///payment
        //////////////////////////////////////////////////////////////////
        ///
        public static List<Payment> GetAllPayments()
        {
            List<Payment> payments = new List<Payment>();

            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM payment";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Payment payment = new Payment(
                                Convert.ToInt32(reader["paymentID"]),
                                Convert.ToDouble(reader["amount"]),
                                Convert.ToDateTime(reader["timestamp"]),
                                Convert.ToInt32(reader["DoctorID"]),
                                Convert.ToInt32(reader["patientID"])
                            );

                            payments.Add(payment);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return payments;
        }

        public static List<Payment> GetPatientPayments(int patientID)
        {
            List<Payment> patientPayments = new List<Payment>();

            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM payment WHERE patientID = @patientID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Payment payment = new Payment(
                                    Convert.ToInt32(reader["paymentID"]),
                                    Convert.ToDouble(reader["amount"]),
                                    Convert.ToDateTime(reader["timestamp"]),
                                    Convert.ToInt32(reader["DoctorID"]),
                                    Convert.ToInt32(reader["patientID"])
                                );

                                patientPayments.Add(payment);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return patientPayments;
        }

        public static List<Payment> GetDoctorPayments(int DoctorID)
        {
            List<Payment> DoctorPayments = new List<Payment>();

            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM payment WHERE DoctorID = @DoctorID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Payment payment = new Payment(
                                    Convert.ToInt32(reader["paymentID"]),
                                    Convert.ToDouble(reader["amount"]),
                                    Convert.ToDateTime(reader["timestamp"]),
                                    Convert.ToInt32(reader["DoctorID"]),
                                    Convert.ToInt32(reader["patientID"])
                                );

                                DoctorPayments.Add(payment);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return DoctorPayments;
        }

        public static void InsertPayment(Payment payment)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO payment (paymentID, amount, timestamp, DoctorID, patientID) " +
                                   "VALUES (@paymentID, @amount, @timestamp, @DoctorID, @patientID)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@paymentID", payment.PaymentID);
                        command.Parameters.AddWithValue("@amount", payment.Amount);
                        command.Parameters.AddWithValue("@timestamp", payment.TimeStamp);
                        command.Parameters.AddWithValue("@DoctorID", payment.DoctorID);
                        command.Parameters.AddWithValue("@patientID", payment.PatientID);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdatePayment(Payment payment)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE payment SET amount = @amount, timestamp = @timestamp, " +
                                   "DoctorID = @DoctorID, patientID = @patientID " +
                                   "WHERE paymentID = @paymentID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@amount", payment.Amount);
                        command.Parameters.AddWithValue("@timestamp", payment.TimeStamp);
                        command.Parameters.AddWithValue("@DoctorID", payment.DoctorID);
                        command.Parameters.AddWithValue("@patientID", payment.PatientID);
                        command.Parameters.AddWithValue("@paymentID", payment.PaymentID);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeletePayment(int paymentID)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM payment WHERE paymentID = @paymentID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@paymentID", paymentID);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }


        ///globalVars
        /////////////////////////////////////////////////////////////
        ///

        public static string GetSessionTime(int id)
        {
            string time = "";

            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM globalVars WHERE varID = @patientID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                time = (reader["varValue"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return time;
        }
        public static int GetDefaultAppointmentTimeInMinutes()
        {
            return (int)GetIntValueByID(9);
        }
        public static double GetSlotDuration()
        {
            return GetIntValueByID(10);
        }
        public static int GetOpeningTime()
        {
            return (int)GetIntValueByID(11);
        }
        public static int GetClosingTime()
        {
            return (int)GetIntValueByID(12);
        }
        public static double GetIntValueByID(int id)
        {
            string time = "";

            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM globalVars WHERE varID = @varID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@varID", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                time = (reader["varValue"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return Double.Parse(time);
        }
        public static void UpdateDefaultAppointmentTimeInMinutes(string val)
        {
            UpdateGlobalVar(9, val);
        }
        public static void UpdateConsultationCost(string val)
        {
            UpdateGlobalVar(10, val);
        }
        public static void UpdateFollowUpCost(string val)
        {
            UpdateGlobalVar(11, val);
        }
        public static void UpdateExerciseCost(string val)
        {
            UpdateGlobalVar(12, val);
        }
        public static void UpdateGlobalVar(int id, string val)
        {
            using (connection)
            {
                connection.Open();

                string query = "UPDATE globalVars SET varValue=@varValue, WHERE varID=@varID ";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@varID", id);
                    cmd.Parameters.AddWithValue("@varValue", val);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        ///calendarEvent
        ////////////////////////////////////////////////////////
        ///

        public static void InsertCalendarEvent(CalendarEvent calendarEvent)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO calendarEvent (eventID, userID, eventName, eventText, startTime, endTime, isDone) " +
                                   "VALUES (@eventID, @userID, @eventName, @eventText, @startTime, @endTime, @done)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@eventID", calendarEvent.EventID);
                        command.Parameters.AddWithValue("@userID", calendarEvent.UserID);
                        command.Parameters.AddWithValue("@eventName", calendarEvent.EventName);
                        command.Parameters.AddWithValue("@eventText", calendarEvent.EventText);
                        command.Parameters.AddWithValue("@startTime", calendarEvent.EventStartTime);
                        command.Parameters.AddWithValue("@endTime", calendarEvent.EventEndTime);
                        command.Parameters.AddWithValue("@done", calendarEvent.IsDone);

                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Event added successfully.");

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdateCalendarEvent(CalendarEvent calendarEvent)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE calendarEvent SET " +
                                   "userID = @userID, " +
                                   "eventName = @eventName, " +
                                   "eventText = @eventText, " +
                                   "startTime = @startTime, " +
                                   "endTime = @endTime, " +
                                   "isDone = @done " +
                                   "WHERE eventID = @eventID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@eventID", calendarEvent.EventID);
                        command.Parameters.AddWithValue("@userID", calendarEvent.UserID);
                        command.Parameters.AddWithValue("@eventName", calendarEvent.EventName);
                        command.Parameters.AddWithValue("@eventText", calendarEvent.EventText);
                        command.Parameters.AddWithValue("@startTime", calendarEvent.EventStartTime);
                        command.Parameters.AddWithValue("@endTime", calendarEvent.EventEndTime);
                        command.Parameters.AddWithValue("@done", calendarEvent.IsDone);

                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Event updated successfully.");

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static List<CalendarEvent> GetAllCalendarEvents()
        {
            List<CalendarEvent> calendarEvents = new List<CalendarEvent>();
            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM calendarEvent";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CalendarEvent calendarEvent = MapCalendarEvent(reader);
                                calendarEvents.Add(calendarEvent);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return calendarEvents;
        }
        public static void DeleteCalendarEvent(CalendarEvent calendarEvent)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "Delete FROM calendarEvent Where eventID=@eId";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@eId", calendarEvent.EventID);
                        cmd.ExecuteReader();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

        }

        private static CalendarEvent MapCalendarEvent(MySqlDataReader reader)
        {
            return new CalendarEvent(Convert.ToInt32(reader["eventID"]),
                Convert.ToInt32(reader["userID"]),
                Convert.ToString(reader["eventName"]),
                Convert.ToString(reader["eventText"]),
                Convert.ToDateTime(reader["startTime"]),
                Convert.ToDateTime(reader["endTime"]),
                Convert.ToBoolean(reader["isDone"])

            );
        }
        public static List<CalendarEvent> GetCalendarEventsByUserID(int userID)
        {
            List<CalendarEvent> calendarEvents = new List<CalendarEvent>();
            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM calendarEvent WHERE userID = @userID";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CalendarEvent calendarEvent = MapCalendarEvent(reader);
                                calendarEvents.Add(calendarEvent);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return calendarEvents;
        }

        public static List<CalendarEvent> GetCalendarEventsByUserIDAndDate(int userID, DateTime startDateTime)
        {
            List<CalendarEvent> calendarEvents = new List<CalendarEvent>();
            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM calendarEvent WHERE userID = @userID AND DATE(startTime) = DATE(@startDateTime) ORDER BY startTime ASC";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);
                        cmd.Parameters.AddWithValue("@startDateTime", startDateTime);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CalendarEvent calendarEvent = MapCalendarEvent(reader);
                                calendarEvents.Add(calendarEvent);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return calendarEvents;
        }
        public static List<CalendarEvent> GetCalendarEventsByDate(DateTime startDateTime)
        {
            List<CalendarEvent> calendarEvents = new List<CalendarEvent>();
            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM calendarEvent WHERE DATE(startTime) = DATE(@startDateTime) ORDER BY startTime ASC";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@startDateTime", startDateTime);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CalendarEvent calendarEvent = MapCalendarEvent(reader);
                                calendarEvents.Add(calendarEvent);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return calendarEvents;
        }

        ///appointment type
        /////////////////////////////////////////////////////////
        ///
        public static void InsertAppointmentType(AppointmentType appointmentType)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = "INSERT INTO appointmentType (typeID, typeName, typeDescription, timeInMinutes, cost) " +
                                   "VALUES (@typeID, @typeName, @typeDescription, @timeInMinutes, @cost)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@typeID", appointmentType.TypeID);
                        command.Parameters.AddWithValue("@typeName", appointmentType.Name);
                        command.Parameters.AddWithValue("@typeDescription", appointmentType.Description);
                        command.Parameters.AddWithValue("@timeInMinutes", appointmentType.TimeInMinutes);
                        command.Parameters.AddWithValue("@cost", appointmentType.Cost);

                        command.ExecuteNonQuery();

                        MessageBox.Show("AppointmentType inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        public static List<AppointmentType> GetAllAppointmentTypes()
        {
            List<AppointmentType> appointmentTypes = new List<AppointmentType>();

            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM appointmentType";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AppointmentType appointmentType = new AppointmentType(
                                Convert.ToInt32(reader["typeID"]),
                                Convert.ToString(reader["typeName"]),
                                Convert.ToString(reader["typeDescription"]),
                                Convert.ToInt32(reader["timeInMinutes"]),
                                Convert.ToDouble(reader["cost"])
                            );

                            appointmentTypes.Add(appointmentType);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return appointmentTypes;
        }
        public static AppointmentType GetAppointmentTypeByID(int id)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM appointmentType Where typeID=@id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return new AppointmentType(
                                    Convert.ToInt32(reader["typeID"]),
                                    Convert.ToString(reader["typeName"]),
                                    Convert.ToString(reader["typeDescription"]),
                                    Convert.ToInt32(reader["timeInMinutes"]),
                                    Convert.ToDouble(reader["cost"])
                                );

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            return null;
        }
        public static void UpdateAppointmentType(AppointmentType appointmentType)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = "UPDATE appointmentType SET typeName = @typeName, " +
                                   "typeDescription = @typeDescription, timeInMinutes = @timeInMinutes, cost = @cost " +
                                   "WHERE typeID = @typeID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@typeName", appointmentType.Name);
                        command.Parameters.AddWithValue("@typeDescription", appointmentType.Description);
                        command.Parameters.AddWithValue("@timeInMinutes", appointmentType.TimeInMinutes);
                        command.Parameters.AddWithValue("@cost", appointmentType.Cost);
                        command.Parameters.AddWithValue("@typeID", appointmentType.TypeID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("AppointmentType updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        public static void DeleteAppointmentType(int typeID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = "DELETE FROM appointmentType WHERE typeID = @typeID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@typeID", typeID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("AppointmentType deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        ///OntologyTerm
        /////////////////////////////////////////////////////////////
        ///
        public static void InsertTerm(OntologyTerm term)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Ontology (ID, Name, Def, Urls, Synonyms, Parent) VALUES (@ID, @Name, @Def, @Urls, @Synonyms, @Parent)", connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", term.Id);
                        cmd.Parameters.AddWithValue("@Name", term.Name);
                        cmd.Parameters.AddWithValue("@Def", term.Def);
                        cmd.Parameters.AddWithValue("@Urls", ConvertListToString(term.Urls));
                        cmd.Parameters.AddWithValue("@Synonyms", ConvertListToString(term.Synonyms));
                        cmd.Parameters.AddWithValue("@Parent", term.Parent);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    // Handle exception as needed
                }
            }
        }

        public static OntologyTerm GetTermByName(string name)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Ontology WHERE Name = @ID", connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", "name");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReaderToTerm(reader);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    // Handle exception as needed
                }

                return null;
            }
        }

        public static List<OntologyTerm> GetTermsLikeName(string name)
        {
            List<OntologyTerm> terms=new List<OntologyTerm>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Ontology WHERE Name LIKE @ID", connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", $"%{name}%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                terms.Add(MapReaderToTerm(reader));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
            return terms;

        }
        public static List<OntologyTerm> GetTermsLikeID(string query)
        {
            List<OntologyTerm> terms = new List<OntologyTerm>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Ontology WHERE ID LIKE @ID OR ID = @cID LIMIT 0,200", connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", $"%{query}%");
                        cmd.Parameters.AddWithValue("@cID", query);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                terms.Add(MapReaderToTerm(reader));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
            return terms;
        }
        public static List<OntologyTerm> GetTermsLikeDef(string query)
        {
            List<OntologyTerm> terms = new List<OntologyTerm>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Ontology WHERE Def LIKE @ID LIMIT 0,200", connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", $"%{query}%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                terms.Add(MapReaderToTerm(reader));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
            return terms;
        }
        public static List<OntologyTerm> GetTermsLikeParent(string query)
        {
            List<OntologyTerm> terms = new List<OntologyTerm>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Ontology WHERE Parent LIKE @ID LIMIT 0,200", connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", $"%{query}%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                terms.Add(MapReaderToTerm(reader));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
            return terms;
        }
        public static List<OntologyTerm> GetTermsLikeSynonyms(string query)
        {
            List<OntologyTerm> terms = new List<OntologyTerm>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Ontology WHERE Synonyms LIKE @ID LIMIT 0,200", connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", $"%{query}%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                terms.Add(MapReaderToTerm(reader));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
            return terms;
        }

        public static List<OntologyTerm> GetAllTerms()
        {
            List<OntologyTerm> terms = new List<OntologyTerm>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Ontology LIMIT 0,50", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                terms.Add(MapReaderToTerm(reader));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    // Handle exception as needed
                }
            }

            return terms;
        }

        public static void UpdateTerm(OntologyTerm term)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("UPDATE Ontology SET Name = @Name, Def = @Def, Urls = @Urls, Synonyms = @Synonyms, Parent = @Parent WHERE ID = @ID", connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", term.Id);
                        cmd.Parameters.AddWithValue("@Name", term.Name);
                        cmd.Parameters.AddWithValue("@Def", term.Def);
                        cmd.Parameters.AddWithValue("@Urls", ConvertListToString(term.Urls));
                        cmd.Parameters.AddWithValue("@Synonyms", ConvertListToString(term.Synonyms));
                        cmd.Parameters.AddWithValue("@Parent", term.Parent);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    // Handle exception as needed
                }
            }
        }

        public static void DeleteTerm(string id)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM Ontology WHERE ID = @ID", connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    // Handle exception as needed
                }
            }
        }

        private static string ConvertListToString(List<string> list)
        {
            return list != null ? string.Join(",", list) : null;
        }

        private static List<string> ConvertStringToList(string value)
        {
            return !string.IsNullOrEmpty(value) ? new List<string>(value.Split(',')) : null;
        }

        private static OntologyTerm MapReaderToTerm(MySqlDataReader reader)
        {
            return new OntologyTerm
            {
                Id = reader["ID"].ToString(),
                Name = reader["Name"].ToString(),
                Def = reader["Def"] != DBNull.Value ? reader["Def"].ToString() : null,
                Urls = ConvertStringToList(reader["Urls"].ToString()),
                Synonyms = ConvertStringToList(reader["Synonyms"].ToString()),
                Parent = reader["Parent"] != DBNull.Value ? reader["Parent"].ToString() : null
            };
        }

        
    }
}