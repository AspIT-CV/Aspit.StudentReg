using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspit.StudentReg.Entities;
using System.Data.SqlClient;


namespace Aspit.StudentReg.DataAccess
{
    /// <summary>
    /// The repository for AttendanceRegistrations
    /// </summary>
    class AttendanceRegistrationsRepository : RepositoryBase
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
            //TODO create the update method
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the given <see cref="AttendanceRegistration"/> in the database
        /// </summary>
        /// <param name="attendanceRegistration">The <see cref="AttendanceRegistration"/> to create</param>
        public void Create(AttendanceRegistration attendanceRegistration)
        {
            //TODO create the create method
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all <see cref="AttendanceRegistration"/>s from the database
        /// </summary>
        /// <returns>a list containing all the <see cref="AttendanceRegistration"/>s</returns>
        public List<AttendanceRegistration> GetAll()
        {
            //TODO create the GetAll method
            throw new NotImplementedException();
            
        }

        /// <summary>
        /// Gets the given <see cref="User"/>'s current <see cref="AttendanceRegistration"/> from the database
        /// </summary>
        /// <param name="student">the <see cref="User"/> to get the <see cref="AttendanceRegistration"/> from</param>
        /// <returns>the <see cref="User"/>'s current <see cref="AttendanceRegistration"/></returns>
        public AttendanceRegistration GetStudentsCurrentRegistration(User student)
        {
            //TODO create the GetFromId method
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the given <see cref="User"/>'s <see cref="AttendanceRegistration"/>s from the database
        /// </summary>
        /// <param name="student">the <see cref="User"/> to get the <see cref="AttendanceRegistration"/>s from</param>
        /// <returns>a lsit containing all the <see cref="AttendanceRegistration"/>s for the student</returns>
        public List<AttendanceRegistration> GetUsersRegistrations(User student)
        {
            //TODO create the GetUsersRegistrations method
            throw new NotImplementedException();
        }
    }
}
