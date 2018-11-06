using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Aspit.StudentReg.Entities
{
    public class Student : User
    {
        /// <summary>
        /// The student's unilogin
        /// </summary>
        string uniLogin;

        /// <summary>
        /// The student's current <see cref="AttendanceRegistration"/>
        /// </summary>
        AttendanceRegistration attendanceRegistrations;

        /// <summary>
        /// Intializes a new <see cref="Student"/> using the given parameters
        /// </summary>
        /// <param name="id">The student's id</param>
        /// <param name="name">The student's name</param>
        /// <param name="uniLogin">The student's unilogin</param>
        /// <param name="attendanceRegistrations">The student's current <see cref="AttendanceRegistration"/></param>
        public Student(int id, string name, string uniLogin, AttendanceRegistration attendanceRegistrations = default):base(id,name)
        {
            Id = id;
            Name = name;
            UniLogin = uniLogin;
            AttendanceRegistrations = attendanceRegistrations;
        }

        /// <summary>
        /// Gets or sets the student's unilogin
        /// </summary>
        public string UniLogin
        {
            get
            {
                return uniLogin;
            }

            set
            {
                //Check if value is null
                if (value is null)
                {
                    throw new ArgumentNullException("Unilogin cannot be null");
                }

                //Trim whitespace
                value = value.Trim();

                //Check if value is a correct unilogin format
                Regex reg = new Regex(@"^[a-x]{4}[a-x0-9]{4}$");
                if (!reg.IsMatch(value))
                {
                    throw new ArgumentException("Unilogin is invalid");
                }
                uniLogin = value;
            }
        }

        /// <summary>
        /// Gets or sets the student's current <see cref="AttendanceRegistration"/>
        /// </summary>
        public AttendanceRegistration AttendanceRegistrations
        {
            get
            {
                return attendanceRegistrations;
            }

            set
            {
                attendanceRegistrations = value;
            }
        }

    }
}
