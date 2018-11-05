using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspit.StudentReg.DataAccess
{
    /// <summary>
    /// The repository for AttendanceRegistrations
    /// </summary>
    class RepositoryAttendanceRegistrations : RepositoryBase
    {
        /// <summary>
        /// Intializes a new Repository for AttendanceRegistration using the given connection string.
        /// </summary>
        /// <param name="connectionString">the connection string used to connect to the database</param>
        public RepositoryAttendanceRegistrations(string connectionString) : base(connectionString)
        {

        }
    }
}
