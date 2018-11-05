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
    class StudentsRepository : RepositoryBase
    {
        public StudentsRepository(string connectionString) : base(connectionString)
        {

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
                    Student registration = default;

                    try
                    {
                        registration.Id = row.Field<int>("id");
                        registration.Name = row.Field<string>("MeetingTime");
                        registration.UniLogin = row.Field<string>("MeetingTime");
                        //registration.AttendanceRegistrations = ;
                    }
                    catch (InvalidCastException)
                    {
                        throw new DataAccessException("Failed to convert table row into the needed AttendanceRegistration properties");
                    }

                    returnList.Add(registration);
                }

                return returnList;
            }
        }

        /// <summary>
        /// Gets all <see cref="Student"/>s from the database
        /// </summary>
        /// <returns>a list containing all the <see cref="Student"/>s</returns>
        public List<Student> GetAll()
        {
            SqlCommand getCommand = new SqlCommand("SELECT * FROM AttandanceRegistrations");
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



    }

}
