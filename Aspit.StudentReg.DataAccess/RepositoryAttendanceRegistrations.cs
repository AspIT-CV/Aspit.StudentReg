using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspit.StudentReg.Entities;


namespace Aspit.StudentReg.DataAccess
{
    /// <summary>
    /// The repository for AttendanceRegistrations
    /// </summary>
    class RepositoryAttendanceRegistrations : RepositoryBase
    {
        /// <summary>
        /// Intializes a new Repository for <see cref="AttendanceRegistration"/> using the given connection string.
        /// </summary>
        /// <param name="connectionString">the connection string used to connect to the database</param>
        public RepositoryAttendanceRegistrations(string connectionString) : base(connectionString)
        {

        }

        /// <summary>
        /// Updates the given <see cref="AttendanceRegistration"/> in the database
        /// </summary>
        /// <param name="attendanceRegistration">The <see cref="AttendanceRegistration"/> to update</param>
        public void Update(AttendanceRegistration attendanceRegistration)
        {
            //TODO create the update method
        }

        /// <summary>
        /// Creates the given <see cref="AttendanceRegistration"/> in the database
        /// </summary>
        /// <param name="attendanceRegistration">The <see cref="AttendanceRegistration"/> to create</param>
        public void Create(AttendanceRegistration attendanceRegistration)
        {
            //TODO create the create method
        }

        /// <summary>
        /// Gets all <see cref="AttendanceRegistration"/>s from the database
        /// </summary>
        /// <returns>a <see cref="IEnumerator<AttendanceRegistration>"/> containing all the <see cref="AttendanceRegistration"/>s</returns>
        public IEnumerator<AttendanceRegistration> Get()
        {
            //TODO create the get method
            yield break;
        }
    }
}
