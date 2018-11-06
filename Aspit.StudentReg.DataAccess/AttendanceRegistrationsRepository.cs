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
    /// <summary>
    /// The repository for AttendanceRegistrations
    /// </summary>
    public class AttendanceRegistrationsRepository: RepositoryBase
    {
        /// <summary>
        /// Intializes a new Repository for <see cref="AttendanceRegistration"/> using the given connection string.
        /// </summary>
        /// <param name="connectionString">the connection string used to connect to the database</param>
        public AttendanceRegistrationsRepository(string connectionString) : base(connectionString)
        {

        }

        /// <summary>
        /// Updates the given <see cref="AttendanceRegistration"/> in the database
        /// </summary>
        /// <param name="attendanceRegistration">The <see cref="AttendanceRegistration"/> to update</param>
        public void Update(AttendanceRegistration attendanceRegistration)
        {
            if(attendanceRegistration.IsDefault())
            {
                throw new ArgumentException("attendanceRegistration cannot be default value");
            }

            SqlCommand updateCommand = new SqlCommand("UPDATE AttendanceRegistrations SET MeetingTime=@MeetingTime,LeavingTime=@LeavingTime,Date=@Date");
            updateCommand.Parameters.AddWithValue("@MeetingTime", attendanceRegistration.MeetingTime);
            updateCommand.Parameters.AddWithValue("@LeavingTime", attendanceRegistration.LeavingTime);
            updateCommand.Parameters.AddWithValue("@Date", attendanceRegistration.Date);

            Execute(updateCommand);
        }

        /// <summary>
        /// Creates the given <see cref="Student"/>'s <see cref="AttendanceRegistration"/> in the database
        /// </summary>
        /// <param name="attendanceRegistration">The <see cref="Student"/>'s <see cref="AttendanceRegistration"/> to create</param>
        public void CreateRegistration(Student student)
        {
            if(student == null)
            {
                throw new NullReferenceException("student cannot be null");
            }
            else if(student.AttendanceRegistrations.IsDefault())
            {
                throw new ArgumentException("the student's current AttendanceRegistration cannot be default value");
            }

            SqlCommand createCommand = new SqlCommand("INSERT INTO AttendanceRegistrations (UsersKey,MeetingTime,LeavingTime,Date) OUTPUT inserted.Id VALUES (@UsersKey,@MeetingTime,@LeavingTime,@Date)");
            createCommand.Parameters.AddWithValue("@UsersKey", student.Id);
            createCommand.Parameters.AddWithValue("@MeetingTime", student.AttendanceRegistrations.MeetingTime);
            createCommand.Parameters.AddWithValue("@LeavingTime", student.AttendanceRegistrations.LeavingTime);
            createCommand.Parameters.AddWithValue("@Date", student.AttendanceRegistrations.Date);

            DataSet output = Execute(createCommand);
            if(output.Tables.Count < 1 || output.Tables[0].Rows.Count < 1)
            {
                throw new DataAccessException("Failed to get the registration's new id");
            }
            else
            {
                student.AttendanceRegistrations = new AttendanceRegistration
                {
                    Id = output.Tables[0].Rows[0].Field<int>("Id"),
                    LeavingTime = student.AttendanceRegistrations.LeavingTime,
                    MeetingTime = student.AttendanceRegistrations.MeetingTime
                };
            }
        }

        /// <summary>
        /// Gets all <see cref="AttendanceRegistration"/>s from the database
        /// </summary>
        /// <returns>a list containing all the <see cref="AttendanceRegistration"/>s</returns>
        public List<AttendanceRegistration> GetAll()
        {
            SqlCommand getCommand = new SqlCommand("SELECT * FROM AttendanceRegistrations");
            DataSet getOutput = Execute(getCommand);

            if(getOutput.Tables.Count < 1)
            {
                throw new DataAccessException("Failed to get any tables from database");
            }
            else
            {
                return DateRowsIntoRegistrations(getOutput.Tables[0].Rows);
            }
        }

        /// <summary>
        /// Gets the AttendanceRegistration with the given id
        /// </summary>
        /// <param name="id">the id</param>
        /// <returns>a AttendanceRegistration from the database</returns>
        public AttendanceRegistration GetFromId(int id)
        {
            SqlCommand getCommand = new SqlCommand("SELECT * FROM AttendanceRegistrations WHERE Id=@Id");
            getCommand.Parameters.AddWithValue("@Id",id);
            DataSet getOutput = Execute(getCommand);

            if(getOutput.Tables.Count < 1)
            {
                throw new DataAccessException("Failed to get any tables from database");
            }
            else
            {
                List<AttendanceRegistration> registrations = DateRowsIntoRegistrations(getOutput.Tables[0].Rows);
                if(registrations.Count != 1)
                {
                    throw new DataAccessException("Failed to get an AttendanceRegistration with the given id");
                }
                else
                {
                    return registrations[0];
                }
            }
        }

        /// <summary>
        /// Gets the given <see cref="Student"/>'s <see cref="AttendanceRegistration"/>s from the database
        /// </summary>
        /// <param name="student">the <see cref="Student"/> to get the <see cref="AttendanceRegistration"/>s from</param>
        /// <returns>a lsit containing all the <see cref="AttendanceRegistration"/>s for the student</returns>
        public List<AttendanceRegistration> GetUsersRegistrations(Student student)
        {
            if(student is null)
            {
                throw new ArgumentNullException("student cannot be null");
            }

            SqlCommand getCommand = new SqlCommand("SELECT * FROM AttendanceRegistrations WHERE UsersKey=@UsersKey");
            getCommand.Parameters.AddWithValue("@UsersKey",student.Id);
            DataSet getOutput = Execute(getCommand);

            if(getOutput.Tables.Count < 1)
            {
                throw new DataAccessException("Failed to get any tables from database");
            }
            else
            {
                return DateRowsIntoRegistrations(getOutput.Tables[0].Rows);
            }
        }

        /// <summary>
        /// Takes a <see cref="DataRowCollection"/> and converts all its rows into AttendanceRegistrations
        /// </summary>
        /// <param name="dataRows">The <see cref="DataRowCollection"/> to convert into AttendanceRegistration list</param>
        /// <returns>A list of all the AttendanceRegistrations made from the dataRows</returns>
        private static List<AttendanceRegistration> DateRowsIntoRegistrations(DataRowCollection dataRows)
        {
            if(dataRows is null)
            {
                throw new ArgumentNullException("datarows cannot be null.");
            }

            if(dataRows.Count < 0)
            {
                return new List<AttendanceRegistration>();
            }
            else
            {
                List<AttendanceRegistration> returnList = new List<AttendanceRegistration>();
                foreach(DataRow row in dataRows)
                {
                    AttendanceRegistration registration = default;

                    try
                    {
                        registration.Id = row.Field<int>("Id");
                        registration.MeetingTime = row.Field<DateTime>("MeetingTime");
                        registration.LeavingTime = row.Field<DateTime>("LeavingTime");
                    }
                    catch(InvalidCastException e)
                    {
                        throw new DataAccessException("Failed to convert table row into the needed AttendanceRegistration properties", e);
                    }

                    returnList.Add(registration);
                }

                return returnList;
            }
        }
    }
}
