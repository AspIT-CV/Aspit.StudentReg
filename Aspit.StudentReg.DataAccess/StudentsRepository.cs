using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspit.StudentReg.Entities;
using System.Data.SqlClient;
using System.Data;

namespace Aspit.StudentReg.DataAccess
{
    public class StudentsRepository : RepositoryBase
    {
        public StudentsRepository(string connectionString) : base(connectionString)
        {

        }

        /// <summary>
        /// Creates the given <see cref="Student"/> in the database
        /// </summary>
        public void CreateStudent(Student student)
        {
            if (student == null)
            {
                throw new NullReferenceException("student cannot be null");
            }

            SqlCommand createCommand = new SqlCommand("INSERT INTO Users (name,username,AttendanceRegistrationsKey) OUTPUT inserted.Id VALUES (@name,@username,@attendanceregistrationskey)");
            createCommand.Parameters.AddWithValue("@name", student.Name);
            createCommand.Parameters.AddWithValue("@username", student.UniLogin);

            if (student.AttendanceRegistrations.IsDefault())
            {
                createCommand.Parameters.AddWithValue("@attendanceregistrationskey", DBNull.Value);
            } else
            {
                createCommand.Parameters.AddWithValue("@attendanceregistrationskey", student.AttendanceRegistrations.Id);
            }

            

            DataSet output = Execute(createCommand);
            if (output.Tables.Count < 1 || output.Tables[0].Rows.Count < 1)
            {
                throw new DataAccessException("Failed to get the new student's id");
            }
            else
            {
                student.Id = output.Tables[0].Rows[0].Field<int>("Id");
            }
        }

        /// <summary>
        /// Update the given <see cref="Student"/> in the database
        /// </summary>
        public void UpdateStudent(Student student)
        {
            if (student == null)
            {
                throw new NullReferenceException("student cannot be null");
            }

            SqlCommand updateCommand = new SqlCommand("UPDATE Users SET name = @name, username = @username, AttendanceRegistrationsKey = @attendanceregistrationskey WHERE id = @id;");
            updateCommand.Parameters.AddWithValue("@id", student.Id);
            updateCommand.Parameters.AddWithValue("@name", student.Name);
            updateCommand.Parameters.AddWithValue("@username", student.UniLogin);
            if (student.AttendanceRegistrations.IsDefault())
            {
                updateCommand.Parameters.AddWithValue("@attendanceregistrationskey", DBNull.Value);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@attendanceregistrationskey", student.AttendanceRegistrations.Id);
            }

            DataSet output = Execute(updateCommand);
        }

        /// <summary>
        /// Gets all <see cref="Student"/>s from the database
        /// </summary>
        /// <returns>a list containing all the <see cref="Student"/>s</returns>
        public List<Student> GetAll()
        {
            SqlCommand getCommand = new SqlCommand("SELECT * FROM Users");
            DataSet getOutput = Execute(getCommand);

            if (getOutput.Tables.Count < 0)
            {
                throw new DataAccessException("Failed to get any tables from database");
            }
            else
            {
                return ConvertDateRowsIntoStudents(getOutput.Tables[0].Rows);
            }
        }

        /// <summary>
        /// Gets the AttendanceRegistration with the given id
        /// </summary>
        /// <param name="id">the databaseid</param>
        /// <returns>a AttendanceRegistration from the database</returns>
        public Student GetFromId(int id)
        {
            SqlCommand getCommand = new SqlCommand("SELECT * FROM Users WHERE Id=@Id");
            getCommand.Parameters.AddWithValue("@Id", id);
            DataSet getOutput = Execute(getCommand);

            if (getOutput.Tables.Count < 1)
            {
                throw new DataAccessException("Failed to get any tables from database");
            }
            else
            {
                List<Student> registrations = ConvertDateRowsIntoStudents(getOutput.Tables[0].Rows);
                if (registrations.Count != 1)
                {
                    throw new DataAccessException("Failed to get an Student with the given id");
                }
                else
                {
                    return registrations[0];
                }
            }
        }


        public bool IsCheckedIn(Student student)
        {
            if (student.AttendanceRegistrations.IsDefault())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CheckIn(Student student, DateTime time)
        {
            AttendanceRegistrationsRepository attendanceRegistrationsRepository = new AttendanceRegistrationsRepository(RepositoryBase.RetrieveConnectionString());
            

            AttendanceRegistration registration = new AttendanceRegistration
            {
                MeetingTime = DateTime.Now
            };

            student.AttendanceRegistrations = registration;
            attendanceRegistrationsRepository.CreateRegistration(student);
            UpdateStudent(student);
        }


        /// <summary>
        /// Takes a <see cref="DataRowCollection"/> and converts all its rows into Students
        /// </summary>
        /// <param name="dataRows">The <see cref="DataRowCollection"/> to convert into Students list</param>
        /// <returns>A list of all the Students made from the dataRows</returns>
        private static List<Student> ConvertDateRowsIntoStudents(DataRowCollection dataRows)
            {
                if (dataRows is null)
                {
                    throw new ArgumentNullException("datarows cannot be null.");
                }

                if (dataRows.Count < 0)
                {
                    return new List<Student>();
                }
                else
                {
                    List<Student> returnList = new List<Student>();
                    foreach (DataRow row in dataRows)
                    {
                        Student registration;
                        try
                        {
                            int registrationId = row.Field<int>("id");
                            string registrationName = row.Field<string>("name");
                            string registrationUniLogin = row.Field<string>("username");

                            Nullable<int> AttendanceRegistrationsKey = row.Field<Nullable<int>>("AttendanceRegistrationsKey");

                            AttendanceRegistration attendanceRegistration;

                            if (AttendanceRegistrationsKey is null)
                            {
                                attendanceRegistration = default;
                            }
                            else
                            {
                                AttendanceRegistrationsRepository ARR = new AttendanceRegistrationsRepository(RepositoryBase.RetrieveConnectionString());
                                attendanceRegistration = ARR.GetFromId(AttendanceRegistrationsKey.Value);
                            }

                            registration = new Student(registrationId, registrationName, registrationUniLogin, attendanceRegistration);
                        }
                        catch (InvalidCastException)
                        {
                            throw new DataAccessException("Failed to convert table row into the needed Student properties");
                        }

                        returnList.Add(registration);
                    }

                    return returnList;
                }
            }

    }

}
